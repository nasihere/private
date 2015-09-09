<?php
    include_once("db.php");

  //  echo "<pre>";print_r($_REQUEST);echo "</pre>";echo remedies();die;
    //echo $result = mysql_query($_REQUEST['qry']);


            //If id_web is not there then set null value in id_web
            if ($_REQUEST['id_web'] == ""){

                $_REQUEST['id_web'] = "null"; //it's required to set null for new record
            }


            // Mobile Number setting if its empty then set Admin Number
            if ($_REQUEST['mobile'] == ""){
                $_REQUEST['mobile'] = "3233004756";
            }

            $_REQUEST['selected'] = "";
            if ($_REQUEST['logo'] != ""){
                $_REQUEST['selected'] .= $_REQUEST['logo'] . ",";
            }

            if ($_REQUEST['picture'] != ""){
                $_REQUEST['selected'] .= $_REQUEST['picture'] . ",";
            }

            $PreviewDetailsRemedies = remedies();

           $PreviewDetails ="";
            $_REQUEST['Name'] = ucwords(strtolower($_REQUEST['Name']));
            $PreviewDetails .= "Intensity#-#'" . $_REQUEST['mobile'] . "'#_#";
            $PreviewDetails .= "maincategoy#-#'" . $_REQUEST['Name'] . "'#_#";
            $PreviewDetails .= "categoy#-#'" . $_REQUEST['categoy'] . "'#_#";
            $PreviewDetails .= "Name#-#'" . $_REQUEST['Name'] . "'#_#";
            $PreviewDetails .= "level#-#'" . "" . "'#_#";
            $PreviewDetails .= "selected#-#'" . $_REQUEST['selected'] . "'#_#";
             $PreviewDetails .= "lat_lng#-#'" . $_REQUEST['lat_lng'] . "'#_#";

            $PreviewDetails .= "remedies#-#'" . $PreviewDetailsRemedies . "'#_#";
            $PreviewDetails .= "sublevel#-#'" . "" . "'#_#";
            $PreviewDetails .= "id_web#-#" . $_REQUEST['id_web']. "#_#";


            $_REQUEST["param"] = $PreviewDetails;
            include_once("addnew.php");

          /*  $_REQUEST["session_id"] = $_REQUEST["uniqueid"];
            $_REQUEST["webupload"] = "true";

            include_once("shifaappsettings.php");
            header("location:webviewhost.php?category=".$_REQUEST['categoy']. "&load=CreateStation");
        */


    function remedies(){
        $data = "";
        foreach($_REQUEST as $key => $val){
           if (strrpos($key,"remedies") === false) {
           }
           else{
                $key = str_replace("remedies_","",$key);
                if ($val != ""){
                    $data .= $key . "-,-" . $val . "--,--";
                }
            }
        }
        return $data;
    }


?>