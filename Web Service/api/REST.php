<?php
  /*
   * One of this project's MoSCoW Could-Have requirements was a full RESTful API.
   * This work is the beginnings of that API, however due to some setbacks detailed in the write-up, it never was finished.
   * All other files in this folder are part of a functional web service. This one is however not ready for use.
   */
  require_once('Database.class.php');
  switch ($_REQUEST['method']) {
    case 'purchase':
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
          echo "Error: invalid API call";
        }
      break;
    case 'getProducts':
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
      break;
    case 'getAuthorisations':
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
      break;
    default:
      echo 'Error: Invalid API call';
      break;
  }
?>