<?php

    $requestCategory = str_replace("'","",$param["categoy"]);
    $explodeSplit = $explode = explode("|",$requestCategory);



    foreach($explode as $value){


        $categorypop = implode(",",$explodeSplit);
        $categorypop = str_replace(",","|",$categorypop);
        $pos = strrpos($categorypop, "|");
        if ($pos === false){
            break;
        }


          $query = "select count(*) as cnt from mumbra  where categoy like '".$categorypop."%';";
         $result = mysql_query($query);
         $row = mysql_fetch_assoc($result);

        $name = array_pop($explodeSplit);
        $categorypop = implode(",",$explodeSplit);
        $categorypop = str_replace(",","|",$categorypop);

        $query = "update mumbra set entry = '".$row["cnt"]."'  where Name = '" . $name . "' and categoy = '".$categorypop."';";
         $result = mysql_query($query);
        echo  $query .= "#_#";



    }

?>