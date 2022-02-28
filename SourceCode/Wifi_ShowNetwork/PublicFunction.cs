using System;
using System.Runtime.InteropServices;

using static Wifi_ShowNetwork.WlanInterface;

namespace Wifi_ShowNetwork {
    public class PublicFunction {
        #region Enum 

        // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/wlantypes/ne-wlantypes-_dot11_bss_type
        // https://www.pinvoke.net/default.aspx/Enums/DOT11_BSS_TYPE.html
        public enum DOT11_BSS_TYPE {
            dot11_BSS_type_infrastructure = 1,
            dot11_BSS_type_independent = 2,
            dot11_BSS_type_any = 3,
        }

        // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/windot11/ne-windot11-_dot11_phy_type
        // https://www.pinvoke.net/default.aspx/Enums/DOT11_PHY_TYPE.html
        public enum DOT11_PHY_TYPE {
            dot11_phy_type_unknown,
            dot11_phy_type_any = dot11_phy_type_unknown,
            dot11_phy_type_fhss,
            dot11_phy_type_dsss,
            dot11_phy_type_irbaseband,
            dot11_phy_type_ofdm,
            dot11_phy_type_hrdsss,
            dot11_phy_type_erp,
            dot11_phy_type_ht,
            dot11_phy_type_vht,
            dot11_phy_type_IHV_start,
            dot11_phy_type_IHV_end,
        }

        // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/wlantypes/ne-wlantypes-_dot11_auth_algorithm
        // https://www.pinvoke.net/default.aspx/Enums/DOT11_AUTH_ALGORITHM.html
        public enum DOT11_AUTH_ALGORITHM {
            DOT11_AUTH_ALGO_80211_Open = 1,
            DOT11_AUTH_ALGO_80211_Shared_Key = 2,
            DOT11_AUTH_ALGO_WPA = 3,
            DOT11_AUTH_ALGO_WPA_PSK = 4,
            DOT11_AUTH_ALGO_WPA_None = 5,
            DOT11_AUTH_ALGO_RSNA = 6,
            DOT11_AUTH_ALGO_RSNA_PSK = 7,
            DOT11_AUTH_ALGO_IHV_Start = -2147483648,
            DOT11_AUTH_ALGO_IHV_End = -1,
        }

        // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/wlantypes/ne-wlantypes-_dot11_cipher_algorithm
        // https://www.pinvoke.net/default.aspx/Enums/DOT11_CIPHER_ALGORITHM.html
        public enum DOT11_CIPHER_ALGORITHM {
            DOT11_CIPHER_ALGO_NONE = 0,
            DOT11_CIPHER_ALGO_WEP40 = 1,
            DOT11_CIPHER_ALGO_TKIP = 2,
            DOT11_CIPHER_ALGO_CCMP = 4,
            DOT11_CIPHER_ALGO_WEP104 = 5,
            DOT11_CIPHER_ALGO_WPA_USE_GROUP = 256,
            DOT11_CIPHER_ALGO_RSN_USE_GROUP = 256,
            DOT11_CIPHER_ALGO_WEP = 257,
            DOT11_CIPHER_ALGO_IHV_START = -2147483648,
            DOT11_CIPHER_ALGO_IHV_END = -1,
        }

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlangetavailablenetworklist
        // 自行寫
        [Flags]
        public enum WlanGetAvailableNetworkListFlags {
            WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_ADHOC_PROFILES = 0x00000001,
            WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_MANUAL_HIDDEN_PROFILES = 0x00000002
        }

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms706902(v=vs.85).aspx
        // https://www.pinvoke.net/default.aspx/Enums/WLAN_NOTIFICATION_SOURCE.html
        [Flags]
        public enum WLAN_NOTIFICATION_SOURCE : uint {
            WLAN_NOTIFICATION_SOURCE_NONE = 0,
            WLAN_NOTIFICATION_SOURCE_ONEX = 0X00000004,
            WLAN_NOTIFICATION_SOURCE_ACM = 0X00000008,
            WLAN_NOTIFICATION_SOURCE_MSM = 0X00000010,
            WLAN_NOTIFICATION_SOURCE_SECURITY = 0X00000020,
            WLAN_NOTIFICATION_SOURCE_IHV = 0X00000040,
            WLAN_NOTIFICATION_SOURCE_HNWK = 0X00000080,
            WLAN_NOTIFICATION_SOURCE_ALL = 0X0000FFFF,
        }

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/ne-wlanapi-_wlan_notification_acm
        // https://www.pinvoke.net/default.aspx/Structures.WLAN_NOTIFICATION_CODE_ACM
        public enum WLAN_NOTIFICATION_CODE_ACM : short {
            wlan_notification_acm_start = 0,
            wlan_notification_acm_autoconf_enabled,
            wlan_notification_acm_autoconf_disabled,
            wlan_notification_acm_background_scan_enabled,
            wlan_notification_acm_background_scan_disabled,
            wlan_notification_acm_bss_type_change,
            wlan_notification_acm_power_setting_change,
            wlan_notification_acm_scan_complete,
            wlan_notification_acm_scan_fail,
            wlan_notification_acm_connection_start,
            wlan_notification_acm_connection_complete,
            wlan_notification_acm_connection_attempt_fail,
            wlan_notification_acm_filter_list_change,
            wlan_notification_acm_interface_arrival,
            wlan_notification_acm_interface_removal,
            wlan_notification_acm_profile_change,
            wlan_notification_acm_profile_name_change,
            wlan_notification_acm_profiles_exhausted,
            wlan_notification_acm_network_not_available,
            wlan_notification_acm_network_available,
            wlan_notification_acm_disconnecting,
            wlan_notification_acm_disconnected,
            wlan_notification_acm_adhoc_network_state_change,
            wlan_notification_acm_profile_unblocked,
            wlan_notification_acm_screen_power_change,
            wlan_notification_acm_end
        }

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/ne-wlanapi-_wlan_notification_msm
        // https://www.pinvoke.net/default.aspx/Structures.WLAN_NOTIFICATION_CODE_MSM
        public enum WLAN_NOTIFICATION_CODE_MSM : short {
            wlan_notification_msm_start = 0,
            wlan_notification_msm_associating,
            wlan_notification_msm_associated,
            wlan_notification_msm_authenticating,
            wlan_notification_msm_connected,
            wlan_notification_msm_roaming_start,
            wlan_notification_msm_roaming_end,
            wlan_notification_msm_radio_state_change,
            wlan_notification_msm_signal_quality_change,
            wlan_notification_msm_disassociating,
            wlan_notification_msm_disconnected,
            wlan_notification_msm_peer_join,
            wlan_notification_msm_peer_leave,
            wlan_notification_msm_adapter_removal,
            wlan_notification_msm_adapter_operation_mode_change,
            wlan_notification_msm_end
        }

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/ne-wlanapi-_wlan_interface_state
        // https://www.pinvoke.net/default.aspx/Enums/WLAN_INTERFACE_STATE.html
        public enum WLAN_INTERFACE_STATE {
            wlan_interface_state_not_ready = 0,
            wlan_interface_state_connected = 1,
            wlan_interface_state_ad_hoc_network_formed = 2,
            wlan_interface_state_disconnecting = 3,
            wlan_interface_state_disconnected = 4,
            wlan_interface_state_associating = 5,
            wlan_interface_state_discovering = 6,
            wlan_interface_state_authenticating = 7
        }

