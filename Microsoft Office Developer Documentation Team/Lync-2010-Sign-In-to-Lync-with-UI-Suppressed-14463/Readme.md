# Lync 2010:  Sign In to Lync with UI Suppressed
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2010
## Topics
* UI suppression
## IsPublished
* True
## ModifiedDate
* 2011-12-30 04:52:59
## Description

<p><span style="font-size:medium"><strong>Introduction</strong></span></p>
<p><span style="font-size:small">Learn how to sign in to Microsoft Lync 2010 and send an IM when the Lync 2010 UI is suppressed.</span></p>
<p><span style="font-size:medium"><strong>Description</strong></span></p>
<p><span style="font-size:small">UI suppression is a feature that lets developers create applications that provide Lync 2010 functionality without exposing any of the Microsoft Lync 2010 UI. The advantage to developers is they get access to all Lync 2010 functionality,
 and they can completely hide the Lync 2010 UI, which lets them make the application just as simple and elegant as they want, but they can also prevent access to any features, like the operating system or the off button, that they want to lock away from prying
 fingers and curious eyes.</span></p>
<p><span style="font-size:small">So how do you sign in when the UI is suppressed? This code sample provides an example of how to sign in and sign out from Lync with a suppressed UI.</span></p>
<p><span style="font-size:small">Note: Consider two important points. First, when the BeginSignIn method is used, provide credentials. It is possible to pass in nulls and let the system provide credentials, but if multiple users have logged in to the computer
 recently, you cannot ensure which user credential is used. Second, it is important to sign out and shut down so that when the Lync client is used in the future its object state is cleaned up. Also, if two applications use the same client, the one that initialized
 Lync is the one that must sign out and shut down Lync.</span></p>
<p><span style="font-size:small">Sample run time scenario when signing in to Lync with the UI Suppressed:</span></p>
<ol>
<li><span style="font-size:small">Get the Lync client.</span> </li><li><span style="font-size:small">Call the BeginInitialize method.</span> </li><li><span style="font-size:small">Get user credentials.</span> </li><li><span style="font-size:small">Call the BeginSignIn method.</span> </li></ol>
<p><span style="font-size:medium"><strong>More information</strong></span></p>
<ul>
<li><span style="font-size:small">Channel 9 Video: <a href="http://channel9.msdn.com/posts/Sign-In-to-Lync-with-UI-Suppressed">
Sign In to Lync with UI Suppressed</a></span> </li><li><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/hh378603.aspx">
Walkthrough: Sign In to Lync with UI Suppressed</a></span> </li><li><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/hh378588.aspx">
Walkthrough: Sign Out of Lync with UI Suppressed</a></span> </li></ul>
<p><br>
<br>
<br>
<br>
</p>
