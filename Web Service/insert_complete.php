<html>
<head>
  <title></title>
</head>
<body>
  <h1>Insertion complete!</h1>
  <p>Your location hash is <pre><?php echo $_GET['location']; ?></pre></p>
  <p>The file should be uploaded to the FTP in donatello/<?php echo $_GET['location']; echo "/"; echo $_GET['location']; echo ".file";?>
  <a href="index.php">Back to Dashboard</a>
</body>
</html>