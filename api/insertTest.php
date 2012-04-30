<?php
  require_once('Database.class.php');
  $db = new Database();
  
  $i = ; // Set the product's location_id here
  
  $type = ""; // Define a Type string here
  $name = ""; // Define the product's name here
  $dtl = ""; // Product's details
  $price = ; // Define the price here, as an integer (e.g. £34.99 -> 3499)
  $rating = ; // Define the product's age rating here
  $hash = md5_file(''); // Point this at the file to upload
  $location_hash = md5((string)$i);
  
  $name = mysql_real_escape_string($name);
  $dtl = mysql_real_escape_string($dtl);
  
  $query = "INSERT INTO products (product_type, product_nm, product_dtl, product_price, product_rating, product_hash, location_hash) VALUES ('$type', '$name', '$dtl', $price, $rating, '$hash', '$location_hash')";
  
  echo $query."\r\n\r\n";
  $result = mysql_query($query) or die('Error in query');
  
?>