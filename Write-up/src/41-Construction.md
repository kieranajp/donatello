##Development Plan
As stated previously, the project will be completed using a bespoke software development lifecycle adapted to bet fit a one-person development team. As far as implementation is concerned, the project will be divided into four sections to work on as sprints, similar to the agile Scrum methodology, however the overall process going from design to testing will be more linear and reminiscent of Waterfall.

<div>
<figure>
<img src="img/sdlc.png">
<figcaption>The custom SDLC this project will adhere to.</figcaption>
</figure>
</div>

The idea of this SDLC is to break everything down into manageable chunks and deal with it one piece at a time. By dealing with all major design work before beginning implementation, the project will have a clear direction before any code is written, and since the requirements are very unlikely to change (there is no client setting them), the rigid structure of Waterfall is not really a drawback. However, using the Scrum tactic of splitting the work up into sprints, at least for the software implementation, allows each work unit to be worked and concentrated on fully, and will hopefully prove this SDLC experiment of sorts a success.

The Scrum part of the SDLC will work as follows:

*	Sprint design: Objectives for the sprint will be laid out - i.e. which features will be implemented, and how long the sprint will take.
*	Sprint implementation: Spend the amount of time that was defined creating the features that were designated for this sprint.

Testing during a sprint will be limited to basic identification and fixing of major bugs; that is, unit testing and extensive black-box testing will be saved until after implementation is wholly complete (in accordance with the Waterfall methodology), but any major issues that arise during a sprint should be dealt with there and then.

The four sprints, then, will be split in accordance with the functionality of the application. Each sprint will complete another major feature of the application, until a complete product has been created ready for testing and integration. The sprints will be as follows:

1.	Database design and creation  
2.	GUI creation and account management tools
3.	Web service creation
4.	Download feature
5.	File management feature and refactor.

Each sprint will take either one or two weeks depending on its complexity, and more specific objectives will be drawn up before each sprint commences. Each sprint will be designed to fulfil one or more high-priority requirements.

##Design
###Requirements
One of the first, most important stages of a project's design comes in defining the project's requirements. Using the MoSCoW methodology along with some tips from Volere, identifying and prioritising requirements was performed in the initial planning phase of the SDLC. For each category in MoSCoW, functional and non-functional requirements were thought up; Volere was instrumental in instructing about the importance of non-functional requirements in software engineering. In this project, every attempt will be made to meet all the Must-Have and Should-Have requirements, and Could-Haves will be dealt with opportunistically, if there is time towards the end of a sprint when all more essential functionality has been created (and non-functional Must-Have and Should-Have requirements have been met, too). Indeed, the project will only be considered a success if all the Must- and Should-Have requirements have been met. Won't-Have requirements, for the purposes of this project at least, are being defined as requirements that will not make it into the dissertation, but would be nice-to-haves if the project were to be extended in future. They are mostly noted down here with some forethought to the evaluation phase.

<figure>
**Must-Have:**  
Functional:  

*	Account creation, login and management
*	Ability to select and download products remotely

Non-functional:  

*	Account security

**Should-Have:**  
Functional:  

*	Web service to allow web stores to plug into and utilise the functionality
*	Ability to re-download products that have been purchased previously
*	Runs in the system tray (as a Windows service)

Non-functional:  

*	Lightweight
*	Fast
*	Consistent aesthetic and User Experience (UX)
*	Robust

**Could-Have:**  
Functional:  

*	Ability to choose where files download to
*	Checksumming of files
*	Graceful error handling (picking up dropped downloads where they left off)
*	Age ratings and user DOBs to prevent minors buying age-restricted content
*	Provision in the web service for easy database manipulation by web store owners / API users

Non-functional:  

*	Extensive RESTful API with token authentication
*	Compiled for multiple platforms (using Mono .NET or similar)
*	Provision for multiple concurrent asynchronous downloads

**Won't-Have:**  
Functional:  

