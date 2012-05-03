<?php
$start = microtime(true);
require_once 'markdown.php';

//get compile types
$do_html = (array_search("html", $argv) !== false);
$do_toc = (array_search("toc", $argv) !== false);
$do_tof = (array_search("tof", $argv) !== false);
$do_app = (array_search("app", $argv) !== false);

$words = $toc = $tof = array();
$toc_i = 1;
$f = 0;

//compile Appendices
if ($do_app) {
  $fh = fopen("src/ZZ-Appendices.html", "w");
  fwrite($fh, get_dir("src/Appendices"));
  fclose($fh);
}

if ($handle = opendir('src')) {
  //markdown
	while (false !== ($entry = readdir($handle))) {
		if ($entry != "." && $entry != ".." && $entry != ".DS_Store" && !is_dir($file = 'src/'.$entry)) {
			$info = pathinfo($file);
      $name = mb_substr($info['filename'], 3);
      $html = "";
			if ($info['extension'] == "md" || $info['extension'] == "markdown") {
				$src = "# $name\r\n".file_get_contents($file);
        $html = Markdown($src);
      }
      else if ($info['extension'] == "html") $html = file_get_contents($file);
			//dont include invalid sections in body count
			$body_count = (!in_array($name, array("Abstract", "References", "Appendices"))) ? word_count($html, true) : 0;
			$words[$name] = array(word_count($html), $body_count);
			if ($do_toc || $do_tof) {
				$dom = new DOMDocument();
				$dom->loadHTML($html);

				//create unique id incase of duplicate heading names
				$h2 = 0;
				$h3 = 0;

        $section = str_replace(" ", "", $name);
				//h1 is filename
				if ($section != "") {
  				$toc[$section]['page'] = 0;

  				foreach($dom->getElementsByTagName('h2') as $h) {
  					++$h2;
  					$toc[$section]["$section-2-$h2"] = 0;
  				}
  				foreach($dom->getElementsByTagName('h3') as $h) {
  					++$h3;
  					$toc[$section]["$section-3-$h3"] = 0;
  				}
          foreach($dom->getElementsByTagName('figcaption') as $fig) {
            ++$f;
            //remove anything after [ or >60 chars
            $caption = (($pos = strpos($fig->nodeValue, "[")) !== false) ? substr($fig->nodeValue, 0, $pos) : $fig->nodeValue;
            if (strlen($caption) > 60) $caption = substr($caption, 0, 60) . "..."; 
            $tof[] = array("Figure $f: $caption", 0);
          }      
        }     
      }
		}
	}
	closedir($handle);

	if ($do_html) {
		//get 'compiled' version of print.php
		exec("wget -O html/index.html http://10.0.1.49/~Kieran/Write-up/html/print.php");
	}
	if ($do_toc) {
		//update table of contents
		$fh = fopen("toc.json", "w");
		fwrite($fh, json_encode($toc));
		fclose($fh);
	}
  if ($do_tof) {
    //update list of figures
    $fh = fopen("tof.json", "w");
    fwrite($fh, json_encode($tof));
    fclose($fh);
  }

	echo "\r\nCompiled ". sizeof($words) ." in ". round(microtime(true) - $start, 2) ." seconds.\r\n\r\nWord Counts:\r\n";
	$total = 0;
	$body = 0;
	foreach ($words as $name => $w) {
		echo "\r\n$name => ". $w[0] . " (body: +". $w[1] .")";
		$total += $w[0];
		$body += $w[1];
	}
	echo "\r\n\r\nBody Count = $body words\r\nTotal Count = $total words\r\n";

	//write to words.json
	$fh = fopen("words.json", "w");
	fwrite($fh, json_encode(array("body"=>$body, "total"=>$total, "chapters"=>$words)));
	fclose($fh);
}

function word_count($src, $body = false) {
  //if body count, remove invalid elements (figures, headings)
  if ($body) {
    for ($i = 0; $i < strlen($src); $i++) {
      if (substr($src, $i, 1) == "<") {
        $tag_end = (strpos($src, ">", $i) > strpos($src, " ", $i)) ? strpos($src, " ", $i) : strpos($src, ">", $i);
        $tag = strtolower(substr($src, $i + 1, $tag_end - $i - 1));
        $element_end = strpos($src, "</$tag>", $i) + strlen("</$tag>");
        if (in_array($tag, array("figure", "h1", "h2", "h3", "h4"))) $src = substr($src, 0, $i) . substr($src, $element_end);
      }
    }
  }
  return str_word_count(strip_tags($src), 0);
}
function get_dir($dir, $level = 2) {
  $html = "";
  if ($handle = opendir($dir)) {  
    while (false !== ($entry = readdir($handle))) {
      if ($entry != "." && $entry != ".." && $entry != ".DS_Store") {
        if (is_dir("$dir/$entry")) {          
          $html .= "<h$level>$entry</h$level>" . get_dir("$dir/$entry", $level + 1);
        }
        else {
          $info = pathinfo($dir.$entry);
          $name = $info['filename'];
          //if image, just put in img element
          if (in_array($info['extension'], array("jpg", "jpeg", "png", "gif", "tiff"))) {
            $contents = "<img src=\"img/$entry\" />";
            $entry = substr($entry, 0, strrpos($entry, "."));
            $entry = str_replace("_", " ",$entry);
          }
          //if html just grab contents
          else if ($info['extension'] == "html" || $info['extension'] == "") $contents = file_get_contents("$dir/$entry");
          else {
            $contents = htmlspecialchars(file_get_contents("$dir/$entry"));
            //if code put in prettyprint block
            $contents = (in_array($info['extension'], array("php", "json", "css", "html", "js", "cs"))) ? "<pre class=\"prettyprint linenums\">". $contents ."</pre>" : "<pre>$contents</pre>";
          }
          $html .= "<h$level>$entry</h$level>$contents";
        }
      }
    }
    closedir($handle);
  }
  return $html;
}