        // https://en.wikipedia.org/wiki/List_of_WLAN_channels
        // 自行寫
        public enum ChannelFrequency_enum {
            Channel_1 = 2412,
            Channel_2 = 2417,
            Channel_3 = 2422,
            Channel_4 = 2427,
            Channel_5 = 2432,
            Channel_6 = 2437,
            Channel_7 = 2442,
            Channel_8 = 2447,
            Channel_9 = 2452,
            Channel_10 = 2457,
            Channel_11 = 2462,
            Channel_12 = 2467,
            Channel_13 = 2472,
            Channel_14 = 2484,
            Channel_131 = 3658,
            Channel_133 = 3665,
            Channel_135 = 3675,
            Channel_137 = 3685,
            Channel_16 = 5080,
            Channel_34 = 5170,
            Channel_36 = 5180,
            Channel_38 = 5190,
            Channel_40 = 5200,
            Channel_42 = 5210,
            Channel_44 = 5220,
            Channel_46 = 5230,
            Channel_48 = 5240,
            Channel_50 = 5250,
            Channel_52 = 5260,
            Channel_54 = 5270,
            Channel_56 = 5280,
            Channel_58 = 5290,
            Channel_60 = 5300,
            Channel_62 = 5310,
            Channel_64 = 5320,
            Channel_100 = 5500,
            Channel_102 = 5510,
            Channel_104 = 5520,
            Channel_106 = 5530,
            Channel_108 = 5540,
            Channel_110 = 5550,
            Channel_112 = 5560,
            Channel_114 = 5570,
            Channel_116 = 5580,
            Channel_118 = 5590,
            Channel_120 = 5600,
            Channel_122 = 5610,
            Channel_124 = 5620,
            Channel_126 = 5630,
            Channel_128 = 5640,
            Channel_132 = 5660,
            Channel_134 = 5670,
            Channel_136 = 5680,
            Channel_138 = 5690,
            Channel_140 = 5700,
            Channel_142 = 5710,
            Channel_144 = 5720,
            Channel_149 = 5745,
            Channel_151 = 5755,
            Channel_153 = 5765,
            Channel_155 = 5775,
            Channel_157 = 5785,
            Channel_159 = 5795,
            Channel_161 = 5805,
            Channel_165 = 5825,
            Channel_169 = 5845,
            Channel_173 = 5865,
            Channel_183 = 4915,
            Channel_184 = 4920,
            Channel_185 = 4925,
            Channel_187 = 4935,
            Channel_188 = 4940,
            Channel_189 = 4945,
            Channel_192 = 4960,
            Channel_196 = 4980,
        }

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlansetprofile
        // 自行寫
        [Flags]
        public enum WlanProfileFlags {
            /// <remarks>
            /// The only option available on Windows XP SP2.
            /// </remarks>
            AllUser = 0,
            WLAN_PROFILE_GROUP_POLICY = 1,
            WLAN_PROFILE_USER = 2
        }

        // https://docs.microsoft.com/zh-tw/windows/desktop/NativeWiFi/wlan-reason-code
        // Managed Wifi Source code
        public enum WlanReasonCode {
            Success = 0,
            // general codes
            UNKNOWN = 0x10000 + 1,

            RANGE_SIZE = 0x10000,
            BASE = 0x10000 + RANGE_SIZE,

            // range for Auto Config
            //
            AC_BASE = 0x10000 + RANGE_SIZE,
            AC_CONNECT_BASE = (AC_BASE + RANGE_SIZE / 2),
            AC_END = (AC_BASE + RANGE_SIZE - 1),

            // range for profile manager
            // it has profile adding failure reason codes, but may not have 
            // connection reason codes
            //
            PROFILE_BASE = 0x10000 + (7 * RANGE_SIZE),
            PROFILE_CONNECT_BASE = (PROFILE_BASE + RANGE_SIZE / 2),
            PROFILE_END = (PROFILE_BASE + RANGE_SIZE - 1),

            // range for MSM
            //
            MSM_BASE = 0x10000 + (2 * RANGE_SIZE),
            MSM_CONNECT_BASE = (MSM_BASE + RANGE_SIZE / 2),
            MSM_END = (MSM_BASE + RANGE_SIZE - 1),

