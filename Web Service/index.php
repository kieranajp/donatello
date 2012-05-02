<?php
  require_once('api/Database.class.php');
  $db = new Database();
?>
<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8">
  <meta name="Author" content="Kieran Patel (i7869019)">
  <title>Donatello test dashboard</title>
</head>
<body>
  <h1>Donatello Demos</h1>
  <p>This page demonstrates some functionality of the Donatello API.</p>
  <h3>Actions:</h3>
  <ul>
    <li><button onClick="document.location.href='purchase.php';">Purchase</button><br></li>
    <li><button onClick="document.location.href='insertProduct.php';">Insert new product</button><br></li>
    <li><button onClick="document.location.href='revoke.php';">Revoke authentication of a product</button><br></li>
  </ul>
  <h3>Data retrieval (JSON)</h3>
  <ul>
    <li>
      <?php
        echo '<form action="api/getAuthorisations.php" method="post" accept-charset="utf-8">';
        echo '<select name="account">';
        $query = 'SELECT * FROM accounts';
        $result = mysql_query($query);
        
        while ($row = mysql_fetch_array($result)) {
          echo '<option value="'.$row['account_id'].'">'.$row['account_id'].'</option>';
        }
        echo '</select>';
        echo '<input type="submit" value="Get Authorisations for this account"></form>'
      ?>
    </li>
    <li>
      <?php
        echo '<form action="api/getProducts.php" method="post" accept-charset="utf-8">';
        echo '<select name="product">';
        $query = 'SELECT * FROM products';
        $result = mysql_query($query);
        echo '<option value="">Get all products</option>';
        while ($row = mysql_fetch_array($result)) {
          echo '<option value='.$row['product_id'].'>'.$row['product_nm'].'</option>';
        }
        echo '</select>';
        echo '<input type="submit" value="Get Products"></form>'
      ?>
    </li>
    <li>
      <?php
        echo '<form action="api/getProducts.php" method="post" accept-charset="utf-8">';
        echo '<select name="product">';
        $query = 'SELECT DISTINCT product_type FROM products';
        $result = mysql_query($query);
        echo '<option value="">Get all products</option>';
        while ($row = mysql_fetch_array($result)) {
          echo '<option value='.$row['product_type'].'>'.$row['product_type'].'</option>';
        }
        echo '</select>';
        echo '<input type="submit" value="Get Products by Type"></form>'
      ?>
    </li>
  </ul>
</body>
</html>
