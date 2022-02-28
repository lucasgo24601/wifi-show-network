using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using static Wifi_ShowNetwork.PublicFunction;

namespace Wifi_ShowNetwork {
    public class ConnectControl {

        public Wlan_Hotpot ConnectNetwork;

        public WLAN_INTERFACE_INFO UseInterface;
        public string Password; // Password

        public bool IsPasswordRequired;

        #region 建構子
        public ConnectControl () {

        }
        public ConnectControl (Wlan_Hotpot ConnectNetwork, WLAN_INTERFACE_INFO UseInterface) {
            this.ConnectNetwork = ConnectNetwork;
            this.UseInterface = UseInterface;

            Trace.WriteLine ("Now use wlan inteface = " + UseInterface.strInterfaceDescription);
            Trace.WriteLine ("Now Connect Network name = " + ConnectNetwork.SSID);

            IsPasswordRequired = ConnectNetwork.SelectAvailableNetwork.dot11DefaultCipherAlgorithm != DOT11_CIPHER_ALGORITHM.DOT11_CIPHER_ALGO_NONE &
                ConnectNetwork.SelectAvailableNetwork.bSecurityEnabled;

            Trace.WriteLine ("Need Password ?? " + IsPasswordRequired);
        }
        #endregion

        public bool HasProfile {
            get {
                try {
                    // 獲取當前電腦所有的以儲存Profile
                    return UseInterface.GetProfiles ().ProfileInfo.Where (p => p.Description == ConnectNetwork.SelectAvailableNetwork.SSID).Any ();
                } catch {
                    return false;
                }
            }
        }

        public bool IsPasswordLegal {
            get {
                return IsLegalPassword (Password, ConnectNetwork.SelectAvailableNetwork.dot11DefaultCipherAlgorithm);
            }
        }

        public bool IsLegalPassword (string password, DOT11_CIPHER_ALGORITHM cipherAlgorithm) {
            int len = password.Length;
            switch (cipherAlgorithm) {
                case DOT11_CIPHER_ALGORITHM.DOT11_CIPHER_ALGO_NONE:
                    return true;
                case DOT11_CIPHER_ALGORITHM.DOT11_CIPHER_ALGO_WEP: // WEP key is 10, 26 or 40 hex digits long.
                    if (string.IsNullOrEmpty (password))
                        return false;

                    // 指定40位元  + 24位元的IV值 實現  64位元的RC4加密值 其最大長度為10位元的數據
                    // 指定104位元 + 24位元的IV值 實現 128位元的RC4加密值 其最大長度為26位元的數據
                    // 指定任意位元 其最大長度為 40 位元的數據
                    bool correctLength = len == 10 || len == 26 || len == 40;
                    bool onlyHex = new Regex ("^[0-9A-F]+$").IsMatch (password);

                    return correctLength && onlyHex;
                case DOT11_CIPHER_ALGORITHM.DOT11_CIPHER_ALGO_CCMP: // WPA2-PSK 8 to 63 ASCII characters					
                case DOT11_CIPHER_ALGORITHM.DOT11_CIPHER_ALGO_TKIP: // WPA-PSK 8 to 63 ASCII characters
                    if (string.IsNullOrEmpty (password))
                        return false;

                    return 8 <= len && len <= 63;
                default:
                    return true;
            }

        }

        public bool ConnectToNetwork (bool OldProfile) {
            try {
                if (HasProfile == false & OldProfile)
                    return false;

                if (HasProfile & OldProfile == false) {
                    WlanDeleteProfile (UseInterface.ClientHandle, UseInterface.InterfaceGuid, ConnectNetwork.SelectAvailableNetwork.SSID, IntPtr.Zero);
                }

                string profileXML = Generate (ConnectNetwork.SelectAvailableNetwork, Password);

                WlanReasonCode reasonCode;
                WlanSetProfile (UseInterface.ClientHandle,
                    UseInterface.InterfaceGuid,
                    WlanProfileFlags.AllUser,
                    profileXML, null, OldProfile, IntPtr.Zero, out reasonCode);

                return true;

            } catch (Exception) {
                return false;
            }
        }

        private static string GetHexString (byte[] ba) {
            StringBuilder sb = new StringBuilder (ba.Length * 2);

            foreach (byte b in ba) {
                if (b == 0)
                    break;

                sb.AppendFormat ("{0:x2}", b);
            }

            return sb.ToString ();
        }
        internal static string Generate (WLAN_AVAILABLE_NETWORK network, string password) {
            string profile = string.Empty;
            string template = string.Empty;
            string name = network.SSID;
            string hex = GetHexString (Encoding.ASCII.GetBytes (name));

            var authAlgo = network.dot11DefaultAuthAlgorithm;

            switch (network.dot11DefaultCipherAlgorithm) {
                case DOT11_CIPHER_ALGORITHM.DOT11_CIPHER_ALGO_NONE:
                    template = GetTemplate ("OPEN");
                    profile = string.Format (template, name, hex);
                    break;
                case DOT11_CIPHER_ALGORITHM.DOT11_CIPHER_ALGO_WEP:
                    template = GetTemplate ("WEP");
                    profile = string.Format (template, name, hex, password);
                    break;
                case DOT11_CIPHER_ALGORITHM.DOT11_CIPHER_ALGO_CCMP:
                    if (authAlgo == DOT11_AUTH_ALGORITHM.DOT11_AUTH_ALGO_RSNA) {
                        template = GetTemplate ("WPA2-Enterprise-PEAP-MSCHAPv2");
                        profile = string.Format (template, name);
                    } else // PSK
                    {
                        template = GetTemplate ("WPA2-PSK");
                        profile = string.Format (template, name, password);
                    }
                    break;
                case DOT11_CIPHER_ALGORITHM.DOT11_CIPHER_ALGO_TKIP:

                    if (authAlgo == DOT11_AUTH_ALGORITHM.DOT11_AUTH_ALGO_RSNA) {
                        template = GetTemplate ("WPA-Enterprise-PEAP-MSCHAPv2");
                        profile = string.Format (template, name);
                    } else // PSK
                    {
                        template = GetTemplate ("WPA-PSK");
                        profile = string.Format (template, name, password);
                    }

                    break;
                default:
                    throw new NotImplementedException ("Profile for selected cipher algorithm is not implemented");
            }

            return profile;
        }

        private static string GetTemplate (string name) {
            string resourceName = string.Format ("Wifi_ShowNetwork.ProfileXML.{0}.xml", name);
            Trace.WriteLine (resourceName);
            using (StreamReader reader = new StreamReader (
                Assembly.GetExecutingAssembly ().GetManifestResourceStream (resourceName))) {
                Trace.WriteLine ("dsa");
                return reader.ReadToEnd ();
            }

        }
    }
}