            // range for MSMSEC
            //
            MSMSEC_BASE = 0x10000 + (3 * RANGE_SIZE),
            MSMSEC_CONNECT_BASE = (MSMSEC_BASE + RANGE_SIZE / 2),
            MSMSEC_END = (MSMSEC_BASE + RANGE_SIZE - 1),

            // AC network incompatible reason codes
            //
            NETWORK_NOT_COMPATIBLE = (AC_BASE + 1),
            PROFILE_NOT_COMPATIBLE = (AC_BASE + 2),

            // AC connect reason code
            //
            NO_AUTO_CONNECTION = (AC_CONNECT_BASE + 1),
            NOT_VISIBLE = (AC_CONNECT_BASE + 2),
            GP_DENIED = (AC_CONNECT_BASE + 3),
            USER_DENIED = (AC_CONNECT_BASE + 4),
            BSS_TYPE_NOT_ALLOWED = (AC_CONNECT_BASE + 5),
            IN_FAILED_LIST = (AC_CONNECT_BASE + 6),
            IN_BLOCKED_LIST = (AC_CONNECT_BASE + 7),
            SSID_LIST_TOO_LONG = (AC_CONNECT_BASE + 8),
            CONNECT_CALL_FAIL = (AC_CONNECT_BASE + 9),
            SCAN_CALL_FAIL = (AC_CONNECT_BASE + 10),
            NETWORK_NOT_AVAILABLE = (AC_CONNECT_BASE + 11),
            PROFILE_CHANGED_OR_DELETED = (AC_CONNECT_BASE + 12),
            KEY_MISMATCH = (AC_CONNECT_BASE + 13),
            USER_NOT_RESPOND = (AC_CONNECT_BASE + 14),

            // Profile validation errors
            //
            INVALID_PROFILE_SCHEMA = (PROFILE_BASE + 1),
            PROFILE_MISSING = (PROFILE_BASE + 2),
            INVALID_PROFILE_NAME = (PROFILE_BASE + 3),
            INVALID_PROFILE_TYPE = (PROFILE_BASE + 4),
            INVALID_PHY_TYPE = (PROFILE_BASE + 5),
            MSM_SECURITY_MISSING = (PROFILE_BASE + 6),
            IHV_SECURITY_NOT_SUPPORTED = (PROFILE_BASE + 7),
            IHV_OUI_MISMATCH = (PROFILE_BASE + 8),
            // IHV OUI not present but there is IHV settings in profile
            IHV_OUI_MISSING = (PROFILE_BASE + 9),
            // IHV OUI is present but there is no IHV settings in profile
            IHV_SETTINGS_MISSING = (PROFILE_BASE + 10),
            // both/conflict MSMSec and IHV security settings exist in profile 
            CONFLICT_SECURITY = (PROFILE_BASE + 11),
            // no IHV or MSMSec security settings in profile
            SECURITY_MISSING = (PROFILE_BASE + 12),
            INVALID_BSS_TYPE = (PROFILE_BASE + 13),
            INVALID_ADHOC_CONNECTION_MODE = (PROFILE_BASE + 14),
            NON_BROADCAST_SET_FOR_ADHOC = (PROFILE_BASE + 15),
            AUTO_SWITCH_SET_FOR_ADHOC = (PROFILE_BASE + 16),
            AUTO_SWITCH_SET_FOR_MANUAL_CONNECTION = (PROFILE_BASE + 17),
            IHV_SECURITY_ONEX_MISSING = (PROFILE_BASE + 18),
            PROFILE_SSID_INVALID = (PROFILE_BASE + 19),
            TOO_MANY_SSID = (PROFILE_BASE + 20),

            // MSM network incompatible reasons
            //
            UNSUPPORTED_SECURITY_SET_BY_OS = (MSM_BASE + 1),
            UNSUPPORTED_SECURITY_SET = (MSM_BASE + 2),
            BSS_TYPE_UNMATCH = (MSM_BASE + 3),
            PHY_TYPE_UNMATCH = (MSM_BASE + 4),
            DATARATE_UNMATCH = (MSM_BASE + 5),

            // MSM connection failure reasons, to be defined
            // failure reason codes
            //
            // user called to disconnect
            USER_CANCELLED = (MSM_CONNECT_BASE + 1),
            // got disconnect while associating
            ASSOCIATION_FAILURE = (MSM_CONNECT_BASE + 2),
            // timeout for association
            ASSOCIATION_TIMEOUT = (MSM_CONNECT_BASE + 3),
            // pre-association security completed with failure
            PRE_SECURITY_FAILURE = (MSM_CONNECT_BASE + 4),
            // fail to start post-association security
            START_SECURITY_FAILURE = (MSM_CONNECT_BASE + 5),
            // post-association security completed with failure
            SECURITY_FAILURE = (MSM_CONNECT_BASE + 6),
            // security watchdog timeout
            SECURITY_TIMEOUT = (MSM_CONNECT_BASE + 7),
            // got disconnect from driver when roaming
            ROAMING_FAILURE = (MSM_CONNECT_BASE + 8),
            // failed to start security for roaming
            ROAMING_SECURITY_FAILURE = (MSM_CONNECT_BASE + 9),
            // failed to start security for adhoc-join
            ADHOC_SECURITY_FAILURE = (MSM_CONNECT_BASE + 10),
            // got disconnection from driver
            DRIVER_DISCONNECTED = (MSM_CONNECT_BASE + 11),
            // driver operation failed
            DRIVER_OPERATION_FAILURE = (MSM_CONNECT_BASE + 12),
            // Ihv service is not available
            IHV_NOT_AVAILABLE = (MSM_CONNECT_BASE + 13),
            // Response from ihv timed out
            IHV_NOT_RESPONDING = (MSM_CONNECT_BASE + 14),
            // Timed out waiting for driver to disconnect
            DISCONNECT_TIMEOUT = (MSM_CONNECT_BASE + 15),
            // An internal error prevented the operation from being completed.
            INTERNAL_FAILURE = (MSM_CONNECT_BASE + 16),
            // UI Request timed out.
            UI_REQUEST_TIMEOUT = (MSM_CONNECT_BASE + 17),
            // Roaming too often, post security is not completed after 5 times.
            TOO_MANY_SECURITY_ATTEMPTS = (MSM_CONNECT_BASE + 18),

