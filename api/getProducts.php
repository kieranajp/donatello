<?php
  require_once('Database.class.php');
  
  if(isset($_GET['product']) && intval($_GET['product'])) {
    $query = "SELECT * FROM products WHERE id = ".$_GET['product'];
  } elseif(isset($_GET['type'])) {
    $query = "SELECT * FROM products WHERE type = ".$_GET['type'];
  } else {
    $query = "SELECT * FROM products;";
  }

  $db = new Database();
  $result = mysql_query($query) or die ('Error in query: '.$query);

  $products = array();
  if(mysql_num_rows($result)) {
    while ($product = mysql_fetch_assoc($result)) {
      $products[] = array('product'=>$product);
    }
  }

  //header('Content-type: application/json');
  echo json_encode(array('products'=>$products));
