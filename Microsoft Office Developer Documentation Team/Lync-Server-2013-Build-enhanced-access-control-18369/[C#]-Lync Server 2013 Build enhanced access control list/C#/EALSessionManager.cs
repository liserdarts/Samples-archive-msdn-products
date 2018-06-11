/*++

Copyright © Microsoft Corporation

Module Name:

    SessionManager.cs   

Abstract:

    This module implements ServerAgent specific logic for applications.
    Applications can register callbacks to receive the message received
    events.

Notes:

    Currently the SessionManager implements request handler only.

--*/
#region Using directives

using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Microsoft.Rtc.Sip.SDK.Samples.Utils;
#endregion

namespace Microsoft.Rtc.Sip.SDK.Samples.EnhancedAllowList
{
    /// <summary>
    /// Provides common functionality needed by all ServerAgent applications.
    /// Implementors can choose to derive from SessionManager and support
    /// application specific functionality.
    /// </summary>
    public class SessionManager : IDisposable   
    {
        #region BoilerPlate

        /// <summary>
        /// Session specific objects.
        /// </summary>
        private ApplicationManifest applicationManifest;
        private ServerAgent serverAgent;

        /// <summary>
        /// the Event manager thread implements the main
        /// message pump for receiving SIP messages and 
        /// delivers it to various handlers. 
        /// </summary>
        private Thread eventManager;
        private AutoResetEvent quitHandle;

        /// <summary>
        /// IDisposable state.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Need to be derived for a full implementation.
        /// </summary>
        protected SessionManager ()
        {
            disposed = false;
        }

        /// <summary>
        /// Load and compile the application manifest, and try
        /// to connect to the Live Communications Server.
        /// </summary>
        /// <exception cref="System.Exception">If unable to
        /// connect. The exception string contains the details
        /// </exception>
        /// <param name="manifestFile">file name of the SPL manifest.
        /// This file must be present in the working directory of the 
        /// executable
        /// </param>
        /// <param name="applicationName">A friendly name for use while
        /// registering with WMI. Use a null value if you dont want 
        /// the function to do registration
        /// </param>
        /// <param name="appGuid">Guid for use during WMI registration.
        /// If a null guid is specified but applicationName is not 
        /// null, a new guid will be generated, used, and returned. 
        /// </param>
        /// <seealso cref="Microsoft.Rtc.Sip.CompilerErrorException"/>
        /// <seealso cref="Microsoft.Rtc.Sip.ServerAgent"/>
        public virtual void ConnectToServer (
            string manifestFile,
            string applicationName,
            ref string appGuid
            )
        {
            CheckDisposed ();

            //
            // Load and compile the application manifest.
            //

            applicationManifest = ApplicationManifest.CreateFromFile (manifestFile);
            if (applicationManifest == null) {
                throw new Exception (
                    String.Format ("The manifest file {0} was not found", manifestFile));
            }

            try {

                //
                // Compile the manifest.
                //

                applicationManifest.Compile ();

                //
                // Connect to server.
                //

                serverAgent = new ServerAgent (this, applicationManifest);
            }
            catch (CompilerErrorException cee) {

                //
                // Collapse all compiler errors into one, and return it
                //

                StringBuilder sb = new StringBuilder (1024, 1024);
                foreach (string errorMessage in cee.ErrorMessages) {

                    if (errorMessage.Length + sb.Length + 2 < sb.MaxCapacity) {
                        sb.Append (errorMessage);
                        sb.Append ("\r\n");
                    }
                    else {
                        sb.Append (errorMessage.Substring (0, sb.MaxCapacity - sb.Length - 1));
                        break;
                    }
                }

                throw new Exception (sb.ToString ());
            }
            catch (Exception e) {

                if (applicationManifest != null) {
                    applicationManifest = null;
                }

                // ServerNotFoundException || UnauthorizedException
                throw e;
            }


            //
            // Install the connection dropped event handler.
            //

            serverAgent.ConnectionDropped += new ConnectionDroppedEventHandler (this.ConnectionDroppedHandler);

            //
            // Start the event manager.
            // 

            quitHandle = new AutoResetEvent (false);
            eventManager = new Thread (new ThreadStart (EventManager));
            eventManager.Start ();

            return;
        }


        /// <summary>
        /// Disconnect from server, cleanup. 
        /// </summary>
        /// <remarks>
        /// Derived classes should override InternalDisconnect to receive the
        /// disconnect event.
        /// </remarks>
        public void Disconnect ()
        {
            CheckDisposed ();

            //
            // This will call the correct override.
            //

            InternalDisconnect (null);
            return;
        }

        /// <summary>
        /// Are we connected to the server ?
        /// </summary>
        public bool Connected
        {
            get
            {
                return this.serverAgent != null;
            }
        }

      
        /// <summary>
        /// Event manager is responsible for receiving server events
        /// and dispatching them.
        /// </summary>
        private void EventManager ()
        {
            Debug.Write ("Event manager is started");

            //
            // We need to listen on two events - ServerAgent event handle
            // and quit handle.
            //

            WaitHandle[] waitHandle = new WaitHandle[2];
            waitHandle[0] = serverAgent.WaitHandle;
            waitHandle[1] = this.quitHandle;

            WaitCallback wcb = new WaitCallback (serverAgent.ProcessEvent);

            while (true) {

                int handleSignalled = WaitHandle.WaitAny (waitHandle);

                //
                // Do we need to quit ?
                //

                if (handleSignalled == 1) {
                    Debug.Write ("Event manager exiting.");
                    return;
                }

                //
                // Service Server event.
                //

                ThreadPool.QueueUserWorkItem (wcb);
            }
        }


        /// <summary>
        /// Disconnect from the Server and cleanup
        /// </summary>
        /// <remarks> In order to disconnect we need to dispose the ServerAgent object.
        /// Derived classes should always call base.InternalDisconnect from their overrides.
        /// </remarks>
        protected virtual void InternalDisconnect (ConnectionDroppedEventArgs cde)
        {
            ServerAgent serverAgentToDispose = serverAgent;

            if (serverAgentToDispose != null) {

                serverAgent = null;
                applicationManifest = null;

                //
                // Stop event manager.
                //

                quitHandle.Set ();
                eventManager.Join (1000 /* upto a second */);
                eventManager = null;

                try {

                    //
                    // Disconnect from server.
                    //

                    serverAgentToDispose.Dispose ();
                }
                catch (Exception e) {
                    Debug.Write (e.Message);
                }
            }

            return;
        }


        /// <summary>
        /// This callback will be invoked by ServerAgent when we are
        /// disconnected by the server due to some external reason
        /// </summary>
        /// <param name="sender">the ServerAgent raising this notification.</param>
        /// <param name="cde">reason for connection drop.</param>
        protected virtual void ConnectionDroppedHandler (object sender, ConnectionDroppedEventArgs cde)
        {
            //
            // Stop the event manager and cleanup if necessary.
            //

            InternalDisconnect (cde);
          
            return;
        }

        /// <summary>
        /// Implements support for IDisposable. Derived classes should
        /// always call the base version from their overrides.
        /// </summary>
        public virtual void Dispose ()
        {
            Dispose (true);
        }

        /// <summary>
        /// Implements support for IDisposable. 
        /// </summary>
        /// <remarks>
        /// Derived classes should always call the base version 
        /// from their overrides.
        /// </remarks>
        public virtual void Dispose (bool disposing)
        {
            if (!this.disposed) {

                InternalDisconnect (null);
                disposed = true;
            }
        }

        public void CheckDisposed ()
        {
            if (this.disposed) {
                throw new ObjectDisposedException ("SessionManager");
            }

            return;
        }

        #endregion BoilerPlate
    }
}
