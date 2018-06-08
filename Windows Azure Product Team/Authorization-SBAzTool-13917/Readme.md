# Authorization : SBAzTool
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Azure
* Service Bus
## Topics
* Service Bus
## IsPublished
* True
## ModifiedDate
* 2011-11-14 12:19:36
## Description

<h1>Introduction</h1>
<p style="text-align:justify">This sample demonstrates how to create and manage service identities in the Access Control Service for use with Service Bus and how to assign (and revoke) right from these service Identities so that they can send to or receive
 from a particular Service Bus entity or manage a particular branch of a Service Bus namespace.</p>
<p style="text-align:justify">The sample consists of a command-line tool and a reusable assembly that share the same code files. The command-line tool references the code files directly instead of using the assembly just to limit the number of files to be copied
 if the tool needs to be copied for management purposes.</p>
<p><strong>&nbsp;</strong></p>
<h1><strong>SBAzTool</strong></h1>
<p>The tool, SBAzTool.exe, allows managing service identities and authorization rules associated with a Windows Azure Service Bus namespace.</p>
<p>The command structure is generally as follows:<br>
<em>&nbsp;</em></p>
<p><em>sbaztool.exe [command] [command-arg] ... [command-arg] {option} {option}</em></p>
<p>&nbsp;</p>
<p style="text-align:justify">Options are generally applicable across commands and supply information such as namespace names or access keys. The command &quot;storeoptions&quot; allows storing the options in the user context for subsequent command invocations. The commands
 &quot;showoptions&quot; and &quot;clearoptions&quot; allow showing and clearing the stored options.</p>
<p>The following options are defined:</p>
<p><span style="text-decoration:underline">-n &lt;namespace&gt; : </span>&lt;namespace&gt; is the Service Bus namespace to operate on. Required.</p>
<p><br>
<span style="text-decoration:underline">-k &lt;key&gt;&nbsp;</span>&nbsp;: &lt;key&gt; is the Access Control management key for the Access Control &lt;namespace&gt;-sb namespace. Required.</p>
<p><span style="text-decoration:underline">-q&nbsp;</span>&nbsp;:&nbsp;Suppresses the logo and all output except errors.</p>
<p><br>
The following commands are defined:</p>
<p><span style="text-decoration:underline">makeid &lt;name&gt; [&lt;key&gt;] : </span>
Creates a new service identity with &lt;name&gt; and a 32-byte, base64-encoded &lt;key&gt;. If &lt;key&gt; is not provided, it is generated and displayed.</p>
<p><br>
<span style="text-decoration:underline">showid &lt;name&gt;</span> : Gets details for the service identity with &lt;name&gt;</p>
<p><span style="text-decoration:underline">deleteid &lt;name&gt;</span>: Deletes the service identity with &lt;name&gt;</p>
<p><span style="text-decoration:underline">grant &lt;op&gt; &lt;path&gt; &lt;name&gt;</span> : Grants operation &lt;op&gt; on &lt;path&gt; for identity &lt;name&gt;. See remarks below.</p>
<p><br>
<span style="text-decoration:underline">revoke &lt;op&gt; &lt;path&gt; &lt;name&gt;</span> : Revokes permission for operation &lt;op&gt; on &lt;path&gt; for service identity &lt;name&gt;. See remarks below.</p>
<p><span style="text-decoration:underline">show &lt;path&gt;</span>: Shows all permissions effective for &lt;path&gt;</p>
<p><span style="text-decoration:underline">storeoptions</span>: Stores the options provided with the command in the user's context. Stored options are sticky across command line sessions and reboots until cleared.</p>
<p><span style="text-decoration:underline">showoptions</span>: Shows the stored options
<br>
<span style="text-decoration:underline">&nbsp;</span></p>
<p><span style="text-decoration:underline">clearoptions</span>: Clears the stored options.</p>
<p><br>
The defined operations for the &quot;grant&quot; and &quot;revoke&quot; command are</p>
<p><span style="text-decoration:underline">Send</span>: Sending into a queue, topic or relay endpoint.</p>
<p><span style="text-decoration:underline">Listen</span>: Receiving from a queue or subscription or listening on the relay.</p>
<p><span style="text-decoration:underline">Manage</span>: Creating or deleting queues, topics, or subscriptions.</p>
<p><br>
Details about the associated rights can be found in the product documentation. The &lt;path&gt; expression is a relative path on the Service Bus namespace, e.g. /myqueue or /my/endpoint. The leading slash is optional.</p>
<p>&nbsp;</p>
<h1><strong>Prerequisites</strong></h1>
<p>If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</p>
<h1><br>
<strong>Example Usage</strong></h1>
<p>Here are a few examples for how to use the tool\</p>
<h1><strong>Managing service identities</strong></h1>
<p><strong>sbaztool makeid johndoe -n mynamespace -k TiNj35FAIviW1ZxCcNUtEfowH//9jAYvU28Vz4NhRBM= &nbsp;</strong></p>
<p>Creates a new service identity 'johndoe' in namespace 'mynamespace' with the management key 'TiNj35FAIviW1ZxCcNUtEfowH//9jAYvU28Vz4NhRBM='. The actual namespace to use here is your own service namespace and the master management key for that namespace, which
 can be obtained from the management portal. The access key for the new service identity is generated and printed on the console. The -n and -k options can be omitted if they have been previously stored in the user context using the &quot;storeoptions&quot; command.</p>
