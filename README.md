# WiFi ShowNetwork：
WiFi API，此工具為前公司當時需要時做WiFi項目的檢測，但是Windows的WiFi Command Api 會因為語系而產生亂碼，而網路上其他人的API又經常有問題，機率性發生在特定裝置。所以離職後痛定思痛，就決定自己寫了個操作WiFi的API工具，該工具是直接操作Windows的API，必須查表、各函數的16進制、處理C++與C#溝通的相異等等，以此手段操作Wifi，並封裝成dll檔案供他人使用。

1. 可以偵測該電腦的所有無線網卡、並選擇該網卡進行操作
![](https://i.imgur.com/OBw7hS6.png)

2. 可以偵測使用該網卡下，周圍所有WiFi熱點的詳細資訊
![](https://i.imgur.com/PnOw22l.png)

3. 可以使用該網卡對某熱點進行連線，並且有 重新輸入密碼和使用該電腦的記憶密碼功能 來進行連線憑證。