*	First-party web store plugging into the web service / API

Non-functional:  

*	OAuth (or similar token-based) security
*	Secure file transfers (using SSL / TLS / SFTP)
*	Payment information
<figcaption>MoSCoW functional and non-functional requirements</figcaption>
</figure>

While the features in the Could-Have and Won't-Have requirements are very desirable and would almost certainly have a higher, if not the highest, priority if this project were being developed for use in the real world, note that this software is meant as a proof-of-concept for remote media delivery, and as such features such as dealing with payment information are unnecessary to deem this project a success.

###Use-Cases
In the process of design, the main use-cases were identified and written out. Some use-case diagrams were also created to help understand the product's eventual workflow. While it is possible that not all use-cases were considered, identifying these main features' workflows was crucial to understanding the features and functionalities that needed creating.

###Database Design
The information gleaned from creating use-cases and from defining requirements turned out to be invaluable for deciding on a database structure. Optimistically, the decision was made to include provision in the database structure for not only the Must- and Should-Have requirements, but also all the Could-Haves where possible. This way, time permitting, implementing these nice-to-have features should be far easier down the road when the majority of the product has been put together than it would be if the database structure required modifying and test data required repopulating down the line. If the features turn out not to be implemented, having the extra fields in the database will still cause no problems, so seeing as it is minimal effort to add these columns and there is no risk involved, there seems to be no reason not to.

The database design was drawn up on paper, and looked like this:

<div>
<figure>
<img src="img/ERD_drawing.png">
<figcaption>First draft of the database's entity-relationship diagram.</figcaption>
</figure>
</div>

This was very close to how the final product turned out, as no further drafts seemed necessary.

###Interface Design
A good user experience is not the same as an attractive user interface. The UX encompasses more; particularly things like usability and accessibility considerations. The user interface for the application was designed to be as simple as possible and to retain its flow. As for the aesthetic, this was intended to be similar to Microsoft's Metro aesthetic introduced in Windows Phone 7 and Windows 8. This look was accomplished by using the Segoe UI font, large interface elements, and plenty of blank space to achieve a very 'clean' look.

<div>
<figure>
<img src="img/wireframes-noannotate.png">
<figcaption>Some of the wireframes drawn up while designing the front-end.</figcaption>
</figure>
</div>

###Class Design
Another thing drawn up in the planning phase was a class diagram. Having an idea of what class structure to create and where methods should be located is a really good insight to have ahead of time. This was again drawn up on paper, and can be seen in <a href="#class" class="figref">figure</a>.

<div>
<figure id="class">
<img src="img/class_drawing.png">
<figcaption>The preliminary class diagram.</figcaption>
</figure>
</div>

##Tools Available

Available for use on this project are two computers - a MacBook Pro running OS X 10.7 and a desktop PC running Windows 7 Professional 64-bit. The software tools that will be used are as follows:

*	Visual Studio 2010 Ultimate
*	C# .NET 4.0
*	PHP 5.3.8
*	MySQL 5.5.15
*	e-TextEditor 2.0.1
*	TextMate 1.6
*	Markdown 1.0 and a PHP Markdown compiler, for this longform writing and formatting.

##The Build
###Sprint 1 - Database
The first sprint of the build process consisted of creating the database that would be used by both software components - the web service and the client application. This sprint was slated to last five working days. It was very important to get the database design correct from the start, as a change to the database structure later on would mean several code changes, as the code that manipulated or pulled from the database could well be different.

A lot of the time allocated to this sprint was right away taken up with configuration of the DBMS and of Apache: for security reasons, it is often impossible to connect to a database remotely without a large amount of tweaking. The first task was writing a test connection script in PHP: a simple job of a few minutes' work. This was then run on various computers until access to the database was granted. However, getting this working required much more time than anticipated, and so creating the database had to happen in a couple of days. Luckily, the plans and ERDs drawn up during the design phase were detailed enough that database creation was simple.

