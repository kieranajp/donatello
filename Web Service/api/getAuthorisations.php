<?php
  require_once('Database.class.php');
  
  $query = "";
  
  if(isset($_REQUEST['account'])) {
    $query = "SELECT * FROM authentications WHERE account_id = '".$_REQUEST['account']."';";
  }
  else {
    return false;
  }
  
  $db = new Database();
  $result = mysql_query($query) or die ('Error in query: '.$query);

  $auths = array();
  if(mysql_num_rows($result)) {
    while ($auth = mysql_fetch_assoc($result)) {
      $auths[] = array('auth'=>$auth);
    }
  }
  header('Content-type: application/json');
  echo json_encode(array('authorisations'=>$auths));