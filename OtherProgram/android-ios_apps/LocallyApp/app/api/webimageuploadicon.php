<?php
if ($_REQUEST['selected'] == ""){
    $_REQUEST['selected'] = $_REQUEST['id_web'];
}

print_r($_REQUEST);
echo "<hr>";

 $BigSize = 122;
 $ThumbSize = 35;


 include_once("image.php");
 include_once('shifaappsettings.php');
 header("location:".$_SERVER['HTTP_REFERER']);
/*
else{
    $tempuniqueid =    $_REQUEST['uniqueid'] ;
}

?>
<!doctype xml>
<html>
<head>
<meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>
<img style='height:100px;width:100px;' src='http://kent.nasz.us/app_php/shifaappsettings/mumbra/<?php echo $_REQUEST["pictureid"];?>.jpg'/>
<form action="shifaappsettings.php" method="post" enctype="multipart/form-data">
   <input type="text" style="width:250px" name="session_id" value="<?php  echo $tempuniqueid;?>" /><br />
      <input type="file" name="image" id="fileToUpload">
      <input type="hidden" name="webupload" value="true" />
      <input type="submit" name="submit" value="Save" /><br />
</form>



</body>
</html>
*/

?>