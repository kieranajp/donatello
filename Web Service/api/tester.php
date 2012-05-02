<?php
  $host = "10.0.1.145";
  $port = 31337;
  set_time_limit(0);
  
  if ($argc > 1) {
    $id = md5($argv[1]);
  }
  else {
    exit(1);
  }
    
  $socket = socket_create(AF_INET, SOCK_STREAM, 0) or die ("Could not create socket\n");
  $result = socket_connect($socket, $host, $port) or die ("Could not connect to address\n");
    
  if ($result !== false) {
    echo "connected!\n";
    socket_send($socket, $id, 1024, 0);
  }
  
  socket_close($socket);