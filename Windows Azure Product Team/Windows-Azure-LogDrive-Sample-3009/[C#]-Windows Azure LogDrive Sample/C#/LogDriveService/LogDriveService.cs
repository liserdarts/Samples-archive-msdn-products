//
// Copyright (C) Microsoft. All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using System.IO;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure;
using System.Threading;
using Microsoft.Web.Administration;

namespace Samples
{
    public partial class LogDriveService : ServiceBase
    {
        public LogDriveService()
        {
            InitializeComponent();
            RoleEnvironment.Changed += RoleEnvironmentChanged;
            RoleEnvironment.StatusCheck += RoleEnvironmentStatusCheck;
        }

        #region Service Control Manager (SCM) Lifecycle

        private const int ThreadPollTimeInMilliseconds = 1000;

        protected override void OnStart(string[] args)
        {
            /// Windows Azure waits for auto-start services to be fully started 
            /// before sending any traffic.  Service Control Manager (SCM) does
            /// not impose a time limit on startup; the service need only 
            /// request additional time.

            if (!RoleEnvironment.IsAvailable) return;

            var startThread = new Thread(OnStartInternal);
            startThread.Start();

            // wait until a status check has occurred, so that Windows Azure knows we are working on something.
            WaitForHandle(statusCheckWaitHandle);
        }

        protected override void OnStop()
        {
            OnStopInternal();
        }

        protected override void OnShutdown()
        {
            /// Windows Azure stops sending traffic before shutting down.
            /// Note that some requests may still be executing.
            OnStopInternal();
        }
               
        private void WaitForThread(Thread thread)
        {
            while (!thread.Join(ThreadPollTimeInMilliseconds))
            {
                this.RequestAdditionalTime(ThreadPollTimeInMilliseconds * 2);
            }
        }

        private void WaitForHandle(WaitHandle handle)
        {
            while (!handle.WaitOne(ThreadPollTimeInMilliseconds))
            {
                this.RequestAdditionalTime(ThreadPollTimeInMilliseconds * 2);
            }
        }

        #endregion

        #region Implementation

        private CloudDrive currentDrive;

        private readonly EventWaitHandle statusCheckWaitHandle = new ManualResetEvent(false);
        private volatile bool busy = true;

        private void RoleEnvironmentStatusCheck(object sender, RoleInstanceStatusCheckEventArgs e)
        {
            if (this.busy)
            {
                e.SetBusy();
            }
            statusCheckWaitHandle.Set();
        }

        private void OnStartInternal()
        {
            try
            {
                // initialize the drive cache
                string cachePath = RoleEnvironment.GetLocalResource("Data").RootPath;
                CloudDrive.InitializeCache(cachePath, 4096);
                this.EventLog.WriteEntry("initialization succeeded");

                // mount the current drive
                this.currentDrive = MountDrive();

                // configure IIS
                ConfigureWebServer(this.currentDrive.LocalPath);

                this.busy = false;
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                throw;
            }
        }

        private void RoleEnvironmentChanged(object sender, RoleEnvironmentChangedEventArgs e)
        {
            if(!e.Changes.OfType<RoleEnvironmentConfigurationSettingChange>().Any(c => c.ConfigurationSettingName == "LogDriveService.BlobPath"))
                return;

            try
            {
                // perform a rolling drive change
                var oldDrive = this.currentDrive;
                var newDrive = MountDrive();
                try
                {
                    ConfigureWebServer(newDrive.LocalPath);
                }
                catch (Exception)
                {
                    UnmountDrive(newDrive);
                    throw;
                }
                this.currentDrive = newDrive;
                UnmountDrive(oldDrive);
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                throw;
            }
        }

        private void OnStopInternal()
        {
            try
            {
                ConfigureWebServer(@"%SystemDrive%");

                if (this.currentDrive != null)
                {
                    UnmountDrive(this.currentDrive);
                    this.currentDrive = null;
                }
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                throw;
            }
        }

        private CloudDrive MountDrive()
        {
            // create or mount an instance-specific drive
            var credentials = GetStorageCredentials();
            var driveUri = GetDriveUri();
            var drive = new CloudDrive(driveUri, credentials);

            try
            {
                drive.Create(1024);
            }
            catch (Exception ex)
            {
                if (ex.Message != "ERROR_BLOB_ALREADY_EXISTS") throw;
            }

            // mount the drive
            string mountPoint = drive.Mount(1024, DriveMountOptions.FixFileSystemErrors | DriveMountOptions.Force);
            this.EventLog.WriteEntry(string.Format("{0} mounted at {1}", drive.Uri, mountPoint));
            return drive;
        }

        private void UnmountDrive(CloudDrive drive)
        {
            drive.Unmount();
            this.EventLog.WriteEntry(string.Format("{0} unmounted", drive.Uri));
        }

        private Uri GetDriveUri()
        {
            return new Uri(string.Format(
                RoleEnvironment.GetConfigurationSettingValue("LogDriveService.BlobPath"), 
                RoleEnvironment.CurrentRoleInstance.Id));
        }

        private StorageCredentials GetStorageCredentials()
        {
            return new StorageCredentialsAccountAndKey(
                RoleEnvironment.GetConfigurationSettingValue("LogDriveService.AccountName"),
                RoleEnvironment.GetConfigurationSettingValue("LogDriveService.AccountKey"));
        }

        private void ConfigureWebServer(string drivePath)
        {
            using (var config = new ServerManager())
            {
                var logdir = Path.Combine(drivePath, @"inetpub\logs\LogFiles");
                config.SiteDefaults.LogFile.Directory = logdir;

                config.CommitChanges();

                this.EventLog.WriteEntry(string.Format("IIS log location set to '{0}'", logdir));
            }
        }

        #endregion

    }
}