            // MSMSEC reason codes
            //

            MSMSEC_MIN = MSMSEC_BASE,

            // Key index specified is not valid
            MSMSEC_PROFILE_INVALID_KEY_INDEX = (MSMSEC_BASE + 1),
            // Key required, PSK present
            MSMSEC_PROFILE_PSK_PRESENT = (MSMSEC_BASE + 2),
            // Invalid key length
            MSMSEC_PROFILE_KEY_LENGTH = (MSMSEC_BASE + 3),
            // Invalid PSK length
            MSMSEC_PROFILE_PSK_LENGTH = (MSMSEC_BASE + 4),
            // No auth/cipher specified
            MSMSEC_PROFILE_NO_AUTH_CIPHER_SPECIFIED = (MSMSEC_BASE + 5),
            // Too many auth/cipher specified
            MSMSEC_PROFILE_TOO_MANY_AUTH_CIPHER_SPECIFIED = (MSMSEC_BASE + 6),
            // Profile contains duplicate auth/cipher
            MSMSEC_PROFILE_DUPLICATE_AUTH_CIPHER = (MSMSEC_BASE + 7),
            // Profile raw data is invalid (1x or key data)
            MSMSEC_PROFILE_RAWDATA_INVALID = (MSMSEC_BASE + 8),
            // Invalid auth/cipher combination
            MSMSEC_PROFILE_INVALID_AUTH_CIPHER = (MSMSEC_BASE + 9),
            // 802.1x disabled when it's required to be enabled
            MSMSEC_PROFILE_ONEX_DISABLED = (MSMSEC_BASE + 10),
            // 802.1x enabled when it's required to be disabled
            MSMSEC_PROFILE_ONEX_ENABLED = (MSMSEC_BASE + 11),
            MSMSEC_PROFILE_INVALID_PMKCACHE_MODE = (MSMSEC_BASE + 12),
            MSMSEC_PROFILE_INVALID_PMKCACHE_SIZE = (MSMSEC_BASE + 13),
            MSMSEC_PROFILE_INVALID_PMKCACHE_TTL = (MSMSEC_BASE + 14),
            MSMSEC_PROFILE_INVALID_PREAUTH_MODE = (MSMSEC_BASE + 15),
            MSMSEC_PROFILE_INVALID_PREAUTH_THROTTLE = (MSMSEC_BASE + 16),
            // PreAuth enabled when PMK cache is disabled
            MSMSEC_PROFILE_PREAUTH_ONLY_ENABLED = (MSMSEC_BASE + 17),
            // Capability matching failed at network
            MSMSEC_CAPABILITY_NETWORK = (MSMSEC_BASE + 18),
            // Capability matching failed at NIC
            MSMSEC_CAPABILITY_NIC = (MSMSEC_BASE + 19),
            // Capability matching failed at profile
            MSMSEC_CAPABILITY_PROFILE = (MSMSEC_BASE + 20),
            // Network does not support specified discovery type
            MSMSEC_CAPABILITY_DISCOVERY = (MSMSEC_BASE + 21),
            // Passphrase contains invalid character
            MSMSEC_PROFILE_PASSPHRASE_CHAR = (MSMSEC_BASE + 22),
            // Key material contains invalid character
            MSMSEC_PROFILE_KEYMATERIAL_CHAR = (MSMSEC_BASE + 23),
            // Wrong key type specified for the auth/cipher pair
            MSMSEC_PROFILE_WRONG_KEYTYPE = (MSMSEC_BASE + 24),
            // "Mixed cell" suspected (AP not beaconing privacy, we have privacy enabled profile)
            MSMSEC_MIXED_CELL = (MSMSEC_BASE + 25),
            // Auth timers or number of timeouts in profile is incorrect
            MSMSEC_PROFILE_AUTH_TIMERS_INVALID = (MSMSEC_BASE + 26),
            // Group key update interval in profile is incorrect
            MSMSEC_PROFILE_INVALID_GKEY_INTV = (MSMSEC_BASE + 27),
            // "Transition network" suspected, trying legacy 802.11 security
            MSMSEC_TRANSITION_NETWORK = (MSMSEC_BASE + 28),
            // Key contains characters which do not map to ASCII
            MSMSEC_PROFILE_KEY_UNMAPPED_CHAR = (MSMSEC_BASE + 29),
            // Capability matching failed at profile (auth not found)
            MSMSEC_CAPABILITY_PROFILE_AUTH = (MSMSEC_BASE + 30),
            // Capability matching failed at profile (cipher not found)
            MSMSEC_CAPABILITY_PROFILE_CIPHER = (MSMSEC_BASE + 31),