Here it was necessary to add test data to the database. For some data this was easy - adding the product names and descriptions, for example - and the account details were not relevant for now (since all account management was to be in the client application anyway). However, uploading product files and adding their MD5 hashes and encrypted locations to the database was deemed part of this sprint, and so a little programming was required to create this data.

Firstly, a UNIX tool, <code>dd</code>, was used to create large files to upload to the server. Files of a variety of sizes were created: 500MB files to serve as tests for download speed and for how well the eventual software will deal with files that take time to obtain, and files of 30 or so megabytes that will be for quick testing of downloads, so no long waits are encountered while debugging. Hashes of these files were created in PHP; using PHP's interactive mode (invoked with <code>php -a</code>) the <code>md5()</code> and <code>md5_file()</code> functions could be quickly used for hash generation. Full utilities for generating hashes for long-term and mass database input are slated for a later sprint, however one quick and small utility (only suited for developer use) was created here to facilitate uploading these files' details to the database.

<div>
<figure>
<pre class="prettyprint linenums">require_once('Database.class.php');
$db = new Database();

$i = ; // Set the product's location_id here

$type = ""; // Define a Type string here
$name = ""; // Define the product's name here
$dtl = ""; // Product's details
$price = ; // Define the price here, as an integer (e.g. £34.99 -> 3499)
$rating = ; // Define the product's age rating here
$hash = md5_file(''); // Point this at the file to upload
$location_hash = md5((string)$i);

$name = mysql_real_escape_string($name);
$dtl = mysql_real_escape_string($dtl);

$query = "INSERT INTO products (product_type, product_nm, product_dtl, product_price, product_rating, product_hash, location_hash) VALUES ('$type', '$name', '$dtl', $price, $rating, '$hash', '$location_hash')";

echo $query."\r\n\r\n";
$result = mysql_query($query) or die('Error in query');
</pre>
<figcaption>A small PHP utility used to facilitate database inputs.</figcaption>
</figure>
</div>

Inputting variables into the above code and running it from the command line generated the required hashes much quicker than could be one manually, and put them into the database without any second step required.

Once enough data was in the database, this sprint was complete.

###Sprint 2 - Account Management
The second sprint could have been dedicated to writing either the web service or a part of the client application; the option not picked for the second sprint would be the third sprint, so the only thing that mattered was the order in which these were done. Both sprints were scheduled to last 10 days, so they were interchangeable. Having programmed some PHP already, for variety's sake it was decided to work on the Windows program for this sprint - written in C#.

The part of the application scheduled for this sprint was the login and account management feature. The design for this consisted of a series of dialogs to allow a user to login, or, if they had no account, to create one. However, rather than opening a new form each time and causing the application to disappear and reappear, potentially in a different location on-screen, the design was for a single form window with changing contents. Before creating the UI, though, there was some back-end work to do, namely creating an Object to hold Accounts.

<div>
<figure>
<pre class="prettyprint linenums">
public class Account
{
	public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Dob { get; set; }
    public Account(string email, string name, string password, string dob)
    {
        Email = email;
        Name = name;
        Password = password;
        Dob = dob;
    }
}
</pre>
<figcaption>The basic constructor for Accounts</figcaption>
</figure>
</div>

