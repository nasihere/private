<?php
// Where the file is going to be placed
$target_dir = "../../app_php/shifaappsettings/mumbra/audio/";
 
/* Add the original filename to our target path.
Result is "uploads/filename.extension" */
$target_path = $target_dir .   basename( $_REQUEST["AudioUniqueID"]);

if(move_uploaded_file($_FILES['uploaded_file']['tmp_name'], $target_path)) {
    echo "The file ".  basename( $_FILES['uploaded_file']['name']).
    " has been uploaded";
} else{
    echo "There was an error uploading the file, please try again!";
    echo "filename: " .  basename( $_FILES['uploaded_file']['name']);
    echo "target_path: " .$target_path;
}