# 2018/06/27 Wifi ShowNetwork version：0.0.0.1
1. WLAN 的 Interfaces 建置
2. WLAN 的 Interfaces 連線異動通知
3. 修改元WLAN的Struct結構
4. 判斷當前使用者系統是否支援本API

# 2018/06/26 Wifi ShowNetwork version：0.0.0.1
1. 提供Wifi熱點的基本訊息(根據CMD network show network 指令)
2. 提供Wifi熱點的類別，使使用者可以對其進行連線
3. 根據熱點屬性可提供相對應的Profile
4. 判斷當前熱點是否早已有Profile，並使用舊Profile進行連線
5. Demo使用讓使用者能夠清楚API的使用流程