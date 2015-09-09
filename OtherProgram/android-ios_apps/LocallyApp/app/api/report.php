<?php
    include_once("db.php");
    include_once("param.php");


      $query = "update  mumbra  set report = report + 1, reported = concat(reported, '".$_REQUEST["session_id"].",') where (categoy = '" . $_REQUEST["categoy"] . "' and Name = '" . $_REQUEST["Name"] . "') or id_web = " . $_REQUEST["id_web"];
      $result = mysql_query($query);

?>