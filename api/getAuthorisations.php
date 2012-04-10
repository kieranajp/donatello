<?php
  require_once('Database.class.php');
  
  $query = "";
  
  if(isset($_GET['account']) && intval($_GET['account'])) {
    $query = "SELECT * FROM authorisations WHERE account_id = ".$_GET['account'];
  }
  else {
    return false;
  }
  
  $db = new Database();
  $result = mysql_query($query) or die ('Error in query: '.$query);

  $auths = array();
  if(mysql_num_rows($result)) {
    while ($auths = mysql_fetch_assoc($result)) {
      $auths[] = array('auth'=>$auth);
    }
  }

  echo json_encode(array('authorisations'=>$auths));