            // Failed to queue UI request
            MSMSEC_UI_REQUEST_FAILURE = (MSMSEC_CONNECT_BASE + 1),
            // 802.1x authentication did not start within configured time 
            MSMSEC_AUTH_START_TIMEOUT = (MSMSEC_CONNECT_BASE + 2),
            // 802.1x authentication did not complete within configured time
            MSMSEC_AUTH_SUCCESS_TIMEOUT = (MSMSEC_CONNECT_BASE + 3),
            // Dynamic key exchange did not start within configured time
            MSMSEC_KEY_START_TIMEOUT = (MSMSEC_CONNECT_BASE + 4),
            // Dynamic key exchange did not succeed within configured time
            MSMSEC_KEY_SUCCESS_TIMEOUT = (MSMSEC_CONNECT_BASE + 5),
            // Message 3 of 4 way handshake has no key data (RSN/WPA)
            MSMSEC_M3_MISSING_KEY_DATA = (MSMSEC_CONNECT_BASE + 6),
            // Message 3 of 4 way handshake has no IE (RSN/WPA)
            MSMSEC_M3_MISSING_IE = (MSMSEC_CONNECT_BASE + 7),
            // Message 3 of 4 way handshake has no Group Key (RSN)
            MSMSEC_M3_MISSING_GRP_KEY = (MSMSEC_CONNECT_BASE + 8),
            // Matching security capabilities of IE in M3 failed (RSN/WPA)
            MSMSEC_PR_IE_MATCHING = (MSMSEC_CONNECT_BASE + 9),
            // Matching security capabilities of Secondary IE in M3 failed (RSN)
            MSMSEC_SEC_IE_MATCHING = (MSMSEC_CONNECT_BASE + 10),
            // Required a pairwise key but AP configured only group keys
            MSMSEC_NO_PAIRWISE_KEY = (MSMSEC_CONNECT_BASE + 11),
            // Message 1 of group key handshake has no key data (RSN/WPA)
            MSMSEC_G1_MISSING_KEY_DATA = (MSMSEC_CONNECT_BASE + 12),
            // Message 1 of group key handshake has no group key
            MSMSEC_G1_MISSING_GRP_KEY = (MSMSEC_CONNECT_BASE + 13),
            // AP reset secure bit after connection was secured
            MSMSEC_PEER_INDICATED_INSECURE = (MSMSEC_CONNECT_BASE + 14),
            // 802.1x indicated there is no authenticator but profile requires 802.1x
            MSMSEC_NO_AUTHENTICATOR = (MSMSEC_CONNECT_BASE + 15),
            // Plumbing settings to NIC failed
            MSMSEC_NIC_FAILURE = (MSMSEC_CONNECT_BASE + 16),
            // Operation was cancelled by caller
            MSMSEC_CANCELLED = (MSMSEC_CONNECT_BASE + 17),
            // Key was in incorrect format 
            MSMSEC_KEY_FORMAT = (MSMSEC_CONNECT_BASE + 18),
            // Security downgrade detected
            MSMSEC_DOWNGRADE_DETECTED = (MSMSEC_CONNECT_BASE + 19),
            // PSK mismatch suspected
            MSMSEC_PSK_MISMATCH_SUSPECTED = (MSMSEC_CONNECT_BASE + 20),
            // Forced failure because connection method was not secure
            MSMSEC_FORCED_FAILURE = (MSMSEC_CONNECT_BASE + 21),
            // ui request couldn't be queued or user pressed cancel
            MSMSEC_SECURITY_UI_FAILURE = (MSMSEC_CONNECT_BASE + 22),

            MSMSEC_MAX = MSMSEC_END
        }

        #endregion

        #region Struct

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/ns-wlanapi-_wlan_interface_info_list
        // https://www.pinvoke.net/default.aspx/Structures/WLAN_INTERFACE_INFO_LIST.html
        [StructLayout (LayoutKind.Sequential)]
        public class WLAN_INTERFACE_INFO_LIST {
            public int dwNumberOfItems;
            public int dwIndex;
            public WLAN_INTERFACE_INFO[] InterfaceInfo;
        }

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms706902(v=vs.85).aspx 
        // https://www.pinvoke.net/default.aspx/Structures/WLAN_NOTIFICATION_DATA.html
        [StructLayout (LayoutKind.Sequential)]
        public struct WLAN_NOTIFICATION_DATA {
            public WLAN_NOTIFICATION_SOURCE notificationSource;
            public int notificationCode;
            public Guid interfaceGuid;
            public int dataSize;
            public IntPtr dataPtr;

