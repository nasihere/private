<?php
    include_once("db.php");
 //   include_once("category.php");
   // include_once("param.php");
    $p = $_REQUEST["param"];
    $p = explode("#_#", $p);


    if ($_REQUEST["edit"] == "true"){
        $param = update($p,"mumbra");
    }
    else{
        $param = insert($p,"mumbra");
    }

      include_once("entry.php");
    //I need to check if category is mumbra then dont add any date
    //If other then category is mumbra then add Date in desending order

?>