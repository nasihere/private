<?php
   if ($_REQUEST["code"] != "0987") die;


?>
  <form action="webaddnew.php">
         <input type="submit" name="load" value="CreateStation" />
    </form>


<?php
    include_once("db.php");

     $selectQry = "select Name,Intensity,categoy, entry, remedies, json,id_web,reported from mumbra  where deleted = 0 and level = '0' order by id_web desc ";
    $result = mysql_query($selectQry);

    echo "<table border='1'>";
    echo "<tr><th>Name</th><th>Mobile</th><th>Category</th><th>Record</th><th><Details/td><th>Modify</th><th>Reported</th><th>Delete</th></tr>";
    while ($line = mysql_fetch_array($result, MYSQL_ASSOC)) {
         echo "<tr>";

        foreach ($line as $key => $col_value) {
            if ($key == "categoy") {
                echo "<td><a href='webaddnew.php?category=$col_value'>". $col_value."</a></td>";
            }
            else if ($key == "Name")
                echo "<td><a href='webviewhost.php?category=".$line['Name']."'>". $col_value."</a></td>";
            else if ($key == "remedies")
                echo "<td><a href='webedit.php?id_web=".$line['id_web']."'>". $col_value."</a></td>";
            else if ($key == "id_web")
                echo "";

            else
                echo "<td>". $col_value."</td>";
        }
                echo "<td><a href='webdelete.php?id_web=".$line['id_web']."'>". "Delete"."</a></td>";

         echo "</tr>";
    }
     echo "</table>";

?>