            public object NotificationCode {
                get {
                    if (notificationSource == WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_MSM)
                        return (WLAN_NOTIFICATION_CODE_MSM) notificationCode;
                    else if (notificationSource == WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_ACM)
                        return (WLAN_NOTIFICATION_CODE_ACM) notificationCode;
                    else
                        return notificationCode;
                }
            }
        }

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/ns-wlanapi-_wlan_available_network_list
        // https://www.pinvoke.net/default.aspx/Structures.WLAN_AVAILABLE_NETWORK_LIST
        [StructLayout (LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WLAN_AVAILABLE_NETWORK_LIST {
            public uint dwNumberOfItems;
            public uint dwIndex;
            public WLAN_AVAILABLE_NETWORK[] wlanAvailableNetwork;

            public WLAN_AVAILABLE_NETWORK_LIST (IntPtr ppAvailableNetworkList) {
                dwNumberOfItems = (uint) Marshal.ReadInt32 (ppAvailableNetworkList);
                dwIndex = (uint) Marshal.ReadInt32 (ppAvailableNetworkList, 4);
                wlanAvailableNetwork = new WLAN_AVAILABLE_NETWORK[dwNumberOfItems];

                for (int i = 0; i < dwNumberOfItems; i++) {
                    IntPtr pWlanAvailableNetwork = new IntPtr (ppAvailableNetworkList.ToInt32 () + i * Marshal.SizeOf (typeof (WLAN_AVAILABLE_NETWORK)) + 8);
                    wlanAvailableNetwork[i] = (WLAN_AVAILABLE_NETWORK) Marshal.PtrToStructure (pWlanAvailableNetwork, typeof (WLAN_AVAILABLE_NETWORK));
                }
                WlanFreeMemory (ppAvailableNetworkList);
            }
        }

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/ns-wlanapi-_wlan_available_network
        // https://www.pinvoke.net/default.aspx/Structures.WLAN_AVAILABLE_NETWORK
        [StructLayout (LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WLAN_AVAILABLE_NETWORK {
            #region 原屬性

            [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 256)]
            public string strProfileName;
            public DOT11_SSID dot11Ssid;
            public DOT11_BSS_TYPE dot11BssType;
            public uint uNumberOfBssids;
            public bool bNetworkConnectable;
            public uint wlanNotConnectableReason;
            public uint uNumberOfPhyTypes;
            [MarshalAs (UnmanagedType.ByValArray, SizeConst = 8)]
            public DOT11_PHY_TYPE[] dot11PhyTypes;
            public bool bMorePhyTypes;
            public uint wlanSignalQuality;
            public bool bSecurityEnabled;
            public DOT11_AUTH_ALGORITHM dot11DefaultAuthAlgorithm;
            public DOT11_CIPHER_ALGORITHM dot11DefaultCipherAlgorithm;
            public uint dwFlags;
            public uint dwReserved;

            #endregion

            public string SSID => dot11Ssid.ucSSID;

            public string Radio {
                get {
                    switch (dot11PhyTypes[0]) {
                        case DOT11_PHY_TYPE.dot11_phy_type_ofdm:
                            return "802.11a";
                        case DOT11_PHY_TYPE.dot11_phy_type_hrdsss:
                            return "802.11b";
                        case DOT11_PHY_TYPE.dot11_phy_type_erp:
                            return "802.11g";
                        case DOT11_PHY_TYPE.dot11_phy_type_ht:
                            return "802.11n";
                        case DOT11_PHY_TYPE.dot11_phy_type_vht:
                            return "802.11ac";
                        default:
                            return dot11PhyTypes[0].ToString ();
                    }
                }
            }

            public string Network_Type => dot11BssType.ToString ().Substring ("dot11_BSS_type_".Length);

            public string Authentication {
                get {
                    switch (dot11DefaultAuthAlgorithm) {
                        case DOT11_AUTH_ALGORITHM.DOT11_AUTH_ALGO_80211_Open:
                            return "Open";
                        case DOT11_AUTH_ALGORITHM.DOT11_AUTH_ALGO_WPA_PSK:
                            return "WPA-Personal";
                        case DOT11_AUTH_ALGORITHM.DOT11_AUTH_ALGO_RSNA_PSK:
                            return "WPA2-Personal";
                        default:
                            return dot11DefaultAuthAlgorithm.ToString ();
                    }
                }
            }

            public string Encryption => dot11DefaultCipherAlgorithm.ToString ().Substring ("DOT11_CIPHER_ALGO_".Length);

            public string Signal => wlanSignalQuality.ToString () + "%";
        }

        // https://docs.microsoft.com/zh-tw/windows/desktop/api/wlanapi/ns-wlanapi-_wlan_bss_list
        // https://stackoverflow.com/questions/6332359/find-the-total-size-of-an-array-of-objects-with-only-a-reference-pointer
        [StructLayout (LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WLAN_BSS_LIST {
            public uint dwTotalSize;
            public uint dwNumberOfItems;
            public WLAN_BSS_ENTRY[] wlanBssEntries;

            public WLAN_BSS_LIST (IntPtr ppBssList) {
                dwTotalSize = (uint) Marshal.ReadInt32 (ppBssList);
                dwNumberOfItems = (uint) Marshal.ReadInt32 (ppBssList, 4);
                wlanBssEntries = new WLAN_BSS_ENTRY[dwNumberOfItems];

                for (int i = 0; i < dwNumberOfItems; i++) {
                    IntPtr pWlanBssEntry = new IntPtr (ppBssList.ToInt32 () + i * Marshal.SizeOf (typeof (WLAN_BSS_ENTRY)) + 8);
                    wlanBssEntries[i] = (WLAN_BSS_ENTRY) Marshal.PtrToStructure (pWlanBssEntry, typeof (WLAN_BSS_ENTRY));
                }
                WlanFreeMemory (ppBssList);
            }
        }

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/ns-wlanapi-_wlan_bss_entry
        // https://stackoverflow.com/questions/39564080/wlangetnetworkbsslist-with-c-sharp-on-windows-10
        public struct WLAN_BSS_ENTRY {
            #region 原屬性
            public DOT11_SSID dot11Ssid;
            public uint uPhyId;
            [MarshalAs (UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] dot11Bssid;
            public DOT11_BSS_TYPE dot11BssType;
            public DOT11_PHY_TYPE dot11BssPhyType;
            public int lRssi;
            public uint uLinkQuality;
            public bool bInRegDomain;
            public UInt16 usBeaconPeriod;
            public UInt64 ullTimestamp;
            public UInt64 ullHostTimestamp;
            public UInt16 usCapabilityInformation;
            public uint ulChCenterFrequency;
            public WLAN_RATE_SET wlanRateSet;
            public uint ulIeOffset;
            public uint ulIeSize;
            #endregion

            public string MAC {
                get {
                    string TempMac = "";
                    for (int i = 0; i < dot11Bssid.Length; i++)
                        TempMac += dot11Bssid[i].ToString ("x2").PadLeft (2, '0'); // 串接Mac
                    for (int i = 2; i < TempMac.Length; i += 3)
                        TempMac = TempMac.Insert (i, ":"); // 插入Mac的分隔符號

                    return TempMac;
                }
            }

            public string Channel => ((ChannelFrequency_enum) (ulChCenterFrequency / 1000)).ToString ().Substring ("Channel_".Length);

            public string Speed {
                get {
                    double MaxSpeed = 0;
                    for (int i = 0; i < wlanRateSet.rateSetLength; i++)
                        if (wlanRateSet.rateSet[i] != 0)
                            MaxSpeed = Math.Max (MaxSpeed, wlanRateSet.GetRateInMbps (i));

                    return MaxSpeed.ToString ();
                }
            }
        }

        // https://docs.microsoft.com/zh-tw/windows/desktop/api/wlanapi/ns-wlanapi-_wlan_profile_info_list
        // https://www.pinvoke.net/default.aspx/Structures.WLAN_PROFILE_INFO_LIST
        public struct WLAN_PROFILE_INFO_LIST {
            public uint dwNumberOfItems;
            public uint dwIndex;
            public WLAN_PROFILE_INFO[] ProfileInfo;

            public WLAN_PROFILE_INFO_LIST (IntPtr ppProfileList) {
                dwNumberOfItems = (uint) Marshal.ReadInt32 (ppProfileList);
                dwIndex = (uint) Marshal.ReadInt32 (ppProfileList, 4);
                ProfileInfo = new WLAN_PROFILE_INFO[dwNumberOfItems];

                for (int i = 0; i < dwNumberOfItems; i++) {
                    IntPtr pProfileList = new IntPtr (ppProfileList.ToInt32 () + i * Marshal.SizeOf (typeof (WLAN_PROFILE_INFO)) + 8);
                    ProfileInfo[i] = (WLAN_PROFILE_INFO) Marshal.PtrToStructure (pProfileList, typeof (WLAN_PROFILE_INFO));
                }
                WlanFreeMemory (ppProfileList);
            }
        }

        // https://www.pinvoke.net/default.aspx/Structures.WLAN_PROFILE_INFO
        // https://docs.microsoft.com/zh-tw/windows/desktop/api/wlanapi/ns-wlanapi-_wlan_profile_info
        [StructLayout (LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WLAN_PROFILE_INFO {
            [MarshalAs (UnmanagedType.ByValArray, SizeConst = 256)]
            public char[] strInterfaceDescription;
            public int dwFlags;

            public string Description {
                get {
                    string temp = "";

                    for (int i = 0; i < strInterfaceDescription.Length; i++) {
                        if (Convert.ToInt32 (strInterfaceDescription[i]) != 0)
                            temp += strInterfaceDescription[i];
                    }
                    return temp;
                }

            }
        }

        // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/wlantypes/ns-wlantypes-_dot11_ssid
        // https://www.pinvoke.net/default.aspx/Structures/DOT11_SSID.html
        [StructLayout (LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DOT11_SSID {
            public uint uSSIDLength;
            [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 32)]
            public string ucSSID;
        }

        // https://docs.microsoft.com/zh-tw/windows/desktop/api/wlanapi/ns-wlanapi-_wlan_rate_set
        // https://stackoverflow.com/questions/39564080/wlangetnetworkbsslist-with-c-sharp-on-windows-10
        [StructLayout (LayoutKind.Sequential)]
        public struct WLAN_RATE_SET {
            public uint rateSetLength;
            [MarshalAs (UnmanagedType.ByValArray, SizeConst = 126)]
            public ushort[] rateSet;

            public double GetRateInMbps (int rate) => (rateSet[rate] & 0x7FFF) * 0.5;
        }

        // 因為單個熱點資訊是存在兩個不同Struct 所以用一個新的Struct包裝起來
        public struct Wlan_Hotpot {
            public WLAN_AVAILABLE_NETWORK SelectAvailableNetwork;
            public WLAN_BSS_ENTRY SelectBssEnreyNetwork;

            #region 熱點資訊
            public string SSID => SelectAvailableNetwork.dot11Ssid.ucSSID;

            public string Radio {
                get {
                    switch (SelectAvailableNetwork.dot11PhyTypes[0]) {
                        case DOT11_PHY_TYPE.dot11_phy_type_ofdm:
                            return "802.11a";
                        case DOT11_PHY_TYPE.dot11_phy_type_hrdsss:
                            return "802.11b";
                        case DOT11_PHY_TYPE.dot11_phy_type_erp:
                            return "802.11g";
                        case DOT11_PHY_TYPE.dot11_phy_type_ht:
                            return "802.11n";
                        case DOT11_PHY_TYPE.dot11_phy_type_vht:
                            return "802.11ac";
                        default:
                            return SelectAvailableNetwork.dot11PhyTypes[0].ToString ();
                    }
                }
            }

            public string Network_Type => SelectAvailableNetwork.dot11BssType.ToString ().Substring ("dot11_BSS_type_".Length);

            public string Authentication {
                get {
                    switch (SelectAvailableNetwork.dot11DefaultAuthAlgorithm) {
                        case DOT11_AUTH_ALGORITHM.DOT11_AUTH_ALGO_80211_Open:
                            return "Open";
                        case DOT11_AUTH_ALGORITHM.DOT11_AUTH_ALGO_WPA_PSK:
                            return "WPA-Personal";
                        case DOT11_AUTH_ALGORITHM.DOT11_AUTH_ALGO_RSNA_PSK:
                            return "WPA2-Personal";
                        default:
                            return SelectAvailableNetwork.dot11DefaultAuthAlgorithm.ToString ();
                    }
                }
            }

            public string Encryption => SelectAvailableNetwork.dot11DefaultCipherAlgorithm.ToString ().Substring ("DOT11_CIPHER_ALGO_".Length);

            public string Signal => SelectAvailableNetwork.wlanSignalQuality.ToString () + "%";

            public string MAC {
                get {
                    string TempMac = "";
                    for (int i = 0; i < SelectBssEnreyNetwork.dot11Bssid.Length; i++)
                        TempMac += SelectBssEnreyNetwork.dot11Bssid[i].ToString ("x2").PadLeft (2, '0'); // 串接Mac
                    for (int i = 2; i < TempMac.Length; i += 3)
                        TempMac = TempMac.Insert (i, ":"); // 插入Mac的分隔符號

                    return TempMac;
                }
            }

            public string Channel => ((ChannelFrequency_enum) (SelectBssEnreyNetwork.ulChCenterFrequency / 1000)).ToString ().Substring ("Channel_".Length);

            public string Speed {
                get {
                    double MaxSpeed = 0;
                    for (int i = 0; i < SelectBssEnreyNetwork.wlanRateSet.rateSetLength; i++)
                        if (SelectBssEnreyNetwork.wlanRateSet.rateSet[i] != 0)
                            MaxSpeed = Math.Max (MaxSpeed, SelectBssEnreyNetwork.wlanRateSet.GetRateInMbps (i));

                    return MaxSpeed.ToString ();
                }
            }

            #endregion

            public Wlan_Hotpot (WLAN_AVAILABLE_NETWORK SelectAvailableNetwork, WLAN_BSS_ENTRY SelectBssEnreyNetwork) {
                this.SelectAvailableNetwork = SelectAvailableNetwork;
                this.SelectBssEnreyNetwork = SelectBssEnreyNetwork;
            }
        }

        #endregion

        #region Function API Dll import 

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlanopenhandle
        // https://www.pinvoke.net/default.aspx/wlanapi.WlanOpenHandle
        [DllImport ("Wlanapi.dll")]
        internal static extern int WlanOpenHandle (
            uint dwClientVersion,
            IntPtr pReserved, //not in MSDN but required
            [Out] out uint pdwNegotiatedVersion,
            out IntPtr ClientHandle);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlanclosehandle
        // https://www.pinvoke.net/default.aspx/wlanapi.WlanCloseHandle
        [DllImport ("Wlanapi.dll")]
        internal static extern uint WlanCloseHandle (
            [In] IntPtr hClientHandle,
            IntPtr pReserved);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlanenuminterfaces
        // https://stackoverflow.com/questions/18557244/wlanenuminterfaces-dont-return-guid-c-sharp-whit-p-invoke
        [DllImport ("Wlanapi.dll")]
        internal static extern int WlanEnumInterfaces (
            [In] IntPtr HandleClient, [In, Out] IntPtr Reserved, [In, Out] WLAN_INTERFACE_INFO_LIST ine);

        // https://docs.microsoft.com/zh-tw/windows/desktop/api/wlanapi/nf-wlanapi-wlanregisternotification
        // https://github.com/metageek-llc/ManagedWifi/blob/master/Wlan.cs
        [DllImport ("Wlanapi.dll")]
        internal static extern int WlanRegisterNotification (
            [In] IntPtr clientHandle, [In] WLAN_NOTIFICATION_SOURCE notifSource, [In] bool ignoreDuplicate, [In] WlanNotificationCallbackDelegate funcCallback, [In] IntPtr callbackContext, [In] IntPtr reserved, [Out] out WLAN_NOTIFICATION_SOURCE prevNotifSource);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlangetavailablenetworklist
        // https://www.pinvoke.net/default.aspx/wlanapi.wlangetavailablenetworklist
        [DllImport ("Wlanapi.dll", SetLastError = true)]
        public static extern int WlanGetAvailableNetworkList (
            [In] IntPtr clientHandle, [In, MarshalAs (UnmanagedType.LPStruct)] Guid interfaceGuid, [In] WlanGetAvailableNetworkListFlags flags, [In, Out] IntPtr reservedPtr, [Out] out IntPtr availableNetworkListPtr);

        // https://docs.microsoft.com/zh-tw/windows/desktop/api/wlanapi/nf-wlanapi-wlangetnetworkbsslist
        // 自行寫
        [DllImport ("Wlanapi.dll")]
        public static extern int WlanGetNetworkBssList (
            [In] IntPtr clientHandle, [In, MarshalAs (UnmanagedType.LPStruct)] Guid interfaceGuid, [In] IntPtr dot11SsidInt, [In] DOT11_BSS_TYPE dot11BssType, [In] bool securityEnabled,
            IntPtr reservedPtr, [Out] out IntPtr wlanBssList);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlanfreememory
        // https://www.pinvoke.net/default.aspx/wlanapi.WlanFreeMemory
        [DllImport ("Wlanapi.dll")]
        public static extern void WlanFreeMemory ([In] IntPtr pMemory);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlangetprofilelist
        // https://www.pinvoke.net/default.aspx/wlanapi.wlangetprofilelist
        [DllImport ("wlanapi.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern uint WlanGetProfileList (
            [In] IntPtr clientHandle, [In, MarshalAs (UnmanagedType.LPStruct)] Guid interfaceGuid, [In] IntPtr pReserved, [Out] out IntPtr profileList);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlandeleteprofile
        // https://www.pinvoke.net/default.aspx/wlanapi.wlandeleteprofile
        [DllImport ("Wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WlanDeleteProfile (
            [In] IntPtr hClientHandle, [In, MarshalAs (UnmanagedType.LPStruct)] Guid pInterfaceGuid, [In, MarshalAs (UnmanagedType.LPWStr)] string strProfileName,
            IntPtr pReserved);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wlanapi/nf-wlanapi-wlansetprofile
        // https://www.pinvoke.net/default.aspx/wlanapi.WlanSetProfile
        [DllImport ("wlanapi.dll")]
        public static extern int WlanSetProfile (
            [In] IntPtr clientHandle, [In, MarshalAs (UnmanagedType.LPStruct)] Guid interfaceGuid, [In] WlanProfileFlags flags, [In, MarshalAs (UnmanagedType.LPWStr)] string profileXml, [In, Optional, MarshalAs (UnmanagedType.LPWStr)] string allUserProfileSecurity, [In] bool overwrite, [In] IntPtr pReserved, [Out] out WlanReasonCode reasonCode);

        #endregion
    }
}