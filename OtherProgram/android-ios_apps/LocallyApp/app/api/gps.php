<?php

    include_once("db.php");
    $p = $_REQUEST["param"];


    if ($_REQUEST['mobile'] == "0") $_REQUEST['mobile'] = "3233004756";


    $query = "update tbl_app_registration " .
                " set geolat = " . $_REQUEST["geolat"].", " .
                " geodatetime = NOW(), " .
                " geolng = ".$_REQUEST['geolng']." " .
         " where session_id  = '".$_REQUEST['mobile']."'";


    $result = mysql_query($query);




?>