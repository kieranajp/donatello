<?php
  class Database {
    private $_connect;
  
    public function __construct() {
      $this->connect = mysql_connect("10.0.1.145", "fypu", "eijonu") or die ("Error connecting to database server");
      //$this->connect = mysql_connect("127.0.0.1","crud","eijonu") or die ("Error connecting to database server");
      mysql_select_db("fyp",$this->connect) or die ("Error finding database");
    }
  
    public function __destruct() {
      mysql_close( $this->connect );
    }
  }