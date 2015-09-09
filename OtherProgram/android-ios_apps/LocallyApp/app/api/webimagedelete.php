<?php
    include_once("db.php");



        $update = "update mumbra set selected = replace(selected,'".$_REQUEST['pictureid'].",','') where id_web = " . $_REQUEST["id_web"];
        mysql_query($update);
     //   header("Location: index.php");
        echo "image deleted!";
  header("location:".$_SERVER['HTTP_REFERER']);
?>