<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>Remote Downloading of Large Media Files</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="Kieran Patel (i7869019)">
    

    <link href="prettify.css" type="text/css" rel="stylesheet" />
    <link href="standard.css" type="text/css" rel="stylesheet" />
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
        pre.prettyprint.linenums {
          box-shadow: none;
        }
        pre.prettyprint.linenums li {
          border-bottom: 1px dotted #EEE;
        }
        body {
          margin-left: 2cm;
        }
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
          padding: 40px 0 20px 400px;
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
        counter-reset: t_H1;
        width: 90%;
        padding: 20px;
      }
      .toc li, .tof li {
        list-style-type: none;
        height: 25px;
        overflow: hidden;
      }
      .tof li {
      }
      .toc a, .toc span {
        background: #fff;
        text-decoration:none;
        display: inline-block;
      }
      .toc a {
        width: 100%;
      }
      .tof span.title {
        padding-right: 25px;
      }
      .toc span, .tof span {
        padding: 0 4px;
        page-break-inside: avoid;
      }
      li span.page {
        float: right;
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

      #section {
        page-break-before: always;
      }

      #Appendices h1, #Appendices h2 {
        page-break-before: always;
      }

      /*figcaption:before {
        content: "Figure " counter(fig) ": ";
        counter-increment: fig;
      }*/
    </style>
  </head>
  <body>
    <header class="noprint">
      <ul>
        <li class="active"><a href="#html">HTML5 view</a></li>
        <li><a href="#print">print view</a></li>
      </ul>
      <span id="chapter"></span>
    </header>
    <nav class="noprint">
      <ul class="toc">
      </ul>
    </nav>
    <div id="view">
      <ul class="toc noscreen">
      </ul>
      <div id="section" class="noscreen">
        <ul class="tof">
        </ul>
      </div>
<?php
$start_time = microtime(true);
if ($handle = opendir('../src')) {
  require_once '../markdown.php';    
  while (false !== ($entry = readdir($handle))) {
    if ($entry != "." && $entry != ".." && $entry != ".DS_Store" && !is_dir($file = '../src/'.$entry)) {
      $info = pathinfo($file);
      $name = $src = "";
      $name = mb_substr($info['filename'], 3);
      $contents = file_get_contents($file);
      if ($info['extension'] == "md" || $info['extension'] == "markdown") {
        //if References, sort
        if (strpos($name,'References') !== false) {
          $sorted = explode("\n- ", substr($contents, 2));
          sort($sorted, SORT_STRING);
          $contents = "- ". implode("\n- ", $sorted);
        }
        $src = Markdown($contents);
      }
      else if ($info['extension'] == "html") $src = "$contents";
      echo '<section id="' . str_replace(" ", "", $name) . '"><h1>'. $name ,'</h1>'. $src . '</section>';
    }
  }
  closedir($handle);
}
?>
      <div id="word_count" class="noscreen">
        <h1>Word Counts</h1>
        <table class="table-striped">
          <tr>
            <th>Chapter</th>
            <th>Total Words</th>
            <th>Body Count</th>
          </tr>
        </table>
      </div>
    </div>
    <footer class="noprint">
      <div>
        <a id="prev" href="#prev">&lt;&lt; Previous Chapter</a>
        <a id="next" href="#next">Next Chapter &gt;&gt;</a>
      </div>
    </footer>
    <script type="text/javascript" src="prettify.js"></script>
    <script type="text/javascript" src="jquery.min.js"></script>
    <script>
    var toc;
    console.log("compile time: <?php echo round(microtime(true) - $start_time, 2) ?> seconds");
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

        //list of figures
        $.getJSON("../tof.json", function(data) {
          tof = data;
          for(var i = 0; i < data.length; ++i) $('.tof').append('<li><span class="title">' + data[i][0] + '</span><span class="page">' + data[i][1] + '</span></li>');
        });

        //word count
        $.getJSON("../words.json", function(data) {
          words = data;
          for(var chapter in data.chapters) $('#word_count table').append('<tr><td>' + chapter + '</td><td>' + data.chapters[chapter][0] + '</td><td>+' + data.chapters[chapter][1] + '</td></tr>');
          $('#word_count table').append('<tr><th>Total:</th><td>' + data['total'] + '</td><td>' + data['body'] + '</td></tr>');
          $('section:last').prevAll('section').eq(1).append('<p style="text-align:center;"><em>Total word count, excluding appendices, figures, bibliography and table of contents, is ' + data['body'] + ' words.</em></p>');
        });

        //header events
        $('header li a').click(function() {
          if (!$(this).parent().hasClass("active")) {    
            $('header li').removeClass("active");
            $(this).parent().addClass("active");
            $("#view").toggleClass("print_view");
            $('#chapter, .noscreen').toggle();
          }
        });

        //navbar events
        $('nav .toc a').click(function() {
          var target = ($(this).parent().hasClass("H1")) ? this : $(this).parent().prevAll(".H1").first().children("a");
          var section = $(target).attr('href');
          if (!$(section).hasClass("active")) {
            $('section').removeClass("active");
            $(section).addClass("active");
            $('#chapter').text($(section + ' h1').first().text());
          }
        });

        //footer buttons
        $('#prev').click(function() {
          var prev = $('section.active').first().prevAll("section").first();
          if (prev.length !== 0) {            
            $('section').removeClass("active");
            prev.addClass("active");
            setTimeout("$('body').scrollTop(0)");
            $('#chapter').text(prev.children('h1').first().text());
          }
        });
        $('#next').click(function() {
          var next = $('section.active').first().nextAll("section").first();
          if (next.length !== 0) {            
            $('section').removeClass("active");
            next.addClass("active");
            setTimeout("$('body').scrollTop(0)");
            $('#chapter').text(next.children('h1').first().text());
          }
        });

        //figure numbers & refs
        $('figure').each(function(i) {
          var figure = 'Figure ' + (i + 1);
          $(this).children('figcaption').prepend(figure + ': ');
          $('a.figref[href="#' + this.id + '"]').text(figure);
        });

        //show first chapter
        $('#chapter').text($('section').first().addClass("active").children('h1').text());

        //prettify code
        prettyPrint();
      });
    });
    </script>
</body>
</html>