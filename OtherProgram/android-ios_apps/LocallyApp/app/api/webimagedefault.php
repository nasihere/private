<?php
    include_once("db.php");



       echo $update = "update mumbra set selected = replace(selected,'".$_REQUEST["pictureid"]."','".$_REQUEST["pictureid"]."_set') where id_web = " . $_REQUEST["id_web"];
        mysql_query($update);
     //   header("Location: index.php");
        echo "image replace!";
  header("location:".$_SERVER['HTTP_REFERER']);
?>