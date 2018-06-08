//
// Copyright (C) Microsoft. All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace Samples
{
    [RunInstaller(true)]
    public partial class LogDriveServiceInstaller : System.Configuration.Install.Installer
    {
        public LogDriveServiceInstaller()
        {
            InitializeComponent();

            this.Installers.Add(new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalSystem
            });
            this.Installers.Add(new ServiceInstaller()
            {
                ServiceName = "LogDriveService",
                StartType = ServiceStartMode.Automatic,
                ServicesDependedOn = new string[] { "CloudDrive", "VSS" }
            });
        }
    }
}
