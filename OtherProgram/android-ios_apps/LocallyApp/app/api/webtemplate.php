<?php
    if ($_REQUEST["submit"]){
    include_once("db.php");


    $insert = "insert into template (id_web,template,category) values (NULL,'".$_REQUEST['fields']."','".$_REQUEST['category']."')";
     mysql_query($insert);
    echo "Inserted properly..";
    }
?>
<!doctype xml>
<html>
<head>
<meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<script>
    function preview(){
        var value = document.getElementById("fields").value;
        value = value.replace(/#_#/g,"<br/>");
        document.getElementById("preview").innerHTML = value;
        return "";
    }
</script>
<body>
<a href="webviewhost.php?code=004756">Main Page</a>
<h4>Create New Template in <?php echo str_replace("|"," > ",$_REQUEST['category']); ?> > _______ </h4>
<form action="webtemplate.php" method="post" enctype="multipart/form-data">
    Item Name: <input type="text" onkeyup="preview()" name="fields" id="fields" value="" /><br />
    Example: Phone#_#Address#_#Hour#_#Price Range#_#Web#_#Audio#_#Details
    <div id="preview">

    </div>
    <input type="hidden" value="<?php echo $_REQUEST['category'];?>" name="category" />
    <input type="submit" name="submit" value="Save" /><br />
</form>
</body>

</html>