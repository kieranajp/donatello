<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>Remote Delivery of Large Media Files</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="Kieran Patel (i7869019)">
    
     <!--[if lt IE 9]>
       <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
     <![endif]-->
    <link href="prettify.css" type="text/css" rel="stylesheet" />
    <link href="standard.css" rel="stylesheet">
    <style type="text/css">
      body {
        counter-reset: a;
        counter-reset: t_H1;
      }
      @page {
        margin: 2cm 2cm;
      }
      @media print {
        .noprint { display: none; }
      }
      @media screen {
        body {
        }
        section { height: 0; overflow: hidden; }
        section.active { height: auto; }
        .noscreen { display: none; }
        #view {
          margin: 40px auto;
          width: 440pt;
          padding: 40px 0 0 400px;
        }
        #view.print_view section {height: auto;}
      }
      section {
        counter-increment: a;
      }
      h1 {
        page-break-before: always;
        counter-reset: b;
      }
      h1:before {
        content: counter(a) ". ";
      }
      h2 {
        counter-reset: c;
      }
      h2:before {
        content: counter(a) "." counter(b) " ";
        counter-increment: b;
      }
      h3:before {
        content: counter(a) "." counter(b) "." counter(c) " ";
        counter-increment: c;
      }

      /** toc **/
      .toc {
        width: 480px;
        margin: auto;
        padding: 20px;
      }
      .toc li {
        position: relative;
        list-style-type: none;
        height: 25px;
      }
      .toc a, .toc span {
        background: #fff;
        text-decoration:none;
      }
      .toc a {
        position: absolute;
        border-bottom: dotted 1px #AAA;
        left: 0;
        right: 0;
        top: 0;
        bottom: 0;
      }
      .toc span {
        position: absolute;
        padding: 0 4px;
        bottom: -1px;
      }
      .toc span.page {
        right: 0;
      }
      .toc .H2 span.title {
        padding-left: 40px;
      }
      .toc .H3 span.title {
        padding-left: 80px;
      }      
      .toc .H1 {
        counter-reset: t_H2;
      }
      .toc .H1 span.title:before {
        content: counter(t_H1) ". ";
        counter-increment: t_H1;
      }
      .toc .H2 {
        counter-reset: t_H3;
      }
      .toc .H2 span.title:before {
        content: counter(t_H1) "." counter(t_H2) "  ";
        counter-increment: t_H2;
      }
      .toc .H3 span.title:before {
        content: counter(t_H1) "." counter(t_H2) "." counter(t_H3) "  ";
        counter-increment: t_H3;
      }     
      
      nav .toc {
        width: auto;
      }
      nav .toc a {
        border: none;
      }
      nav .toc span {
        padding: inherit;
        bottom: inherit;
      }
      nav .toc span.page {
        display: none;
      }

      /*figcaption:before {
        content: "Figure " counter(fig) ": ";
        counter-increment: fig;
      }*/
    </style>
  </head>
  <body onload="prettyPrint()">
    <header class="noprint">
      <ul>
        <li class="active"><a href="#html">HTML5 view</a></li>
        <li><a href="#print">print view</a></li>
      </ul>
    </header>
    <nav class="noprint">
      <ul class="toc">
      </ul>
    </nav>
    <div id="view">
      <ul class="toc noscreen">
      </ul>
<?php
if ($handle = opendir('../src')) {
    require_once '../markdown.php';    
    while (false !== ($entry = readdir($handle))) {
        if ($entry != "." && $entry != "..") {
            $info = pathinfo('../src/'.$entry);
            if ($info['extension'] == "md" || $info['extension'] == "markdown") {
                $name = mb_substr($info['filename'], 3);
                $contents = file_get_contents('../src/'.$entry);
                //if bibliography, sort
                if (strpos($name,'Bibliography') !== false) {
                  $sorted = explode("\n- ", substr($contents, 2));
                  sort($sorted, SORT_STRING);
                  $contents = "- ". implode("\n- ", $sorted);
                }
                //if abstract then append .heading not h1 (no numbering)
                //$src = (($name == "Abstract") ? "<div class=\"heading\">$name</div>" : "# $name") . "\r\n$contents";
                $src = "# $name\r\n$contents";
                echo '<section id="' . str_replace(" ", "", $name) . '">'. Markdown($src) . '</section>';
            }
        }
    }
    closedir($handle);
}
?>
    </div>
    <footer>
      <a id="prev" href="#prev">&lt;&lt; Previous Chapter</a>
      <a id="next" href="#next">Next Chapter &gt;&gt;</a>
    </footer>
    <script type="text/javascript" src="prettify.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script>
    var toc;
    $(function() {
      //load toc
      $.getJSON("../toc.json", function(data) {
        toc = data;
        $('section').each(function(i){
          var section = this.id;
          var section_name = $(this).children('h1').first().text();
          //if no section name ignore (i.e. abstract)
          if (section_name.length !== 0) {
            $('.toc').append('<li class="H1"><a href="#' + section + '"><span class="title">' + section_name + '</span><span class="page">' + data[section]['page'] + '</span></a></li>');
            $('#sections').append('<li><a href="#' + section + '">' + section_name + '</a></li>');
            var counters = {"H2":0, "H3":0};
            $('#' + section).children("h2, h3").each(function(){
              var id = section + '-' + this.tagName.substr(1) + '-' + ++counters[this.tagName];
              $(this).prop('id', id);
              var li = '<li class="' + this.tagName + '"><a href="#' + id + '"><span class="title">' + $(this).text() + '</span><span class="page">' + data[section][id] + '</span></a></li>';
              $('.toc').append(li);
            });
          }
        });

        //header events
        $('header li a').click(function() {
          if (!$(this).parent().hasClass("active")) {    
            $('header li').removeClass("active");
            $(this).parent().addClass("active");
            $("#view").toggleClass("print_view");
          }
        });

        //navbar events
        $('nav .toc a').click(function() {
          var target = ($(this).parent().hasClass("H1")) ? this : $(this).parent().prevAll(".H1").first().children("a");
          var section = $(target).attr('href');
          if (!$(section).hasClass("active")) {
            $('section').removeClass("active");
            $(section).addClass("active");
          }
        });

        //footer buttons
        $('#prev').click(function() {
          var prev = $('section.active').first().prevAll("section").first();
          if (prev.length !== 0) {            
            $('section').removeClass("active");
            prev.addClass("active");
            setTimeout("$('body').scrollTop(0)");
          }
        });
        $('#next').click(function() {
          var next = $('section.active').first().nextAll("section").first();
          if (next.length !== 0) {            
            $('section').removeClass("active");
            next.addClass("active");
            setTimeout("$('body').scrollTop(0)");
          }
        });

        $('section').first().addClass("active");
      });
    });
    </script>
</body>
</html>