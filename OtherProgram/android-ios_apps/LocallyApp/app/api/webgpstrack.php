<?php
if ($_REQUEST['mobile'] != ""){
     include_once("db.php");
     $selectQry = "select geolat,geolng,session_id,fname from tbl_app_registration  where session_id like '%".$_REQUEST['mobile']."%'";
     $result = mysql_query($selectQry);
     while($row = mysql_fetch_assoc($result)){
        echo $row['fname'] . " - " . $row['session_id'] . "<br/>";
        echo "<img src='https://maps.googleapis.com/maps/api/staticmap?center=".$row['geolat'].",".$row['geolng']."&zoom=16&size=400x400'/>";
         echo "<img src='https://maps.googleapis.com/maps/api/streetview?location=".$row['geolat'].",".$row['geolng']."&zoom=12&size=400x400&fov=90&heading=235&pitch=1'/>";

        echo "<hr>";
     }
}

?>
<!doctype xml>
<html>
<head>
<meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>
<form action="webgpstrack.php" method="post" enctype="multipart/form-data">
   <input type="text" style="width:250px" name="mobile" value="<?php  echo $_REQUEST['mobile'];?>" /><br />
      <input type="submit" name="submit" value="Track" /><br />
</form>



</body>
</html>