<?php
    include_once("db.php");

if ($_REQUEST['action'] == "") $_REQUEST['action']= "read";
    switch ($_REQUEST['action']) {
        case 'Template_write':

            echo $insert = "INSERT INTO template (category,layout,datetime) VALUES ('".mysql_real_escape_string($_REQUEST['category'])."', '".mysql_real_escape_string($_REQUEST['layout'])."',NOW())";
            echo $result = mysql_query($insert);
            if ($result == 1)
            {
                echo "-Success-";
            }
            else
            {
                echo "-Error-";
            }
            break;

        case 'write':

            print_r($_REQUEST);
            echo $_REQUEST['qry'];

            echo $result = mysql_query($_REQUEST['qry']);

            break;
        case 'web':

            $_REQUEST['qry'] = str_replace("cols ", "select Name,remedies,selected,Book,lat_lng as LatLng,entry as Count,sublevel as Rating,id_web as id from mumbra where ",$_REQUEST['qry']);
            $_REQUEST['qry'] = str_replace("lik ", "like '%",$_REQUEST['qry']);


           // echo $_REQUEST['qry'];
            $result = mysql_query($_REQUEST['qry']);

            $encode = array();
            while($row = mysql_fetch_array($result,MYSQL_ASSOC)) {
                    if ($row['selected']){

                        $tmpdata = explode("-:-",$row['Book']);
                        $i=0;
                        foreach($tmpdata as $data)
                        {
                            if ($data != ""){
                             $data = explode("-,-",$data);
                             $row['comment'][$i]['mobile'] = $data[0];
                             $row['comment'][$i]['review'] = $data[1];
                             $row['comment'][$i]['date'] = $data[2];
                             $i++;
                            }
                        }
                        unset($row['Book']);

                        $tmpdata = explode(",",$row['selected']);
                        foreach($tmpdata as $data)
                        {
                            if (strrpos($data,"_logo")===false){
                                if ($data){
                                    $row['picture'][] = $data;
                                }
                            }
                            else{
                                $row['logo'] = $data;
                             }

                        }
                        unset($row['selected']);

                    }
                    if ($row['remedies']){
                        $tmpdata = explode("--,--",$row['remedies']);
                        unset($row['remedies']);
                        $parentItem = "";
                        foreach($tmpdata as $data)
                        {
                            $data = explode("-,-",$data);
                            if ($data[0]){
                                //echo (strrpos($data[0],$parentItem) === false) . " == " . $parentItem . " ~~ " . $data[0] . "<br />";
                                if (strrpos($data[0],"Group_") === 0){
                                    if (strrpos($data[0],$parentItem) === 0 ){
                                       // echo "Same Item " . $parentItem;
                                       $data[0] = str_replace($parentItem."_","",$data[0]);
                                       $data[0] = str_replace("_"," ",$data[0]);
                                       $row['data'][$parentItem][$data[0]] = $data[1];
                                    }
                                    else{
                                        $row['data'][$data[0]] =  array();
                                        $parentItem = $data[0];
                                    }

                                }
                                else{
                                    $row['data'][$data[0]] = $data[1];

                                    //$count = 0;
                                }
                            }
                        }
                    }
                    $encode[] = $row;
            }
            if (!$encode){
                echo $_REQUEST['qry'];
            }
            //$encrypted = mcrypt_encrypt(MCRYPT_RIJNDAEL_256, $key, json_encode($encode), MCRYPT_MODE_ECB);
            echo json_encode($encode);
          //  echo "<pre>"; print_r($encode); echo "</pre>";
       break;

        //////////Simple Read Raw Data
        case 'read':

            $_REQUEST['qry'] = str_replace("cols ", "select ",$_REQUEST['qry']);
            $_REQUEST['qry'] = str_replace("table", "from mumbra where ",$_REQUEST['qry']);

            $result = mysql_query($_REQUEST['qry']);
            if (!$result) echo "No Data!!";
            $encode = array();
            while($row = mysql_fetch_array($result,MYSQL_ASSOC)) {

                    $encode[] = $row;
            }

            echo json_encode($encode);
        break;



    }
     mysql_close($link);




?>