<?php
  require_once('Database.class.php');
  
  if(isset($_REQUEST['product']) && intval($_REQUEST['product'])) {
    $query = "SELECT * FROM products WHERE product_id = ".$_REQUEST['product'];
  } elseif(isset($_REQUEST['type'])) {
    $query = "SELECT * FROM products WHERE product_type = ".$_REQUEST['type'];
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
  header('Content-type: application/json');
  echo json_encode(array('products'=>$products));
