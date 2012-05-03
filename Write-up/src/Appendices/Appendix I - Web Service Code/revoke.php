<?php
  require_once('api/Database.class.php');
  $db = new Database();
?>
<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8">
  <title>Example purchase page</title>
</head>
<body>
  <form action="api/revoke.php" method="post" accept-charset="utf-8">
    <select name="account">
    <?php
      $query = 'SELECT * FROM accounts';
      $result = mysql_query($query);
      
      while ($row = mysql_fetch_array($result)) {
        echo '<option value="'.$row['account_id'].'">'.$row['account_id'].'</option>';
      }
      echo '</select>';

      echo '<select name="product">';

      $query = 'SELECT * FROM products';
      $result = mysql_query($query);

      while ($row = mysql_fetch_array($result)) {
        echo '<option value='.$row['product_id'].'>'.$row['product_nm'].'</option>';
      }

      echo '</select>';
    ?>
    <input type="submit" value="submit">
  </form>
</body> 
</html>