CVHomepage
==========

#Homepage and CV building demo

This site is still under construction, including this todo list and readme. The basic premise is to use C# and ASP.NET MVC to
create a website that can display my cv, basic info, allow people to contact me, create CVs from entered skills.
##Todo:

###Priority
- Add new tags or categories alongside skills
- Integrate asp identity properly
- Actual cv generator/builder
- Enter Data
- Some kind of guest account interaction to show off the demo
- Some system that makes it easy/obvious that users need to save their cvs after adding stuff to the session

###Plausible extensions
- Refactor to repository pattern
- Refactor out to Onion Architecture
- Refactor in general, especially get some of the trash out of the controllers if possible.
- Expand out demo to actual user accounts
- Deal with incoming links from different places
- Make it look better
- Add more control to the CV, like reordering segments and bullet points. Swapping between bullets and paragraphs, etc.
- Add control to styling of CVs


##IMPORTANT REMINDERS
- Right now there's a potential security flaw where you could manipulate the "Select CV" link into pulling other peoples CVs. If i expand out past the demo I need to properly investigate/fix that.
