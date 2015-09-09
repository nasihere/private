
  <form action="webaddnew.php">
            <input name="category" readonly value ="<?php echo $_REQUEST['category']; ?>" type="text" />
         <input type="submit" name="load" value="CreateStation" />
    </form>

<a href="webviewmainstation.php?code=00987">Main Page</a>

<?php
    include_once("db.php");
     $selectQry = "select Name,Intensity,categoy,  remedies, json,id_web,reported,selected from mumbra  where deleted = 0 and categoy = '".$_REQUEST['category']."' order by id_web desc ";
   echo $selectQry;
   echo "<hr>";
    $result = mysql_query($selectQry);

    echo "<table border='1'>";
    echo "<tr><th>Name</th><th>Mobile</th><th>Category</th><th><Details/td><th>Modify</th><th>id_web</th><th>Reported</th><th>selected</th><th>Delete</th></tr>";
    while ($line = mysql_fetch_array($result, MYSQL_ASSOC)) {
         echo "<tr>";

        foreach ($line as $key => $col_value) {
            if ($key == "categoy"){
                echo "<td><a href='webaddnew.php?category=$col_value'>". $col_value." </a>";
                echo "| <a href='webtemplate.php?category=$col_value|$line[Name]'>Template</a></td>";

           } else if ($key == "Name"){
                echo "<td><a href='webviewhost.php?category=".$line['categoy']."|".$line['Name']."'>". $col_value."</a>";

                echo "<br/><img src='http://kent.nasz.us/app_php/shifaappsettings/mumbra/".$line['id_web']."_thumb.jpg' />";
                echo '<form action="webimageuploadicon.php" method="post" enctype="multipart/form-data">';
                                      echo '<input type="text" style="width:250px" name="session_id" value="" /><br />';
                                      echo '<input type="file" name="image" id="fileToUpload">';
                                     echo '<input type="hidden" name="id_web" value="'.$line['id_web'].'" />';
                                     echo '<input type="hidden" name="webupload" value="true" />';
                                      echo '<input type="submit" name="submit" value="Save" /><br />';
                                echo '</form>';

                        echo "</td>";

            }
            else if ($key == "remedies") //Details
                echo "<td>".str_replace("--,--","<br/>",$col_value)."<a href='webedit.php?id_web=".$line['id_web']."'> - Edit</a></td>";
             else if ($key == "selected" && $line['selected']) //Picture
             {
              echo "<td> ";
                $line['selected'] = str_replace(" ","",$line['selected']);
                $firstimage = explode(",",$line['selected']);
                foreach($firstimage as $key){
                       if ($key != "" ) {
                            $imgsrc = str_replace("_set","",$key);
                            echo "<br><img  src='http://kent.nasz.us/app_php/shifaappsettings/mumbra/".$imgsrc."_thumb.jpg'/><br/>";
                            echo "<a href='webimagedelete.php?id_web=".$line['id_web']."&pictureid=". $key ."'>Delete</a><br>";
                        }
                }
                echo '<form action="webimageupload.php" method="post" enctype="multipart/form-data">';
                      echo '<input type="text" style="width:250px" name="session_id" value="" /><br />';
                      echo '<input type="file" name="image" id="fileToUpload">';
                     echo '<input type="hidden" name="id_web" value="'.$line['id_web'].'" />';
                     echo '<input type="hidden" name="webupload" value="true" />';
                      echo '<input type="submit" name="submit" value="Save" /><br />';
                echo '</form>';
                echo "</td>";
               // echo "<td>".$col_value."<img style='height:100px;width:100px;' src='http://kent.nasz.us/app_php/shifaappsettings/mumbra/".$firstimage[0].".jpg'/> <a href='webimageupload.php?uniqueid=".$line['selected']."'> - Edit</a><a href='webimagedelete.php?id_web=". $line['id_web'] ."'> - Delete</a></td>";
            }
            else if ($key == "selected" && $line['selected'] == "")
            {

                            echo "<td>";
                            echo "<a href='webimageupload.php?id_web=". $line['id_web'] ."'> - Add</a>";
                             echo '<form action="webimageupload.php" method="post" enctype="multipart/form-data">';
                                                  echo '<input type="text" style="width:250px" name="session_id" value="" /><br />';
                                                  echo '<input type="file" name="image" id="fileToUpload">';
                                                  echo '<input type="hidden" name="webupload" value="true" />';
                                                  echo '<input type="hidden" name="id_web" value="'.$line['id_web'].'" />';
                                                  echo '<input type="submit" name="submit" value="Save" /><br />';
                                            echo '</form>';

                            echo "</td>";
             }

            else
                echo "<td>". $col_value."</td>";
        }

                echo "<td><a href='webdelete.php?id_web=".$line['id_web']."'>". "Delete"."</a></td>";

         echo "</tr>";
    }
     echo "</table>";

?>