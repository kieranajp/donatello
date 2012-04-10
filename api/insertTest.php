<?php
  require_once('Database.class.php');
  $db = new Database();
  
  $i = 3;
  
  $type = "game";
  $name = "Batman: Arkham City";
  $dtl = "Developed by Rocksteady Studios, Batman: Arkham City builds upon the intense, atmospheric foundation of Batman: Arkham Asylum, sending players soaring into Arkham City, the new maximum security 'home' for all of Gotham City's thugs, gangsters and insane criminal masterminds.

  Set inside the heavily fortified walls of a sprawling district in the heart of Gotham City, this highly anticipated sequel introduces a brand-new story that draws together a new all-star cast of classic characters and murderous villains from the Batman universe, as well as a vast range of new and enhanced gameplay features to deliver the ultimate experience as the Dark Knight.

  Batman: Arkham City is the follow-up to the award-winning hit video game Batman: Arkham Asylum and delivers an authentic and gritty Batman experience
  - Become the Dark Knight: Batman: Arkham City delivers a genuinely authentic Batman experience with advanced, compelling gameplay on every level: high-impact street brawls, nail-biting stealth, multifaceted forensic investigation, epic super-villain encounters and unexpected glimpses into Batman's tortured psychology
  - Advanced FreeFlow combat: Batman faces highly coordinated, simultaneous attacks from every direction as Arkham's gangs bring heavy weapons and all-new AI to the fight, but Batman steps it up with twice the number of combat animations and double the range of attacks, counters and takedowns
  - New gadgets: Batman has access to new gadgets such as the Cryptographic Sequencer V2 and Smoke Pellets, as well as new functionality for existing gadgets that expand the range of Batman's abilities without adding extra weight to his Utility Belt";
  $price = 2999;
  $rating = 15;
  $hash = md5_file('/Users/Kieran/Desktop/Batman_Arkham_City.file');
  $location_hash = md5((string)$i);
  
  $name = mysql_real_escape_string($name);
  $dtl = mysql_real_escape_string($dtl);
  
  $query = "INSERT INTO products (product_type, product_nm, product_dtl, product_price, product_rating, product_hash, location_hash) VALUES ('$type', '$name', '$dtl', $price, $rating, '$hash', '$location_hash')";
  
  echo $query."\r\n\r\n";
  $result = mysql_query($query) or die('Error in query');
  
?>