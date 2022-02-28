using System;
using System.Collections.Generic;
using System.Linq;

using static Wifi_ShowNetwork.PublicFunction;

namespace Wifi_ShowNetwork {
    public class Hotspot {
        public WLAN_INTERFACE_INFO WLANInterface;

        public WLAN_AVAILABLE_NETWORK_LIST AvailableNetwork;
        public WLAN_BSS_LIST BssNetwork;
        public int NumberOfHotspot => Convert.ToInt32 (Math.Min (AvailableNetwork.dwNumberOfItems, BssNetwork.dwNumberOfItems));

        public Wlan_Hotpot SelectNetwork;

        #region 建構子

        public Hotspot (WLAN_INTERFACE_INFO WLANInterface) {
            this.WLANInterface = WLANInterface;
        }
        public Hotspot () { }

        #endregion

        public void GetAllHotpot () {
            #region Available Network List

            IntPtr ppAvailableNetworkList = new IntPtr ();
            WlanGetAvailableNetworkList (WLANInterface.ClientHandle, WLANInterface.InterfaceGuid, WlanGetAvailableNetworkListFlags.WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_MANUAL_HIDDEN_PROFILES, new IntPtr (), out ppAvailableNetworkList);
            WLAN_AVAILABLE_NETWORK_LIST tempAvailable = new WLAN_AVAILABLE_NETWORK_LIST (ppAvailableNetworkList);

            // 由於AVAILABLE NETWORK LIST 所獲取的Number個數(dwNumberOfItems) 會重複包含有Profile的熱點
            // Ex: 有個熱點叫1234並以儲存其Profile，那麼顯示會有兩個1234熱點資訊
            List<WLAN_AVAILABLE_NETWORK> tempList = new List<WLAN_AVAILABLE_NETWORK> ();
            for (int i = 0; i < tempAvailable.wlanAvailableNetwork.Length; i++)
                if (!(tempAvailable.wlanAvailableNetwork.Where (n => n.Equals (tempAvailable.wlanAvailableNetwork[i]) && !string.IsNullOrEmpty (n.strProfileName)).Any ()))
                    tempList.Add (tempAvailable.wlanAvailableNetwork[i]);
            tempList.CopyTo (tempAvailable.wlanAvailableNetwork);
            #endregion

            #region BSS Ntwork List

            IntPtr bssListPtr = new IntPtr ();
            WlanGetNetworkBssList (WLANInterface.ClientHandle, WLANInterface.InterfaceGuid, IntPtr.Zero, DOT11_BSS_TYPE.dot11_BSS_type_any, false, IntPtr.Zero, out bssListPtr);
            WLAN_BSS_LIST tempBss = new WLAN_BSS_LIST (bssListPtr);

            #endregion

            AvailableNetwork = tempAvailable;
            BssNetwork = tempBss;
        }
    }
}