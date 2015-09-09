<?php
    include_once("db.php");


    if ($_REQUEST["submit"]){
        $PreviewDetails ="";
        
        if ($_REQUEST['address'] || $_REQUEST['Address']){
            $PreviewDetails .= "Address-,-" . ucwords(strtolower($_REQUEST['Address'])) . "--,--";
         }
        if ($_REQUEST['phone'] || $_REQUEST['Phone']){
            $PreviewDetails .= "Phone-,-" . $_REQUEST['Phone'] . "--,--";
        }

        if ($_REQUEST['Hours'] || $_REQUEST['Hours']){
            $PreviewDetails .= "Hours-,-" . $_REQUEST['Hours'] . "--,--";
        }

        if ($_REQUEST['price'] || $_REQUEST['Price']){
            $PreviewDetails .= "Price Range-,-" . $_REQUEST['Price'] . "--,--";
        }

        if ($_REQUEST['website'] || $_REQUEST['Website']){
            $PreviewDetails .= "Web-,-" . strtolower($_REQUEST['Website']) . "--,--";
        }

        if ($_REQUEST['details'] || $_REQUEST['Details']){
            $PreviewDetails .= "Details-,-" . ucwords(strtolower($_REQUEST['Details'])) . "--,--";
        }

        $update = "update mumbra set remedies = '$PreviewDetails' where id_web = " . $_REQUEST["id_web"];
        mysql_query($update);
     //   header("Location: index.php");
        echo "Thanks for updating.. Tap back button, Goto Menu, -> Tap Update to refresh your entries";
        die;
    }
    else{

        $res = mysql_query("select * from mumbra where id_web = ". $_REQUEST["id_web"]);
        $param = mysql_fetch_assoc($res);

        $p = explode("--,--", $param["remedies"]);

        foreach($p as $key => $value){
            $explode = explode("-,-",$value);
            if ($explode[0]){
                $keys .= $explode[0] . ",";
                $values .=  $explode[1] . ",";
                $param[$explode[0]] =  $explode[1];
               }
        }
    }
?>
<!doctype xml>
<html>
<head>
<meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>
<form action="webedit.php?edit=true&id_web=$_REQUEST['id_web']" >
    Name: <input type="text" name="Name" readonly value="<?php echo $param['Name'];?>" /><br />

    Address:
    <input type="text" name="Address" value="<?php echo $param['Address'];?>" /><br />


    Phone: <input type="text" name="Phone" value="<?php echo $param['Phone'];?>" /><br />

    Website/Email:
    <input type="text" name="Website" value="<?php echo $param['Web'];?>" /><br />

    Details:
    <input type="text" name="Details" value="<?php echo $param['Details'];?>" /><br />

    Hours:
    <input type="text" name="Hours" value="<?php echo $param['Hours'];?>" /><br />

    Price Range
    <input type="text" name="Price" value="<?php echo $param['Price Range'];?>" /><br />


    <input type="hidden" name="id_web" value="<?php echo $_REQUEST['id_web']; ?>"/>

    <input type="submit" name="submit" value="Save" /><br />
</form>
</body>
</html>