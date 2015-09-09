<?php
    $notificationContentTitle = "Locally: Discount in Cloth Shop";
    $notificationContentText = "50% Show this couple get 200Rs discount";
    $notificationContentTicker = "Locally Super Discount in Cloth Shop";
    $notificationContentScreen = "MainPage";
    $notificationContentId = "200";

    if ($_REQUEST['mobile'] == "3233004756")
        $GPSTrackingMode = "yes";
    else
        $GPSTrackingMode = "no";

    echo "1500" . "#_#" . $notificationContentTitle . "#_#" . $notificationContentText . "#_#" . $notificationContentTicker . "#_#" . $notificationContentScreen .  "#_#" . $notificationContentId .  "#_#" . $GPSTrackingMode ;// First try
         //"SEC"

         //Index = 6 is GPS is text is pass then app will update gps informaation in the database every interval
    //Execute only below geo line when gps lat lng changed
    if ($_REQUEST['geolat'] != "" || $_REQUEST['geolng'] != ""){
        include_once("gps.php");
    }

?>