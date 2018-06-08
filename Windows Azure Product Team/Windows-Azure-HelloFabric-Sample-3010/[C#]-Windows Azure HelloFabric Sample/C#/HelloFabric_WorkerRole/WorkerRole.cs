//
// <copyright file="WorkerRole.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//
using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Samples.ServiceHosting.HelloFabric;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Microsoft.Samples.ServiceHosting.HelloFabric
{
    public class MyWorkerRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString");

            return base.OnStart();
        }

        // Generate a tick in the log every 10 seconds.
        public override void Run()
        {
            var dayTimeServer = new DayTimeTcpServer(RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["DayTime"].IPEndpoint);

            dayTimeServer.Start();

            int count = 0;

            for ( ; ; )
            {
                count ++;

                Trace.WriteLine(String.Format("Message {0}", count));

                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
    }
}