<p><br>
<strong>sbaztool makeid johndoe eYEysqKvEQUCGUf0BTXyBSJg0EUBs2Dh/zsJIkUqTIg= -n ... -k ... &nbsp;</strong></p>
<p>Creates a new service identity 'johndoe' with the preset key 'eYEysqKvEQUCGUf0BTXyBSJg0EUBs2Dh/zsJIkUqTIg=' in the desired namespace with the required key (see above).</p>
<p><br>
<strong>sbaztool showid johndoe -n ... -k ... &nbsp;</strong></p>
<p>Shows the details (key) of the service identity 'johndoe'.</p>
<p><br>
<strong>sbaztool deleteid johndoe -n ... -k ...&nbsp;&nbsp;</strong></p>
<p>Deletes the service identity 'johndoe'. <br>
<br>
<strong></strong></p>
<h1><strong>Managing access control rules</strong></h1>
<p><strong>sbaztool grant Send / johndoe -n ... -k ... &nbsp;</strong></p>
<p>This operation grants 'Send' rights to the previously created service identity 'johndoe' on the namespace root. With that, 'johndoe' can send messages to any Service Bus entity within the namespace.</p>
<p><strong>sbaztool grant Send /foo johndoe -n ... -k ... &nbsp;</strong></p>
<p>This operation grants 'Send' rights to the previously created service identity 'johndoe' on the namespace branch '/foo'. 'johndoe' can send messages to any Service Bus entity at and below the address '/foo'</p>
<p><strong>sbaztool grant Listen /bar/baz johndoe -n ... -k ... </strong>&nbsp;</p>
<p>This operation grants 'Listen' rights to the previously created service identity 'johndoe' on the namespace branch '/bar/baz'. 'johndoe' can receive messages from any Service Bus entity at and below the address '/bar/baz'</p>
<p><strong>sbaztool revoke Listen /bar/baz johndoe -n ... -k ... &nbsp;</strong></p>
<p>This operation revokes the previously granted 'Listen' rights for service identity 'johndoe' on the namespace branch '/bar/baz'. 'johndoe' can no longer receive messages from Service Bus entities at and below the address '/bar/baz'.</p>
<p><strong>sbaztool revoke Send /foo/zoo johndoe -n ... -k ... &nbsp;</strong></p>
<p>This operation revokes the previously granted 'Send' right for 'johndoe' from the namespace branch 'foo/zoo'. However, if the right was previously granted on a parent branch, like '/foo' as shown above, the operation will fail because inherited rights can
 not be revoked on parent branches.</p>
<p>&nbsp;</p>
