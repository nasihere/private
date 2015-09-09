<?php
    include_once("db.php");
    include_once("param.php");

    if ($_REQUEST["cols"] == "") $_REQUEST['cols'] = "id_web,book,newrem,maincategoy,sublevel,Name,remedies,Intensity,categoy,level,selected,entry";

    //$where = $_REQUEST["where"];
    $cols = $_REQUEST["cols"];

    $explode = "";
    $output = "DELETE FROM tbl_shifa;".$explode;
     $selectQry = "select ". $cols ." from mumbra where deleted = 0";
    $result = mysql_query($selectQry);

    while ($line = mysql_fetch_array($result, MYSQL_ASSOC)) {
        $values = "";
        $keys  = "";
        foreach ($line as $key => $col_value) {
            $keys .= $key.",";
            $values .= "'".$col_value."',";
        }
         $keys = rtrim($keys, ",");
         $values = rtrim($values, ",");

         $output .= "insert into tbl_shifa (".$keys.") values (".$values.");".$explode;
    }


      $output .= "DELETE FROM tbl_template;".$explode;
      $selectQry = "select template,id_web,category from template;";
        $result = mysql_query($selectQry);

        while ($line = mysql_fetch_array($result, MYSQL_ASSOC)) {
            $values = "";
            $keys  = "";
            foreach ($line as $key => $col_value) {
                $keys .= $key.",";
                $values .= "'".$col_value."',";
            }
             $keys = rtrim($keys, ",");
             $values = rtrim($values, ",");

             $output .= "insert into tbl_template (".$keys.") values (".$values.");".$explode;
        }
        $output = rtrim($output, "#_#");
    echo $output;

?>