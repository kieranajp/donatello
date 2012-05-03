##Measuring success
###Assessing MoSCoW
The main way of measuring a project's success, as previously stated, is through how well it adheres to its initial requirements. Previously, a set of MoSCoW requirements were outlined (see [chapter 4.2](#Construction-3-1)), and it was stated that the project would be considered a success if the Must-Have and Should-Have requirements were all met. The best way to check this is to go over these requirements again.

<figure>
<table>
	<tr>
		<th>Must-Haves</th>
		<th>Hit / Miss</th>
	</tr><tr>
		<td>Account creation, login and management</td><td>Hit</td>
	</tr><tr>
		<td>Ability to select and download products remotely</td><td>Hit</td>
	</tr><tr>
		<td>Account security</td><td>Hit</td>
	</tr><tr>
		<th>Should-Haves</th>
		<th>Hit / Miss</th>
	</tr><tr>
		<td>Web service to allow web stores to plug into and utilise the functionality</td><td>Hit</td>
	</tr><tr>
		<td>Ability to re-download products that have been purchased previously</td><td>Hit</td>
	</tr><tr>
		<td>Runs in the system tray (as a Windows service)</td><td>Hit</td>
	</tr><tr>
		<td>Lightweight</td><td>Hit</td>
	</tr><tr>
		<td>Fast</td><td>Hit</td>
	</tr><tr>
		<td>Consistent aesthetic and UX</td><td>Hit</td>
	</tr><tr>
		<td>Robust</td><td>Hit</td>
	</tr>
</table>
<figcaption>The Must- and Should-Have requirements.</figcaption>
</figure>

It can be seen here that all the requirements for the project's success were met, and therefore by definition the project must and will be considered a success. Due in particular to the setbacks encountered with the failed hard disk, however, not all the Could-Have requirements were met. The project was a success but some of the desirable features that would have been nice to implement didn't make it in - in particular the API didn't follow RESTful neat URL structures and didn't allow for file uploads. However, this is why Could-Have requirements exist, and it was more important that the strict rules of Scrum were followed for the sprint sections so that there was time for testing and writing. Prioritisation is important, and resisting the temptation to add fun features is difficult but is often the right decision for the success of a project and for meeting deadlines.

###Meeting Objectives
In addition to the MoSCoW requirements, some objectives were outlined before planning had even begun (see [chapter 2.1](#Introduction-2-1)). These objectives were somewhat more subjective than prioritised requirements, and were mostly but not all met. The application doesn't handle errors in the download wonderfully, for example, but it does not throw unintelligible errors at the user, instead displaying a polite and aesthetically pleasing notification that something has gone wrong and allowing the user to attempt the download again through the Re-download form. This is not the best case situation - ideally the application would gracefully fail and make another attempt at the download, however it is good enough to meet the objective halfway.

There were also a certain amount of personal objectives defined. A dissertation is after all the culmination of years of education and knowledge acquisition, and so the developer should have benefited from the application's development. Reading many of the sources in preparation for review was an invaluable source of knowledge, and technologies that had previously only been touched-upon - in particular the concepts of threading and how REST works in practice - were put into perspective and very greatly expanded upon. Additionally, it is widely regarded that the only way to improve at programming is via practical experience, and this project certainly provided a large amount of very intensive practical programming. This was great in teaching about certain classes specific to .NET - the BackgroundWorker and System.Cryptography classes come to mind - and to PHP and MySQL - for example the methods of converting and storing IP addresses as integers. Finally, of course, there are sockets and the whole communication between the two parts of the application: this was something in computing that this developer had not approached until this project.

The testing phase was also critical in determining the success of the project. The robustness of the application is due to the extensive black-box testing and finding bugs in there, as well as the unit tests making sure that method's outputs are within expected ranges. 

From a non-technical perspective, the PRINCE2 and PMBOK methodologies, as well as concepts of risk management and such were new, unexperienced approaches to projects, and were a great learning experience. The SDLC used, although not the SDLC that had been originally stated in the project proposal (see [Appendix A](#Appendices-2-1)), was very effective, and could definitely be used again for similar projects in future. The project proposal actually turned out to be somewhat incorrect as to how the execution turned out: the idea to create a web service instead of a web store wasn't had until the actual planning phase had begun, and the Gantt chart that was drawn up to timeline the project was not really adhered to as it didn't take Scrum into account. This is mostly due to having to think things up too far ahead of time for the proposal (as it was drafted months before the planning began in earnest) and thus not having the required knowledge and not having done enough reading around the subjects to make objective, educated decisions. Not sticking to everything in the proposal isn't necessarily a bad thing - the actual software turned out very much like proposed, for example - but perhaps the proposal should have accounted for itself being so far in advance and should have left more decisions open-ended, so that the project didn't have to continue with such blatant disregard for some of the proposed techniques and methodologies.

##Existing Solution Comparison
The existing solutions, as previously outlined, consist of applications such as Steam or the former web service Direct2Drive. The latter, before its acquisition, was more similar to this project: applications were purchased though its web service and downloaded to the computer. The downloads could be facilitated by an optional download manager program. However, it had no mechanism like the Listener class of this project which allowed *remote* downloading of games; the stand-out feature and indeed the title of this project. Additionally, this project, having been completed as a web service, allows external stores to plug into and use the functionality and the database, something no competitors have tried. While hypothetically breaking into this market would be tough - big names like Steam and EA's Origin have it cornered - it is evident that this functionality could be at the very least a killer feature to incorporate in one of these services, and were the funding and development resources available, could form the backbone of the next big distribution application. However, as the internet speeds up exponentially, in five years' time the duration of even the largest downloads could be negligible, rendering this application's primary use-case moot. As a proof-of-concept, it is successful and works extremely well. As a project it was a challenge and was enjoyable to complete, and related to the degree's subject matter incredibly well. 