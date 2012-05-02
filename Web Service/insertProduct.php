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
  <form action="api/insert.php" method="post" accept-charset="utf-8">
    <?php
      $query = "SELECT * FROM locations";
      $result = mysql_query($query) or die ('Error in query: '.$query.' - '.mysql_error());
      $locs = mysql_num_rows($result);
      
      echo 'Location: <input type="number" name="location" min="'.$locs.'" value="'.$locs.'"><br>';
    ?>
    Type: <select name="type">
      <option value="movie" selected="true">Movie</option>
      <option value="game" selected="true">Game</option>
      <option value="software" selected="true">Software</option>
      <option value="episode" selected="true">TV Series</option>
      <option value="zip" selected="true">Zip archive</option>
    </select><br>
    Title: <input name="title"><br>
    Description: <textarea rows="10" cols="30" name="description"></textarea><br>
    <input type="submit" value="submit">
    Hash: <input name="hash">
  </form>
</body> 
</html>