This basic object was then expanded to do more than just hold data (see [Appendix H](#Appendices-3-17)), including hashing and salting passwords and formatting dates. When creating a user account through the UI, an instance of this object is created to manipulate its details.

An issue encountered here was when hashing passwords. It turns out that some of the hashes that had been created using PHP's SHA512 hashing algorithm were incompatible with their C# counterparts. This is because, by default, PHP hashes in lowercase, whereas C# uses uppercase characters in its hashes. To resolve this an argument needed to be passed to the method that converts the each hash byte to a string to format it correctly: `.ToString("x2")`.

The UI itself was easy to create using Visual Studio's built in WYSIWYG (What You See Is What You Get) editor. Following the design sensibilities that had been previously outlined, it turned out to look like the following:

<div>
<figure>
<img src="img/Screenshot_1_-_Login.png">
<figcaption>How the login screen turned out to look when implemented.</figcaption>
</figure>
</div>

More screen captures of the implemented UI are available in [Appendix K](#Appendices-2-11).

The final features to implement for the sprint were logging in and creating accounts. The hashing and salting algorithms were already written, so all that was needed to do was create a database class and hook everything up to the user interface. Additionally, the IP address of the computer logging in needed to be reported and stored in the database so that downloads would know which IP to be transmitted to. IPs were converted to and stored as integers via the `INET_ATON()` function in MySQL. This went through with few problems and effectively passed black-box testing. The remaining feature was for auto-login: the application is designed to be started whenever Windows starts, so the user would not want to have to type a username and password every time they reboot. It was decided that the application should remember the last user to log in, and this user's information could be wiped with a logout button. Additionally, after creating a new account, the user would have to log in once more (for security purposes) before they would be remembered. This logic was fairly easy to implement, and so the sprint was completed without issue.

###Sprint 3 - Web Service
Slated for this sprint was the web service and its methods. Once again, 10 days were allocated to this sprint. However, this sprint was not as without incident as the previous one.

The work on the database was being done mostly locally - due to some travelling when the work was started, a consistent internet connection was not always available. Therefore, running an Apache server on a MacBook Pro to serve up PHP and MySQL was far more productive than trying to access a remote server to run PHP and test SQL on. However, a few days into the sprint, the hard disk on the Macbook gave up and the device would no longer boot. While this was something planned for in the risk assessment, it was still a blow. Luckily the operating system is configured to back up to a wireless external drive several times a day, so a minimal amount of work was lost (although it had not been able to back up in over 24 hours at this point due to having no connectivity). Ordering a replacement hard disk to install into the laptop took more time, however, and once that arrived reinstalling the operating system and restoring everything from backup took several hours.

During the couple of days when the MacBook was not available, work continued as best possible on another computer. A temporary database in SQLite was created to work on, and PHP scripts hastily modified to temporarily make use of that so queries in the web service could still be authored. When the laptop was repaired and restored and the database back online, only some quick conversions were required before the PHP that had been written during this period was fully functional. The majority of the rest of the work that had been lost was test data, so some more time was spent putting that in.

Because of these four days or so of setback and minimal work being possible, the sprint not only took two days longer than expected, but some of the functionality that would have been desirable but not necessary in the web service didn't make it in. For example, age verifications on purchases and the ability to upload files via the API were all planned for, however since the sprint was already past its due date it was time to move on. Before ending the sprint, a quick 'dashboard' was created with links to pages making calls to the web service. This was for a mix of demonstration and testing purposes. This dashboard and the operations it can perform makes for a great example of the various calls web stores could make to the web service.

###Sprint 4 - Downloading
The work scheduled for this sprint was the 'meat' of the project: receiving an instruction to download a file and running the download. Fifteen days were allocated for this sprint.

The first step here was implementing the TCPListener application, built as part of the Pilot Study seen in [chapter 3.3](#LiteratureReview-3-8), and getting it to listen to messages that were being sent from the web service. A test application was set up in PHP which sent messages, and the listener application code was reused from the pilot study. While the code worked, a problem was instantly spotted: when the listener server was running, the application's UI completely locked up. This was because the listener, which is essentially implemented on an infinite loop - `while(true) { /* listen */ }` - was blocking the entire thread the application was running on. After some research on multithreading and the BackgroundWorker class in .NET, this was overcome pretty easily. This class spawns a second, background thread that performs time-consuming operations without blocking other threads from functioning. 

<div>
<figure id="phptest">
<pre class="prettyprint linenums">
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
</pre>
<figcaption>The PHP code used to test the application's TCPListener was functioning. If the message arrived, the application displayed the MD5 hash sent.</figcaption>
</figure>
</div>

At this point, the application was successfully receiving instructions from the test PHP method (in the form of a `location_hash` from the database) but wasn't doing anything with them. The next step was to get it to work out where to find the file on the server, and then download it. A fairly convoluted algorithm in an attempt to add some Security by Obscurity (Stuttard, 2005) to the system is used to find the actual location of the file before an instance of the Download class is spawned to deal with the actual file transfer. Security by Obscurity, while no replacement for real security, is useful in this case because the resources available do not include any form of secure download, and the server, being managed hosting, does not allow for much encryption in the database or transfer.

The transfer itself involved a decision. Firstly, the code was written so files were put up on the web host and downloaded using `file://` links. This was because of worry over more threading issues: .NET provides a method `WebClient.DownloadFileAsync` which downloads the file asynchronously, to prevent locking issues. However, after a few days working around this code, it was changed to download via FTP (File Transfer Protocol) instead. While there was no SFTP (Secure FTP) server available, FTP was still somewhat more secure because while the transfer wasn't secured, it was possible to set a password on the server, so that nobody could simply download the file via URL. Another BackgroundWorker was spawned for this case, so the download still doesn't block the main thread. It just required more code.

The final part of this sprint was adding a method to MD5 checksum the file and check it against the hash stored on the server. MD5 hashes were chosen because, while not as secure as SHA512 which was used for the passwords, here it isn't an issue: the speed of the hashing algorithm is what matters most. The same issue with hash formatting was run into here as with the SHA512 hash in the Account section, but was resolved in the same way.

###Sprint 5 - Re-download & Refactor
The final sprint involved two parts. Firstly, five days were set aside to create the Redownload form. This is an interface to show all the products that had already been purchased by the logged-in user and allows them to download them again, in case the file was deleted or the download didn't complete for some reason. Creating the form itself was no problem. The issue came when trying to spawn an instance of Download: the security by obscurity built in proved to backfire and be confusing, and the logic wasn't quite right. The solution was to rewrite the method that found the actual location from the product's `location_hash` - the code to generate the FTP link had got convoluted, and while the security by obscurity was still retained, hopefully the logic would no longer confuse the person supposed to be developing the program.

Partly because of this and partly because it was in the plan from the beginning, the next five days were spent rewriting the whole application logic in a major refactor effort. This helped lean out the system code, and also exposed several hitherto-unnoticed bugs. Refactoring is a great tool for tidying up after a project's completion.

##Testing
In addition to going through all the program's and web service's features manually and checking the functionality was correct, basic unit tests were written which were intended to check that individual methods were working as expecting and giving valid results. These unit tests were written in the test framework built into the IDE: Visual Studio Test System (VSTS). This was chosen because, while not as feature-complete as the most popular .NET unit testing framework, NUnit, no additional software was required to create and run these tests, and besides it turned out the more complex features were not required in this instance anyway.

Unit tests were written for the most complex methods with outputs that would be readable by the framework. These turned out to be in the Account and Database classes. For example, a unit test was written that checked what happened when various username and password strings were fed to the login method to ensure that the login system was as secure as possible. It would have been very difficult if not impossible to run unit tests to check the downloading feature (particularly as the methods returned void), so these features were left to black-box testing. Running all the unit tests resulted in a couple of fails, but after investigating it turned out the error was in the tests, not in the code. The tests that failed were when hashing, and comparing an expected hash to a method's output. The issue though was in a malformed 'expected' hash - the hash from the database which was pasted into the test case had not copied correctly and become truncated. After identifying and fixing both instances of this, all the test cases ran successfully.

<div>
<figure>
<img src="img/unittests.png">
<figcaption>All the unit tests for Account and DbConnect classes running successfully (full unit test code can be found in <a href="#Appendices-2-10">Appendix J</a>)</figcaption>
</figure>
</div>