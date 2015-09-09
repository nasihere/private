<?php
	include "config.php";
	
	function create_query($TABLENAME, $WHERE){
		return "SELECT * FROM " . $TABLENAME . " WHERE " . $WHERE;
	}
	
	
	function create_query_col($TABLENAME, $WHERE, $COLS){
		return "SELECT " . $COLS . " FROM " . $TABLENAME . " WHERE " . $WHERE;
	}
	
	
	function get_col_val($query){
	    $res = mysql_query($query);
		if (!$res) return "";
         $num_rows = mysql_num_rows($res);
         if ($num_rows)
         {

             $row = mysql_fetch_array($res);

             return $row[0];
        }
        else
        {
            return "";
        }
    }



    function get_col_array($query){
     // echo $query;
//	echo $query;
 
        $res = mysql_query($query);
        if (!$res)  return "Error in query " . $query;
        $response = "";
        $num_rows = mysql_num_rows($res);
         if ($num_rows)
         {
			 $data = "";
             while($row = mysql_fetch_assoc($res)){
             		 $data[] =  $row;
             }
             
		     return $data;// . "##-##" . $query;

        }
        else
        {
            return "";
        }
    }

?>