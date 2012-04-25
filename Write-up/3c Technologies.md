##Technologies
###Possible Technologies
Creating a client application, especially one that will be designed to run as a service, means several things. Firstly, performance must be considered - this application will be running constantly in the background of a system, and so its resource usage must be kept to a minimum. This straight away rules out interpreted languages such as Java, which in particular is often criticised for using up a large amount of RAM. Secondly, the choice of technology can limit which operating system(s) the program can run on. While various programming languages are universal, notably Java, and while methods of running technologies on alternative operating systems exist (for example, Mono allows .NET applications to run on Linux and Mac operating systems), to some extent choosing a programming language can choose the target operating system by default. Similarly, choosing an operating system to target can limit the available choice of technology. Making an informed decision of either operating system or technology to develop with is important from early on.

This project has been inspired by and likened to Steam, and other services that allow the downloading and purchase of games. While Steam is available on various platforms, most of its deliverables are restricted to the Windows operating system - most games are made on Windows - and as such it makes sense to follow suit and go where the market lies. Developing a Windows application also narrows down, as outlined above, the list of technologies that can be used somewhat - and additionally opens some doors.

The solution also involves a web service, which is for web stores to plug into to make purchases and communicate with the database. This brings into focus two more choices for technology: the language in which the web service will be written, and the DBMS (Database Management System) that will be used for the solution's persistent storage. One of the resources already available for this project was an Apache web server, which had MySQL and PostgreSQL installed, as well as PHP 5.3 and Ruby on Rails 2.3 - two great choices for a DBMS and two great choices of programming language for the web service.

####Decisions
Taking into account the above restrictions on choice of technologies and making an educated choice, the project will be developed using the following technologies.

Firstly, the native Windows application will be developed using the .NET framework. This is a powerful application development, deployment and runtime framework created by Microsoft, first released in late 2000, with the current version being .NET 4. Not only does .NET contain a huge amount of useful code libraries to develop against, it executes code in a software environment known as the Common Language Runtime (CLR). This wrapper performs tasks such as memory management and exception handling so the developer does not have to. Given the aforementioned necessity for minimal consumption of system resources coupled with the time constraints on this project, using this framework would prevent development becoming bogged down in optimisation and dealing with mundanities such as garbage disposal, and additionally the libraries available when using the .NET framework are invaluable. 

Using this framework still offers a choice of programming language. There are a myriad of languages that can be compiled down to the CLR. However, the most popular of these is C# - and as such, C# .NET is the technology that will be used for creating this application.

The choice of language for the web service is a much easier decision. Since the version of Ruby on Rails running on the web server available for this project is outdated (at time of writing, the current version is RoR 3.2), and seeing as there are several machines with XAMPP (Apache, MySQL, PHP and Perl) installed available for the purposes of this project, it makes sense for the web service to be developed using PHP. For the same reasons, the database will be created using MySQL.

Finally, also available for the purposes of this project is a GitHub 'Micro' account, that allows private Git repositories to be created for the purposes of version control. Given the importance of 

###Deployment Technologies
	An astute developer will be thinking about means of deploying his applications long before it reaches that time. 

###Pilot Study
Performing preliminary work before artefact production is a useful means of evaluating the feasibility of certain methods. One of the key parts of the solution is the communication between the two software components. To study this and its feasibility, two classes (in C#) were created well in advance of the actual implementation, as a pilot study. ListenerServer.cs was a simple application that ran an instance of TCPListener, and upon receiving a message, displayed it on the console and sent back the current date and time. ListenerServerTest.cs was the other half of this application, which sent the message, and received and displayed the response.

<figure>
<pre><code>class ListenerServerTest
{
  static void Main(string[] args)
  {
    int port = 31337;
    string ip = "127.0.0.1";
    TcpClient tcpclient = new TcpClient(ip, port);
    Byte[] request = Encoding.ASCII.GetBytes("request");
	
	Console.WriteLine("Sending...");
	
	tcpclient.GetStream().Write(request, 0, request.Length);
	Byte[] response = new Byte[10];
    int bytesRead = tcpclient.GetStream().Read(response, 0, 10);
    
    Console.WriteLine("Response:   " + Encoding.ASCII.GetString(response));
    Console.ReadLine();
    tcpclient.Close();
  }
}</code></pre>
<figcaption>The preliminary code that was used to test ListenerServer.cs</figcaption>
</figure>