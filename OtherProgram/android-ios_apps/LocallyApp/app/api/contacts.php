<?php

    include_once("db.php");
    $p = $_REQUEST["param"];


    $explode = explode("#_#",$_REQUEST['contactinfo']);

    if ($_REQUEST["mobile"] != ""){
     foreach($explode as $value){
                    $NamePhone = explode("#-#",$value);
                    if ($value != ""){
                      // Check Phone No and NOT in the database by same USER(REQUEST[MOBILE])
                      $query = "select _id,mobile from contacts  where phone = '".$NamePhone[1]."';";
                      $result = mysql_query($query);
                      $row = mysql_fetch_assoc($result);
                      if ($row['_id']){

                           //Check the user REQUESTMobile number is there in the mobile list? if not then do UPDATE qry if yes then skip dont do anything

                            if (strrpos($row['mobile'],$_REQUEST['mobile']) === false){ // not matched same number then update
                                 $query = "update contacts set " .
                                                " lastdatetime = NOW(), ".
                                                " mobile = concat(mobile, '".$_REQUEST['mobile']."#_#'), ".
                                                " name = concat(name,'".$NamePhone[0]."#_#')  " .
                                        " where _id = ".$row["_id"].";";
                           }
                           //Else if mobile no matched then don't do anything..

                      }
                      else{

                            // for some reason if number not found then create new entry of that number
                         $query = "insert into contacts (_id,datetime, mobile, name, phone) values " .
                                      "(NULL,NOW(),'".$_REQUEST["mobile"]."#_#','".$NamePhone[0]. "#_#','".$NamePhone[1]."');";

                      }

                    $result = mysql_query($query);
                 }
             }
    }
    else {
        foreach($explode as $value){
            if ($value != ""){

                $NamePhone = explode("#-#",$value);
                $query = "insert into contacts (_id,datetime, mobile, name, phone) values " .
                      "(NULL,NOW(),'".$_REQUEST["mobile"]."#_#','".$NamePhone[0]. "#_#','".$NamePhone[1]."');";
                $result = mysql_query($query);
            }
        }
    }


?>