<?php
if ($_REQUEST['mobile'] != ""){
     include_once("db.php");
     $selectQry = "select gcmreg from tbl_app_registration  where session_id like '%".$_REQUEST['mobile']."%'";
     $result = mysql_query($selectQry);
     $row = mysql_fetch_assoc($result);
     $row['gcmreg'];

}

?>
<!doctype xml>

<?php

if ($row['gcmreg']){
?>

<html>
<head>
    <title>AndroidBegin - Android GCM Tutorial</title>
    <link rel="icon" type="image/png" href="http://www.androidbegin.com/wp-content/uploads/2013/04/favicon1.png"/>
</head>

<body>

<a href="http://www.AndroidBegin.com" target="_blank">
    <img src="http://www.androidbegin.com/wp-content/uploads/2013/04/Web-Logo.png" alt="AndroidBegin.com"></br></a></br>

<form action="gcm_engine.php" method="post">
    Google API Key (with IP locking) : <INPUT size=70% TYPE="Text"  NAME="apiKey" value="AIzaSyA8byEiPK33JthPTlzxIwT94xBDLLsKors"></br>
    Get your Google API Key : <a href="https://code.google.com/apis/console/" target="_blank">Google API</a></br></br>
    <img src="http://www.androidbegin.com/wp-content/uploads/2013/05/Google-API-Key.png" alt="Google API Key" ></br></br>

    Device Registration ID : <INPUT size=70% TYPE = "Text"  NAME = "registrationIDs" value="<?php echo $row['gcmreg'];?>"></br></br>
    Tap on the Register button in your GCM Tutorial App and locate Device Registration ID in LogCat</br>
    <img src="http://www.androidbegin.com/wp-content/uploads/2013/05/Device-Registration-ID.png" alt="Device Registration ID" ></br></br>

    Notification Message : <INPUT size=70% TYPE = "Text" VALUE="Discount in Cloth Shop#_#50% Show this couple get 200Rs discount" NAME = "message"></br></br>
    <input type="submit" value="Send Notification"/>
    <b>Hint:</b>
    <i>if messge just 'GPS' then it will update person gps location
    
</form>

</body>
</html>


<?php
}
else
{
?>
<html>
<head>
<meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>
<form method="post" enctype="multipart/form-data">
   <input type="text" style="width:250px" name="mobile" value="<?php  echo $_REQUEST['mobile'];?>" /><br />
      <input type="submit" name="submit" value="Track" /><br />
</form>



</body>
</html>

<?php

}

?>