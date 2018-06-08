>>>>>>ServiceBusPowerShellProvider Sample

This sample demonstrate how you can take advantage of PowerShell to: 

- Navigate your ServiceBus Namespace like you will navigate the file system
- Use builtin cmdlets to create and delete Queues, Topics, Subscriptions and Rules
- Use custom cmdlets to send and receive messages

>>>>>>Set up the Module

1. Build the ServiceBusPowerShellProvider solution
2. Create a folder for your module under %PSModulePath%
C:\Windows\system32\WindowsPowerShell\v1.0\Modules\ServiceBus
3. Copy your debug folder to the folder you just created
copy %PathToSDK%\ServiceBusPowerShellProvider\bin\Debug\* %PSModulePath%\ServiceBus\

3. Open a powershell window 
4. Import the module you just created
Import-Module ServiceBus

5. Create a PSDrive
new-psdrive -name ServiceBus -root c:\ps -psprovider ServiceBusPowerShellProvider

6. Move to the PSDrive location you just created
set-location ServiceBus:\

You should see the following

Windows PowerShell
Copyright (C) 2009 Microsoft Corporation. All rights reserved.

PS C:\> Import-Module ServiceBus
PS C:\> new-psdrive -name ServiceBus -root c:\ps -psprovider ServiceBusPowerShellProvider

Name           Used (GB)     Free (GB) Provider      Root                                               CurrentLocation
----           ---------     --------- --------      ----                                               ---------------
ServiceBus                             ServiceBus... c:\ps


PS C:\> set-location ServiceBus:\
PS ServiceBus:\>

There is a sample shortcut that already does the above so next time you can simply use that, it is convenient to do it by hand the
first time in case any issues happen, see ServiceBus.lnk

>>>>>>Use the Module

1. Set your servicebus credentials
Set-Credentials -IssuerName owner -Namespace manuelint7-1 -IssuerSecret B4cfS1l5wN8wxkSEfk01felzOUxN2piFqmgKGyxK1Hw=

2. Navigate through the environment

PS ServiceBus:\> dir
Queues
Topics
PS ServiceBus:\> cd Queues
PS ServiceBus:\Queues> dir
PS ServiceBus:\Queues>

3. Create a new queue

PS ServiceBus:\Queues> new-item demoqueue
PS ServiceBus:\Queues> dir

PSPath                                 : Microsoft.ServiceBus.Samples.ServiceBusPowerShellPSSnapIn\ServiceBusPowerShell
                                         Provider::c:\ps\Queues\demoqueue
PSParentPath                           : Microsoft.ServiceBus.Samples.ServiceBusPowerShellPSSnapIn\ServiceBusPowerShell
                                         Provider::c:\ps\Queues
PSChildName                            : demoqueue
PSDrive                                : ServiceBus
PSProvider                             : Microsoft.ServiceBus.Samples.ServiceBusPowerShellPSSnapIn\ServiceBusPowerShell
                                         Provider
PSIsContainer                          : False
NamespaceClient                        : Microsoft.ServiceBus.ServiceBusNamespaceClient
Path                                   : demoqueue
LockDuration                           : 00:00:30
MaxQueueSizeInBytes                    : 104857600
RequiresDuplicateDetection             : False
RequiresSession                        : False
DefaultMessageTimeToLive               : 10675199.02:48:05.4775807
EnableDeadLetteringOnMessageExpiration : False
DuplicateDetectionHistoryTimeWindow    : 00:10:00
ExtensionData                          : System.Runtime.Serialization.ExtensionDataObject

4. Send/Receive messages from queue

PS ServiceBus:\Queues> Send-Message -To demoqueue -Message message1
PS ServiceBus:\Queues> Send-Message -To demoqueue -Message message2
PS ServiceBus:\Queues> Receive-Message -From demoqueue


CorrelationId           :
SessionId               :
ReplyToSessionId        :
DeliveryCount           : 0
ExpiresAtUtc            : 12/31/9999 11:59:59 PM
LockedUntilUtc          : 4/27/2011 11:51:10 PM
LockToken               : 5ca26ecf-45e9-4399-923f-d3a65c35707c
MessageId               : 55b08788753a49b6951e20949e358921
MessageReceipt          : Microsoft.ServiceBus.Messaging.MessageReceipt
ContentType             :
Label                   :
Properties              : {[Body, message1]}
ReplyTo                 :
EnqueuedTimeUtc         : 4/27/2011 11:50:21 PM
ScheduledEnqueueTimeUtc : 1/1/0001 12:00:00 AM
SequenceNumber          : 1
Size                    : 71
TimeToLive              : 10675199.02:48:05.4775807
To                      :



PS ServiceBus:\Queues> $message = Receive-Message -From demoqueue -PeekLock
PS ServiceBus:\Queues> $message


CorrelationId           :
SessionId               :
ReplyToSessionId        :
DeliveryCount           : 0
ExpiresAtUtc            : 12/31/9999 11:59:59 PM
LockedUntilUtc          : 4/27/2011 11:51:23 PM
LockToken               : bb0c842d-c432-4c0c-bfd9-49aae8599434
MessageId               : fa5c8d6c0c8e45a7b37b79ba3d4dafac
MessageReceipt          : Microsoft.ServiceBus.Messaging.MessageReceipt
ContentType             :
Label                   :
Properties              : {[Body, message2]}
ReplyTo                 :
EnqueuedTimeUtc         : 4/27/2011 11:50:24 PM
ScheduledEnqueueTimeUtc : 1/1/0001 12:00:00 AM
SequenceNumber          : 2
Size                    : 71
TimeToLive              : 10675199.02:48:05.4775807
To                      :



PS ServiceBus:\Queues> $message.Complete()
PS ServiceBus:\Queues>
