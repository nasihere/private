<?php

    include_once("db.php");
    $p = $_REQUEST["param"];


   echo $query = "update tbl_app_registration " .
                " set  geoprivacy = ".$_REQUEST['geoprivacy']. " " .
         " where session_id  = '".$_REQUEST['mobile']."'";


    $result = mysql_query($query);




?>