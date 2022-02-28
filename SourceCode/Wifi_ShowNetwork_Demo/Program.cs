using System;

using Wifi_ShowNetwork;
using static Wifi_ShowNetwork.PublicFunction;

namespace Wifi_ShowNetwork_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 檢查當前系統是否支援此API
            WlanInterface WlanInterfaces = new WlanInterface();
            if (WlanInterfaces.IsNoSupport)
            {
                Console.WriteLine("Now System not support the api");
                Console.ReadKey();
                return;
            }
            #endregion

            #region 設置當前本機所有的無線網路卡 觸發連線事件

            for (int i = 0; i < WlanInterfaces.AllInterfaces.Length; i++)
            {
                WLAN_INTERFACE_INFO Interface = WlanInterfaces.AllInterfaces[i];
                Console.WriteLine("[" + i + "]" + " Your Wireess Network Card ：" + Interface.strInterfaceDescription);
                Interface.WlanNotification += inte_WlanNotification;
            }

            #endregion

            #region 選擇當前要使用的無線網卡
            Console.WriteLine("Please enter you want to connect divce number：");
            string readKey = Console.ReadLine();
            int num = -1;
            bool isParseSucess = Int32.TryParse(readKey, out num);
            bool isInRange = num >= 0 && WlanInterfaces.AllInterfaces.Length > num;
            if (isParseSucess == false || isInRange == false)
            {
                Console.WriteLine("Please enter correct number");
                Console.ReadKey();
                return;
            }
            #endregion

            #region 使用當前的無線網路卡偵測所有的WiFi熱點
            WLAN_INTERFACE_INFO useInterface = new WLAN_INTERFACE_INFO();
            useInterface = WlanInterfaces.AllInterfaces[num];
            Hotspot WifiHotspot = new Hotspot(useInterface);
            WifiHotspot.GetAllHotpot();
            #endregion

            Console.WriteLine("Find the wifi hotspot = ");

            #region 顯示當前所有Hotspot資訊
            for (int i = 0; i < WifiHotspot.NumberOfHotspot; i++)
            {
                Console.WriteLine("-------" + "SSID_" + i + "       : " + WifiHotspot.AvailableNetwork.wlanAvailableNetwork[i].SSID + "-------");
                Console.WriteLine("Network Type         : " + WifiHotspot.AvailableNetwork.wlanAvailableNetwork[i].Network_Type);
                Console.WriteLine("Authentication       : " + WifiHotspot.AvailableNetwork.wlanAvailableNetwork[i].Authentication);
                Console.WriteLine("Encryption           : " + WifiHotspot.AvailableNetwork.wlanAvailableNetwork[i].Encryption);
                Console.WriteLine("MAC                  : " + WifiHotspot.BssNetwork.wlanBssEntries[i].MAC);
                Console.WriteLine("Signal               : " + WifiHotspot.AvailableNetwork.wlanAvailableNetwork[i].Signal);
                Console.WriteLine("Radio Type           : " + WifiHotspot.AvailableNetwork.wlanAvailableNetwork[i].Radio);
                Console.WriteLine("Channel              : " + WifiHotspot.BssNetwork.wlanBssEntries[i].Channel);
                Console.WriteLine("Speed                : " + WifiHotspot.BssNetwork.wlanBssEntries[i].Speed);
                Console.WriteLine("");
            }
            #endregion

            #region 進行選擇使用的熱點
            Console.WriteLine("Please choose wifi hotsport number = ");
            readKey = Console.ReadLine();
            num = -1;
            isParseSucess = Int32.TryParse(readKey, out num);
            isInRange = num >= 0 && WifiHotspot.NumberOfHotspot > num;
            if (isParseSucess == false || isInRange == false)
            {
                Console.WriteLine("Please enter correct number");
                Console.ReadKey();
                return;
            }

            WifiHotspot.SelectNetwork = new Wlan_Hotpot(WifiHotspot.AvailableNetwork.wlanAvailableNetwork[num], WifiHotspot.BssNetwork.wlanBssEntries[num]);
            ConnectControl SelectNetwork = new ConnectControl(WifiHotspot.SelectNetwork, useInterface);
            #endregion

            #region 輸入當前熱點的密碼 並判斷是否合法
            if (SelectNetwork.IsPasswordRequired)
            {
                Console.WriteLine("Please enter user password ：");
                SelectNetwork.Password = Console.ReadLine();
                if (SelectNetwork.IsPasswordLegal == false)
                {
                    Console.WriteLine("The password is not legal. Please check again");
                    Console.ReadKey();
                    return;
                }
            }
            #endregion

            SelectNetwork.ConnectToNetwork(false);

            Console.WriteLine("Please Wait Connect result .When have result you can enter any key to exit");
            Console.ReadKey();
        }

        public static void inte_WlanNotification(WLAN_NOTIFICATION_DATA notifyData)
        {
            if (notifyData.notificationSource == WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_ACM && (WLAN_NOTIFICATION_CODE_ACM)notifyData.NotificationCode == WLAN_NOTIFICATION_CODE_ACM.wlan_notification_acm_disconnected)
            {
                Console.WriteLine("Connect Fail");
            }
            else if (notifyData.notificationSource == WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_MSM && (WLAN_NOTIFICATION_CODE_MSM)notifyData.NotificationCode == WLAN_NOTIFICATION_CODE_MSM.wlan_notification_msm_connected)
            {
                Console.WriteLine("Connect Sucess");
            }
        }
    }
}
