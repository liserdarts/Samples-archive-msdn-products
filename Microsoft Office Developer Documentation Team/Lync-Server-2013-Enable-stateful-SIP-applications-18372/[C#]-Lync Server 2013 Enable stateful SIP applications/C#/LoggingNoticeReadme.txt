
===========================================================================

    Microsoft Lync Server 2013, SDK Samples

    Copyright (c) Microsoft Corporation.  All rights reserved.

===========================================================================

Sample Description
------------------

 LoggingNotice is a managed application that demonstrates stateful
 application logic.  The application maintains dialog state, and appends
 text to the first IM in each session.

 Session state is kept in a Hashtable and indexed by call-id.  An incoming
 ACK places a dialog into the modify-IM state, where incoming IMs are
 appended with warning text until all parties in the conversation are
 notified.  A separate session-participant table is used to determine which
 parties have already been notified, so that each party is notified exactly
 once.  Once all parties in a session have been notified, session state is
 deleted.

 Note that there are trivial ways to cause this application to leak.  In
 particular, clients that create a session and never send a message will
 cause this application to leak state.

 
 This sample demonstrates:

 - Stateful application logic
 - Message modification
 

Configuration and Setup
-----------------------
 
 Configure a homeserver, and create two users.


Building this Sample
--------------------

 Create a Visual Studio Console project
 Add reference to ServerAgent.dll (in the SDK installation folder)
 Add reference to System.Windows.Form;
 Build (F5)
 
 
Running this Sample
-------------------
 
 Log each client in, and add each to the other's contact list, if not already
 present.  Start a new IM session, and send IMs in both directions.  The first
 IM in each direction will have the text

        (*** This conversation may be logged. ***)

 appended to the message.


File List
---------

 LoggingNotice.am
 LoggingNotice.cs
