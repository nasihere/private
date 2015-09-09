





<?php

// Imaging
class imaging
{

    // Variables
    private $img_input;
    private $img_output;
    private $img_src;
    private $format;
    private $quality = 80;
    private $x_input;
    private $y_input;
    private $x_output;
    private $y_output;
    private $resize;

    // Set image
    public function set_img($img)
    {

        // Find format
        $ext = strtoupper(pathinfo($img, PATHINFO_EXTENSION));

$source_url = $_FILES["image"]["tmp_name"];
            $info = getimagesize($source_url);
            if ($info['mime'] == 'image/jpeg') $image = imagecreatefromjpeg($source_url);
         	elseif ($info['mime'] == 'image/gif') $image = imagecreatefromgif($source_url);
         	elseif ($info['mime'] == 'image/png') $image = imagecreatefrompng($source_url);


            $this->format = $ext;
            $this->img_input = $image;
            $this->img_src = $img;



        // Get dimensions
        $this->x_input = imagesx($this->img_input);
        $this->y_input = imagesy($this->img_input);

    }

    // Set maximum image size (pixels)
    public function set_size($size = 100)
    {

        // Resize
        if($this->x_input > $size && $this->y_input > $size)
        {

            // Wide
            if($this->x_input >= $this->y_input)
            {

                $this->x_output = $size;
                $this->y_output = ($this->x_output / $this->x_input) * $this->y_input;

            }

            // Tall
            else
            {

                $this->y_output = $size;
                $this->x_output = ($this->y_output / $this->y_input) * $this->x_input;

            }

            // Ready
            $this->resize = TRUE;

        }

        // Don't resize
        else { $this->resize = FALSE; }

    }

    // Set image quality (JPEG only)
    public function set_quality($quality)
    {

        if(is_int($quality))
        {

            $this->quality = $quality;

        }

    }

    // Save image
    public function save_img($path)
    {

        // Resize
        if($this->resize)
        {

            $this->img_output = ImageCreateTrueColor($this->x_output, $this->y_output);
            ImageCopyResampled($this->img_output, $this->img_input, 0, 0, 0, 0, $this->x_output, $this->y_output, $this->x_input, $this->y_input);
            imagepng($this->img_output, $path);
            return true;
        }

        return false;


    }

    // Get width
    public function get_width()
    {

        return $this->x_input;

    }

    // Get height
    public function get_height()
    {

        return $this->y_input;

    }

    // Clear image cache
    public function clear_cache()
    {

        @ImageDestroy($this->img_input);
        @ImageDestroy($this->img_output);

    }

}

//##### DEMO #####
 $imageOgiFilename = $image = basename($_REQUEST["selected"] . ".jpg");
          $imageThumb = basename($_REQUEST["selected"] . "_thumb.jpg");

                $directory = $target_dir;
          $destination_url = "../../app_php/shifaappsettings/mumbra/"  . $image;
          $destination_urlThumb = "../../app_php/shifaappsettings/mumbra/"  . $imageThumb;
         $source_url = $_FILES["image"]["tmp_name"];



// Image
$src = $imageOgiFilename;

// Begin
$img = new imaging;
$img->set_img($src);
$img->set_quality(100);

// Small thumbnail
if ($BigSize == "") $BigSize = 500;
if ($ThumbSize == "") $ThumbSize = 200;

$img->set_size($BigSize);
$flag = $img->save_img($destination_url);

if ($flag == false){
    // Small thumbnail
    $img->set_size($ThumbSize);
    $flag = $img->save_img($destination_url);

}
// Baby thumbnail
$img->set_size($ThumbSize);
$img->save_img($destination_urlThumb);

// Finalize
$img->clear_cache();

//Make sure dnot response or echo anything in this php
//echo "<img src='http://kent.nasz.us/app_php/shifaappsettings/mumbra/" . $imageOgiFilename . "' />" ;
//echo "<img src='http://kent.nasz.us/app_php/shifaappsettings/mumbra/" . $imageThumb . "' />" ;

?>