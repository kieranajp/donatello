<?php
  require_once('Database.class.php');

  if(isset($_POST['product']) && intval($_POST['product']) && isset($_POST['account'])) {
    // COULD-HAVE: and if valid token supplied?
    $db = new Database();

    $account = mysql_real_escape_string($_POST['account']);

    $query = 'SELECT product_id FROM authentications WHERE account_id = "'.$account.'" AND product_id = '.$_POST['product'].';';
    $result = mysql_query($query) or die ('Error in query: '.$query.' - '.mysql_error());
    $purchased = mysql_num_rows($result);

    if ($purchased != 0) {
      // This product is already owned by that account
      return false;
    }

    // Authenticate the product for the account
    $query = 'INSERT INTO authentications (product_id, account_id) VALUES ('.$_POST['product'].', "'.$account.'")';
    $result = mysql_query($query) or die ('Error in query: '.$query.' - '.mysql_error());
    
    // Find the location of the file to download
    $query = 'SELECT location_hash FROM products WHERE product_id ='.$_POST['product'];
    $result = mysql_query($query) or die ('Error in query: '.$query.' - '.mysql_error());;
    $loc_id = mysql_fetch_assoc($result);
    var_dump($loc_id);
    
    $query = 'SELECT * FROM locations';
    $result = mysql_query($query) or die ('Error in query: '.$query.' - '.mysql_error());;
    
    while ($row = mysql_fetch_array($result)) {
      if ($hash = md5($row['location_id']) == $loc_id['location_hash']) {
        $query = 'SELECT location FROM locations WHERE location_id = '.$row['location_id'];
        break;
      }
    }
    
    if (strpos($query, 'WHERE') === false) {
      return false;
    }
    
    $result = mysql_query($query);
    $loc = mysql_fetch_assoc($result);
    $loc = $loc['location'];
    
    // Find the IP to send the download to
    $query = 'SELECT client_ip FROM clients WHERE account_id = "'.$account.'"';
    $result = mysql_query($query);
    $ip = mysql_fetch_assoc($result);

    // Send the download!
    if ($ip != 0) {
      $ip = inet_ntop($ip);
      var_dump($ip);

    	$port = 31337;
    	set_time_limit(0);

    	$socket = socket_create(AF_INET, SOCK_STREAM, 0) or die ("Could not create socket\n");
    	$result = socket_connect($socket, $ip, $port) or die ("Could not connect to address\n");
      
    	// If a connection can't be made they'll have to download from the Redownloads screen.
      if ($result !== false) {
        echo "connected!\n";
        socket_send($socket, $id, 1024, 0);
      }
  
      socket_close($socket);
  	}

    header('../purchase_complete.php');
  } else {
    return false;
  }