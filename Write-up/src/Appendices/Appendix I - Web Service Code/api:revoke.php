<?php
  require_once('Database.class.php');

  if(isset($_POST['product']) && intval($_POST['product']) && isset($_POST['account'])) {
    $query = "UPDATE authentications SET auth_rvkd = 1 WHERE product_id = ".$_POST['product']." AND account_id = '".$_POST['account']."';";
    
    $result = mysql_query($query) or die('Error in query');
    
    header('Location: revoke_successful.php');
  }