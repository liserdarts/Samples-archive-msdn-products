
===========================================================================

    Microsoft Lync Server 2013, SDK Samples

    Copyright (c) Microsoft Corporation.  All rights reserved.

===========================================================================

Sample Name:
=============

	SIPSnoop.exe


Sample Description:
=================

        SIPSnoop uses the Microsoft Lync Server 2013 API
	to receive all messages that the Microsoft Lync Server 2013 processes.
	It displays them in its GUI interface. SIPSnoop also maintains statistics
	about various SIP messages such as number of requests processed, responses
	processed etc.


	Compile the sample and install it on an Microsoft Lync Server 2013. Type
	sipsnoop.exe /? for command-line help.

Files in the sample
===================

Sessionmanager.cs, utils.cs, sipsnoop.cs - Main source files
sipsnoop.am - The SPL manifest used by sipsnoop.exe. This is a basic manifest that 
   demonstrates the following:

	- Ability to run before UserServices
	- Ability to run on all Servers
	- Ability to receive all requests and responses and proxy them

sipsnoop2.am - This manifest is used by sipsnoop.exe. It demonstrates the use of
	the DispatchNotification method as opposed to the Dispatch Method. 

Notice that the manifest files must be in the same directory of the executable in order for the
application to run.