<?php

    include_once("db.php");
    $p = $_REQUEST["param"];


     $query = "select blocked from tbl_app_registration  where session_id = '" . $_REQUEST['mobile'] . "'";
    $result_ = mysql_query($query);
     $count = mysql_num_rows($result_);
    if ($count == 0){
         $query = "insert into tbl_app_registration (fname,session_id,sex,agegroup,datetime,_id,address,gcmreg,verifycode) values " .
                 "('".$_REQUEST['name']."'," .
                  "'".$_REQUEST['mobile']."'," .
                  "'".$_REQUEST['gender']."'," .
                  "'".$_REQUEST['age']."'," .
                   "NOW()," .
                   "NULL," .

                   "'".$_REQUEST["address"]."'," .
                   "'".$_REQUEST["gcmreg"]."'," .
                   "'".$_REQUEST["code"]."'" .
                 ");";
          $result = mysql_query($query);

        $query = "insert into mumbra (categoy,Name,Intensity) values " .
                  "('Personal & Secured Account','".$_REQUEST['name']." - ".$_REQUEST['mobile']."','".$_REQUEST['mobile']."');";
           $result = mysql_query($query);

            $query = "insert into mumbra (categoy,Name,Intensity) values " .
                             "('Personal & Secured Account|".$_REQUEST['name']." - ".$_REQUEST['mobile']."','My Contacts','".$_REQUEST['mobile']."');";
                      $result = mysql_query($query);

            $query = "insert into mumbra (categoy,Name,Intensity) values " .
                                       "('Personal & Secured Account|".$_REQUEST['name']." - ".$_REQUEST['mobile']."','To Do Items','".$_REQUEST['mobile']."');";
                                $result = mysql_query($query);

          $query = "insert into mumbra (categoy,Name,Intensity) values " .
                             "('Personal & Secured Account|".$_REQUEST['name']." - ".$_REQUEST['mobile']."','Friends Help','".$_REQUEST['mobile']."');";
                      $result = mysql_query($query);


    }
    else
    {
        $row = mysql_fetch_assoc($result_);
        if ($row['blocked'] == "Y"){
            echo "-Details-: Phone Number is blocked! Contact Administrator: nasihere@gmail.com";
            return;
        }
        else
        {
            $query = "update tbl_app_registration set verifycode = '" . $_REQUEST["code"]."',lastvisitdatetime = NOW(), sex = '".$_REQUEST['gender']."',agegroup = '".$_REQUEST['age']."', address= '".$_REQUEST['address']."' where session_id  = '".$_REQUEST['mobile']."'";
            $result = mysql_query($query);
            echo "-Details-: Verification SMS Sent for verification.";

        }
    }
    include_once("contacts.php");



?>