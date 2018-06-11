/*=====================================================================
  This file is part of the Microsoft Unified Communications Code Samples.

  Copyright (C) 2011 Microsoft Corporation.  All rights reserved.

This source code is intended only as a supplement to Microsoft
Development Tools and/or on-line documentation.  See these other
materials for detailed information regarding Microsoft code samples.

THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
PARTICULAR PURPOSE.
=====================================================================*/
using System;
using Microsoft.Lync.Model;


namespace MSDNArticleIM
{
    #region public delegates

    public delegate void ClientStateChanged(Boolean signedIn);
    public delegate void ClientShutdown();
        
    /// <summary>
    /// Communicates that LyncClient has been initialized to any interested classes (such as MainForm). 
    /// </summary>
    public delegate void ClientInitializedDelegate();

    #endregion

    public class ClientModel: IDisposable
    {

        #region public events
        public event ClientStateChanged ClientStateChangedEvent;
        public event ClientShutdown ClientShutdownEvent;


        /// <summary>
        /// LyncClient has been initialized
        /// </summary>
        public event ClientInitializedDelegate ClientInitializedEvent;
        #endregion

        #region private fields
        private LyncClient _lyncClient;
        private Boolean disposed = false;
        private Boolean _thisInitializedLync = false;
        #endregion

        #region public properties

        public LyncClient _LyncClient
        {
            get
            {
                LyncClient returnValue = null;
                if (_lyncClient != null)
                {
                    returnValue = _lyncClient;
                }
                return returnValue;
            }
        }


        #endregion
  

        #region callback methods


        /// <summary>
        /// callback method called by LyncClient.SignIn()
        /// </summary>
        /// <param name="source">ILyncClient</param>
        /// <param name="_asyncOperation">IAsynchronousOperation callback</param>
        /// 
        private void SigninCallback( IAsyncResult ar)
        {
            if (ar.IsCompleted == true)
            {
                try
                {
                    _LyncClient.EndSignIn(ar);
                }
                catch (LyncClientException lce)
                {
                    System.Windows.MessageBox.Show("sign in exception: " + lce.Message);
                }
            }
            
        }

        /// <summary>
        /// Handles the asynchronous initialize callback invoked by a client instance upon initialize
        /// </summary>
        /// <param name="initializedClient">LyncClient. The initialized client.</param>
        /// <param name="AsyncOp">IAsynchronousOperation. The async interface exposing the results of the operation.</param>
        private void InitializeCallback(IAsyncResult ar)
        {
            if (ar.IsCompleted == true)
            {
                object[] asyncState = (object[])ar.AsyncState;
                ((LyncClient)asyncState[0]).EndInitialize(ar);
                _thisInitializedLync = true;
                if (ClientInitializedEvent != null)
                {
                    ClientInitializedEvent();
                }
            }
        }


        #endregion

        #region public methods

        public void InitializeClient()
        {


            //***********************************************************
            //If Lync has been configured to hide its user interface then
            //this method initialize Lync before signing in.
            //***********************************************************

            if (_LyncClient.InSuppressedMode == true)
            {
                if (_LyncClient.State == ClientState.Uninitialized)
                {
                    Object[] _asyncState = { _LyncClient };
                    _LyncClient.BeginInitialize(InitializeCallback, _asyncState);
                }
            }
            else
            {

                //***********************************************************
                //If there is a registering class instance for this ClientModel event
                //raise the event and the lister will complete sign in as though
                //an initialization was necessary and had been completed.
                //***********************************************************
                if (ClientInitializedEvent != null)
                {
                    ClientInitializedEvent();
                }
            }

        }


       /// <summary>
       /// Signs a user into Lync 2010 using the supplied username, domain and password.
       /// </summary>
       /// <param name="SIP">string. Network credential user Id.</param>
       /// <param name="Domain">string. SIP address of user to be signed in.</param>
       /// <param name="Password">string. Network credential password.</param>
        public void SignIn(string SIP, string Domain, string Password)
        {
            if (_LyncClient != null)
            {
                if (_LyncClient.State != ClientState.SignedIn)
                {
                    try
                    {

                        //************************************************
                        //Block UI thread until signin is complete.
                        //************************************************
                        _LyncClient.EndSignIn(_LyncClient.BeginSignIn(
                             SIP,
                            Domain,
                            Password,
                            null,
                            null));


                    }
                    catch (NotInitializedException)
                    {
                        throw new Exception("Lync is not initialized");

                    }
                    catch (NotStartedByUserException)
                    {
                        throw new Exception("Sign in to Lync failed");
                    }
                }
            }
        }

        #endregion
    

        #region client event handlers
        /// <summary>
        /// Handles LyncClient state change events. 
        /// </summary>
        /// <param name="source">Client.  Instance of LyncClient as source of events.</param>
        /// <param name="data">ClientStateChangedEventArgs. State change data.</param>
        void _LyncClient_ClientStateChanged(Object source, ClientStateChangedEventArgs data)
        {
            if (ClientStateChangedEvent != null)
            {
                if (data.NewState == ClientState.SignedIn)
                {
                    ClientStateChangedEvent(true);
                }
                if (data.NewState == ClientState.SignedOut)
                {
                    ClientStateChangedEvent(false);
                }
            }
            if (data.NewState == ClientState.ShuttingDown 
                || data.NewState == ClientState.Invalid)
            {
                if (ClientShutdownEvent != null)
                {
                    ClientShutdownEvent();
                }
            }
        }
        #endregion

        #region constructors
        public ClientModel()
        {
            
            
            try
            {
                _lyncClient = Microsoft.Lync.Model.LyncClient.GetClient();


                if (_LyncClient == null)
                {
                    throw new Exception("Unable to obtain client interface");
                }

                _lyncClient.StateChanged += _LyncClient_ClientStateChanged;
                _lyncClient.CredentialRequested += _LyncClient_CredentialRequested;


            }

            catch (NotStartedByUserException)
            {
                throw new Exception("Lync is not running");
            }
        }

        void _LyncClient_CredentialRequested(object sender, CredentialRequestedEventArgs e)
        {
            if (e.Type == CredentialRequestedType.SignIn)
            {
                using (CredentialForm _CredentialForm = new CredentialForm())
                {
                    _CredentialForm.ShowDialog();
                    if (_CredentialForm.DialogResult == true)
                    {
                        e.Submit(
                            _CredentialForm.userDomain,
                            _CredentialForm.userPassword,
                            false);
                    }
                }
            }
        }

        #endregion

       

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(Boolean disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _LyncClient.StateChanged -= _LyncClient_ClientStateChanged;

                    if (_thisInitializedLync == true)
                    {
                        if (_LyncClient.State == ClientState.SignedIn)
                        {
                            _LyncClient.EndSignOut(_lyncClient.BeginSignOut(null, null));
                            _LyncClient.EndShutdown(_LyncClient.BeginShutdown(null, null));

                        }
                    }
                    _lyncClient = null;
                }
                this.disposed = true;
            }
        }

        #endregion

    }
}

