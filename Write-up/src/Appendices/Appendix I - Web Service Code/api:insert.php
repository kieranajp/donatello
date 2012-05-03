<?php
  require_once('Database.class.php');
  $db = new Database();
  
  $i = $_POST['location_id'];
  
  $type = $_POST['type'];
  $name = mysql_real_escape_string($_POST['title']);
  $dtl = mysql_real_escape_string($_POST['description']);
  $price = 2999;
  $rating = 15;
  // Sadly this feature had to be taken out
  //$hash = md5_file('..');
  $hash = $_POST['hash'];
  $location_hash = md5((string)$i);
  
  $query = "INSERT INTO products (product_type, product_nm, product_dtl, product_price, product_rating, product_hash, location_hash) VALUES ('$type', '$name', '$dtl', $price, $rating, '$hash', '$location_hash')";
  
  $result = mysql_query($query) or die('Error in query');
  
  $lochash = md5('$query');
  
  $query = "INSERT INTO locations (location_id, location) VALUES ($i, '$lochash')";
  $result = mysql_query($query) or die('Error in query');
  
  header('../insert_successful.php?location='.$lochash);
?>