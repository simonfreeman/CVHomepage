CVHomepage
==========

#Homepage and CV building demo

This site is still under construction, including this todo list and readme. The basic premise is to use C# and ASP.NET MVC to
create a website that can display my cv, basic info, allow people to contact me, create CVs from entered skills.
##Todo:

###Priority
- Pagination
- Search
- Actual cv generator/builder
- Enter Data
- Some kind of guest account interaction to show off the demo
- Add sorting and filtering to skills
- Decide if I'm using skills in session or not
- Refactor out repeated code in controllers

###Plausible extensions
- Add new tags or categories alongside skills to the initializor
- Refactor to repository pattern
- Refactor out to Onion Architecture
- Refactor in general, especially get some of the trash out of the controllers if possible.
- Expand out demo to actual user accounts
- Deal with incoming links from different places
- Make it look better
- Add more control to the CV, like reordering segments and bullet points. Swapping between bullets and paragraphs, etc.
- Add control to styling of CVs
- Integrate asp identity better


##IMPORTANT REMINDERS
- Right now there's a potential security flaw where you could manipulate the "Select CV" link into pulling other peoples CVs. If i expand out past the demo I need to properly investigate/fix that.
- Test the security of user id manipulation to get other peoples stuff more thoroughly.
