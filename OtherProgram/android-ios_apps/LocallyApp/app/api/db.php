<?php

        include_once("config.php");
        $select = null;
        $res = null;

        function select($query){
            $select = null;
            $res = mysql_query($query);

            return res;
        }

        function insert($param,$table){
            $values = "";
            $keys  = "";
               $time = microtime();

            foreach($param as $key => $value){
                $explode = explode("#-#",$value);
                if ($explode[0]){
                    $keys .= $explode[0] . ",";
                    $values .=  $explode[1] . ",";
                    $param[$explode[0]] =  $explode[1];
                   }
            }
            $keys = rtrim($keys, ",");
            $values = rtrim($values, ",");
            echo $prepareInsertQry = "insert into " . $table . " (".$keys.",json) values ( ".$values.",'".$time."' );#_#";//#_#";
            $res = mysql_query($prepareInsertQry);




            if (!$res)
            {
                return false;
            }
            return $param;
        }

        function update($param,$table){
            $set = "";
            $values = "";
            $keys  = "";
               $time = microtime();

            foreach($param as $key => $value){
                $explode = explode("#-#",$value);
                if ($explode[0]){
                    $param[$explode[0]] =  $explode[1];
                    $set .= $explode[0] . " = " . $explode[1] . ",";
                   }
            }

            $set .= "json = '" . $time . "'";

             $prepareInsertQry = "update " . $table . " set ".$set . " where id_web = ".$_REQUEST['id_web'].";";
             $res = mysql_query($prepareInsertQry);
             echo $prepareInsertQry .= "#_#";




            if (!$res)
            {
                return false;
            }
            return $param;
        }
?>