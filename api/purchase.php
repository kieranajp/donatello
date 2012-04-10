<?php
  require_once('Database.class.php');
  
  if(isset($_POST['product']) && intval($_POST['product']) && isset($_POST['account']) && intval($_POST['account'])) {
    // and if valid token supplied?
    $db = new Database();
    $query = 'INSERT INTO authentications (product_id, account_id) VALUES '.$_POST['product'].', '.$_POST['account'];
    $result = mysql_query($query) or die ('Error in query: '.$query.' - '.mysql_error());
    
    $query = 'SELECT client_ip FROM clients WHERE account_id = '.$_POST['account'];
    $result = mysql_query($query);
    $ip = mysql_fetch_assoc($result);
    
    $ip = inet_ntop($ip);
    
    $query = 'SELECT location_hash FROM products WHERE product_id ='.$_POST['product'];
    $result = mysql_query($query);
    $loc_id = mysql_fetch_assoc($result);
    
    $query = 'SELECT * FROM locations';
    $result = mysql_query($query);
    
    while ($row = mysql_fetch_array($result)) {
      if ($hash = md5($row['loc_id']) == $loc_id) {
        $query = 'SELECT loc FROM locations WHERE loc_id = '.$row['loc_id'];
        break;
      }
    }
    
    if (strpos($query, 'WHERE') === false) {
      return false;
    }
    
    $result = mysql_query($query);
    $loc = mysql_fetch_assoc($result);

  	$port = 31337;
  	set_time_limit(0);

  	$socket = socket_create(AF_INET, SOCK_STREAM, 0) or die ("Could not create socket\n");
  	$result = socket_connect($socket, $host, $port) or die ("Could not connect to address\n");
  	
    
  } else {
    return false;
  }