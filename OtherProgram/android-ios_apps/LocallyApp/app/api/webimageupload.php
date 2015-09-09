<?php

    if ($_REQUEST["submit"]){
        if ($_REQUEST['name'] == "logo"){
             $tempuniqueid =  uniqid() . "_logo";
             $BigSize = 122;
             $ThumbSize = 35;
        }
        else
        {

             $tempuniqueid =  uniqid();
        }

        $_REQUEST['selected'] = $tempuniqueid;


        include_once('shifaappsettings.php');
    }
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <body>

    <form action="webimageupload.php" method="post" enctype="multipart/form-data">
        <input type="file" name="image"  id="fileToUpload">
        <input type="hidden" name="name"  value="<?php echo $_REQUEST['name']; ?>">
              <input  type="submit" name="submit" value="Save" />
        </form>
    </body>
    <script>
        var PictureId = "<?php echo $tempuniqueid; ?>";
        var ParentElement = "<?php echo $_REQUEST['name']; ?>";
        if(PictureId){

             try{
                    if (ParentElement == "logo"){
                        window.parent.$('input[name = "'+ParentElement+'"]').val(PictureId);
                        window.parent.$('div[name = "'+ParentElement+'_preview"]').html("<img src='http://kent.nasz.us/app_php/shifaappsettings/mumbra/" + PictureId + "_thumb.jpg' />");
                    }
                    else {
                        if (window.parent.$('input[name = "'+ParentElement+'"]').val() != "")
                        {
                            window.parent.$('input[name = "'+ParentElement+'"]').val(window.parent.$('input[name = "'+ParentElement+'"]').val() + ",");
                            window.parent.$('div[name = "'+ParentElement+'_preview"]').html(window.parent.$('div[name = "'+ParentElement+'_preview"]').html() + "<span> </span>");
                        }
                        window.parent.$('input[name = "'+ParentElement+'"]').val(window.parent.$('input[name = "'+ParentElement+'"]').val() + PictureId);
                        window.parent.$('div[name = "'+ParentElement+'_preview"]').html(window.parent.$('div[name = "'+ParentElement+'_preview"]').html() + "<img src='http://kent.nasz.us/app_php/shifaappsettings/mumbra/" + PictureId + "_thumb.jpg' />");

                    }
               }
               catch(E){
               }



        }
    </script>
</html>

