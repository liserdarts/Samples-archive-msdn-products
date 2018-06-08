//
// <copyright file="DayTimeTcpServer.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
namespace Microsoft.Samples.ServiceHosting.HelloFabric
{
    public  class DayTimeTcpServer
    {
        public DayTimeTcpServer(IPEndPoint ep)
        {
            _tcpListener = new TcpListener(ep);
        }

        private TcpListener _tcpListener;

        void OnAcceptTcpClient(IAsyncResult x)
        {
            var thread = new System.Threading.Thread(
            () =>
            {
                using (TcpClient tcpClient = _tcpListener.EndAcceptTcpClient(x))
                {
                    HandleClientConnect(tcpClient);
                }
            });

            thread.Start();

            _tcpListener.BeginAcceptSocket(OnAcceptTcpClient, null);
        }

        public void Start()
        {
            _tcpListener.Start();
            _tcpListener.BeginAcceptSocket(OnAcceptTcpClient, null);
        }

        public void Stop()
        {
            Trace.TraceInformation("Shutting off tcp service on {0}\n", _tcpListener.Server.LocalEndPoint);

            _tcpListener.Stop();
        }

        private void HandleClientConnect(TcpClient tcp)
        {
            Trace.TraceInformation("Accepting client from: {0}", tcp.Client.RemoteEndPoint);

            using (var strm = tcp.GetStream())
            {
                var wr = new StreamWriter(strm);
                wr.WriteLine(DateTime.UtcNow.ToString("r"));
                wr.Close();
            }
        }
    }
}