using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

using static Wifi_ShowNetwork.PublicFunction;

namespace Wifi_ShowNetwork {
    public class WlanInterface {
        #region Public

        public bool IsNoSupport = false;
        public WLAN_INTERFACE_INFO[] AllInterfaces => Interfaces;
        public int WlanInterfaceCount => WlanInfo.dwNumberOfItems;
        public IntPtr ClientHandle;

        #endregion

        #region Internal

        internal delegate void WlanNotificationCallbackDelegate (ref WLAN_NOTIFICATION_DATA notificationData, IntPtr context);
        internal WlanNotificationCallbackDelegate InfoCallBack;
        internal Dictionary<Guid, WLAN_INTERFACE_INFO> ifaces = new Dictionary<Guid, WLAN_INTERFACE_INFO> ();
        internal WLAN_INTERFACE_INFO_LIST WlanInfo = new WLAN_INTERFACE_INFO_LIST ();

        #endregion

        #region Private

        private WLAN_INTERFACE_INFO[] Interfaces {
            get {
                if (IsNoSupport)
                    return null;

                WlanEnumInterfaces (ClientHandle, IntPtr.Zero, WlanInfo); // 使用API獲取WLAN基礎Info

                IntPtr num = (IntPtr) WlanInfo.dwNumberOfItems;
                WlanInfo.dwNumberOfItems = Marshal.ReadInt32 (num); // 當前WLAN Interface 的數量
                WlanInfo.InterfaceInfo = new WLAN_INTERFACE_INFO[WlanInfo.dwNumberOfItems]; // 創建新的陣列

                for (int i = 0; i < WlanInfo.dwNumberOfItems; i++) {
                    WLAN_INTERFACE_INFO tempWlanInfo = new WLAN_INTERFACE_INFO ();
                    byte[] intGuid = new byte[16];

                    IntPtr pItemList = new IntPtr (num.ToInt32 () + (i * 284));

                    for (int j = 0; j < 16; j++)
                        intGuid[j] = Marshal.ReadByte (pItemList, 8 + j);

                    IntPtr InterfacePtr = new IntPtr (pItemList.ToInt32 () + 8);
                    tempWlanInfo.strInterfaceDescription = Marshal.PtrToStringUni (new IntPtr (pItemList.ToInt32 () + 24), 256).Replace ("\0", "");
                    tempWlanInfo.isState = (WLAN_INTERFACE_STATE) Marshal.ReadInt32 (pItemList, 280);
                    tempWlanInfo.InterfaceGuid = new Guid (intGuid);
                    tempWlanInfo.ClientHandle = ClientHandle;

                    if (ifaces.ContainsKey (tempWlanInfo.InterfaceGuid))
                        WlanInfo.InterfaceInfo[i] = ifaces[tempWlanInfo.InterfaceGuid];
                    else
                        WlanInfo.InterfaceInfo[i] = tempWlanInfo;

                    ifaces[tempWlanInfo.InterfaceGuid] = WlanInfo.InterfaceInfo[i];
                }

                return WlanInfo.InterfaceInfo;
            }
        }

        #endregion

        #region 建構子 & 解構子
        public WlanInterface () {
            OperatingSystem OSVersion = Environment.OSVersion;
            uint pdwNegotiatedVersion;

            if (OSVersion.Platform == PlatformID.Win32NT && OSVersion.Version.Major == 5 && OSVersion.Version.Minor != 0) {
                #region XP 沒有Netsh wlan 系列指令 故並不支援
                IsNoSupport = true;
                Trace.WriteLine ("XP System not support");
                return;
                #endregion
            }

            try {
                if (WlanOpenHandle (1, IntPtr.Zero, out pdwNegotiatedVersion, out ClientHandle) != 0) {
                    #region 不具有無線網卡裝置 故並不支援
                    IsNoSupport = true;
                    Trace.WriteLine ("Device not Exist");
                    return;
                    #endregion
                }
            } catch {
                #region 如出現意外 則不支援
                IsNoSupport = true;
                Trace.WriteLine ("Device not Ready");
                return;
                #endregion
            }

            try {
                InfoCallBack = new WlanNotificationCallbackDelegate (OnWlanNotification);

                WLAN_NOTIFICATION_SOURCE prevSrc;
                WlanRegisterNotification (ClientHandle, WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_ALL, false, InfoCallBack, IntPtr.Zero, IntPtr.Zero, out prevSrc);
            } catch (Exception ex) {
                #region 設置連線和斷線事件出現意外 則不支援
                WlanCloseHandle (ClientHandle, IntPtr.Zero);
                IsNoSupport = true;
                Trace.WriteLine ("Setting Call Back Error Message = " + ex.Message);
                return;
                #endregion
            }
        }

        ~WlanInterface () {
            // Free the handle when deconstructing the client. There won't be a handle if its xp sp 2 without wlanapi installed
            try {
                WlanCloseHandle (ClientHandle, IntPtr.Zero);
            } catch (Exception ex) {
                Trace.WriteLine ("[~WlanInterfaces] " + " Error :" + ex.Message);
            }
        }

        #endregion

        private void OnWlanNotification (ref WLAN_NOTIFICATION_DATA notifyData, IntPtr context) {
            if (IsNoSupport)
                return;

            WLAN_INTERFACE_INFO TempInterface = ifaces.ContainsKey (notifyData.interfaceGuid) ? ifaces[notifyData.interfaceGuid] : null;
            TempInterface.OnWlanNotification (notifyData); // 建置單一Interface的連線事件
        }
    }
}