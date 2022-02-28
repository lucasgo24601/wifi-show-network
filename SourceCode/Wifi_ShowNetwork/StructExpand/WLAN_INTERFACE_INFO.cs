using System;

using static Wifi_ShowNetwork.PublicFunction;

namespace Wifi_ShowNetwork {
    public class WLAN_INTERFACE_INFO {
        // 因為了增加個別interface 連線和斷線時的事件 
        // 所以使用Class 加入除了原屬性以外的程式並重新封裝 

        #region 原屬性

        public Guid InterfaceGuid;
        public string strInterfaceDescription;
        public WLAN_INTERFACE_STATE isState;

        #endregion

        #region Public

        public delegate void WlanNotificationEventHandler (WLAN_NOTIFICATION_DATA notifyData);
        public event WlanNotificationEventHandler WlanNotification;
        public IntPtr ClientHandle;

        #endregion

        #region 建構子多載

        public WLAN_INTERFACE_INFO (Guid tempGuid, string tempDescritrion, WLAN_INTERFACE_STATE tempIsState) {
            InterfaceGuid = tempGuid;
            strInterfaceDescription = tempDescritrion;
            isState = tempIsState;
        }

        public WLAN_INTERFACE_INFO () { }

        #endregion

        public void OnWlanNotification (WLAN_NOTIFICATION_DATA notifyData) {
            WlanNotification?.Invoke (notifyData); // if != null WlanNotification(notifyData)
        }

        public WLAN_PROFILE_INFO_LIST GetProfiles () {
            IntPtr profileListPtr;
            WlanGetProfileList (ClientHandle, InterfaceGuid, IntPtr.Zero, out profileListPtr);
            WLAN_PROFILE_INFO_LIST tempProfile = new WLAN_PROFILE_INFO_LIST (profileListPtr);

            return tempProfile;
        }
    }
}