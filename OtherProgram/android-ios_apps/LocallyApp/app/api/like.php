<?php
    include_once("db.php");
    include_once("param.php");

     $time = date("Y-m-d h:i:s");
    echo  $query = "update mumbra set sublevel =  sublevel + 1, json = '" . $time ."'  where id_web = " . $_REQUEST["id_web"];
     $result = mysql_query($query);

?>