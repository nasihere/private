<?php
    //include_once("db.php");


        // PLease confirm in app Param - Activity_addnew
        /*$AddNewParam =
                    "Intensity#-#'"+SessionID+"'#_#" +
                    "maincategoy#-#'" + obj.Name + "'#_#" +
                    "categoy#-#'" + CategoryID + "'#_#" +
                    "Name#-#'" + obj.Name +  "'#_#" +
                    "level#-#'" + "" +  "'#_#" +
                    "remedies#-#'" + "" +  "'#_#" +
                    "sublevel#-#'" + "2" +  "'#_#" +
                    "id_web#-#" + "null" + "#_#";*/

    if ($_REQUEST["submit"]){


            $PreviewDetailsRemedies = "";
            if ($_REQUEST['Address']){
                $PreviewDetailsRemedies .= "Address-,-" . ucwords(strtolower($_REQUEST['Address'])) . "--,--";
             }
            if ($_REQUEST['Phone']){
                $PreviewDetailsRemedies .= "Phone-,-" . $_REQUEST['Phone'] . "--,--";
            }

            if ($_REQUEST['Hour']){
                $PreviewDetailsRemedies .= "Hours-,-" . $_REQUEST['Hour'] . "--,--";
            }

            if ($_REQUEST['Price']){
                $PreviewDetailsRemedies .= "Price Range-,-" . $_REQUEST['Price'] . "--,--";
            }

            if ($_REQUEST['Website']){
                $PreviewDetailsRemedies .= "Web-,-" . strtolower($_REQUEST['Website']) . "--,--";
            }

            if ($_REQUEST['Details']){
                $PreviewDetailsRemedies .= "Details-,-" . ucwords(strtolower($_REQUEST['Details'])) . "--,--";
            }

            if ($_FILES["image"]["tmp_name"] == ""){
                $_REQUEST['uniqueid'] =   "";
            }
            else{
                $_REQUEST['session_id'] =  $_REQUEST["uniqueid"];
            }

            $_REQUEST['newrem'] = str_replace("'","",$_REQUEST['newrem']);
           $PreviewDetails ="";
            $_REQUEST['Name'] = ucwords(strtolower($_REQUEST['Name']));
            $PreviewDetails .= "Intensity#-#'" . "3233004756" . "'#_#";
            $PreviewDetails .= "maincategoy#-#'" . $_REQUEST['Name'] . "'#_#";
            $PreviewDetails .= "categoy#-#'" . $_REQUEST['categoy'] . "'#_#";
            $PreviewDetails .= "Name#-#'" . $_REQUEST['Name'] . "'#_#";
            $PreviewDetails .= "level#-#'" . "" . "'#_#";
            $PreviewDetails .= "selected#-#'" . $_REQUEST['uniqueid'] . "'#_#";

            $PreviewDetails .= "newrem#-#'" . $_REQUEST['newrem'] . "'#_#";
            $PreviewDetails .= "remedies#-#'" . $PreviewDetailsRemedies . "'#_#";
            $PreviewDetails .= "sublevel#-#'" . "" . "'#_#";
            $PreviewDetails .= "id_web#-#" . "null". "#_#";


            $_REQUEST["param"] = $PreviewDetails;
            include_once("addnew.php");
            $_REQUEST["session_id"] = $_REQUEST["uniqueid"];
            $_REQUEST["webupload"] = "true";

            include_once("shifaappsettings.php");
            header("location:webviewhost.php?category=".$_REQUEST['categoy']. "&load=CreateStation");

    }
    if($_REQUEST['save'] == "true") echo "Saved! <br>";
    echo $catHeading = str_replace("|"," > ",$_REQUEST['categoy']);
?>
<!doctype xml>
<html>
<head>
<meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>
<a href="webviewmainstation.php?code=004756">Main Page</a>
<h4>Create New Item in <?php echo str_replace("|"," > ",$_REQUEST['category']); ?> > _______ </h4>
<form action="webaddnew.php" method="post" enctype="multipart/form-data">
    Name: <input type="text" name="Name" value="" /><br />

<?php if ($_REQUEST['category'] == '') { ?>
    Category: <input type="text" name="categoy" value="<?php echo $_REQUEST['category']; ?>" /><br />
<?php } else { ?>
    <input type="hidden" name="categoy" value="<?php echo $_REQUEST['category']; ?>" /><br />

<?php } ?>

    Address:
    <textarea  name="Address" cols="15" rows="8"><?php echo $param['Address'];?></textarea><br />


    Phone: <input type="text" name="Phone" value="<?php echo $param['Phone'];?>" /><br />

    Website/Email:
    <input type="text" name="Website" value="<?php echo $param['Website'];?>" /><br />

    Details:
    <input type="text" name="Details" value="<?php echo $param['Details'];?>" /><br />

    Hours:
    <input type="text" name="Hour" value="<?php echo $param['Hour'];?>" /><br />

    Price Range
    <input type="text" name="Price" value="<?php echo $param['Price Range'];?>" /><br />

    Memo
        <textarea type="text" name="newrem"  ><?php echo $param['newrem'];?></textarea><br />

    Photo:

    <input type="text" style="width:250px" readonly name="uniqueid" value="<?php  printf("%s\r\n", uniqid());?>-<?php printf("%s\r\n", uniqid());?>" /><br />
    <input type="file" name="image" id="fileToUpload">

    <input type="submit" name="submit" value="Save" /><br />
</form>
</body>
</html>