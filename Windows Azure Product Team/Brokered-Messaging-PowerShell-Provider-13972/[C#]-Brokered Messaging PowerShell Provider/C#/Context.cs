//---------------------------------------------------------------------------------
// Microsoft (R)  Windows Azure SDK
// Software Development Kit
// 
// Copyright (c) Microsoft Corporation. All rights reserved.  
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
//---------------------------------------------------------------------------------

namespace Microsoft.ServiceBus.Samples
{
    using System;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.Management.Automation;
    using Microsoft.ServiceBus.Messaging; 

    public static class Context
    {
        static string issuerName;
        static NamespaceManager serviceBusNamespaceClient;
        static string issuerSecret;
        static string serviceBusNamespace;
        static MessagingFactory messagingFactory;

        public const string PathSeparator = "\\";

        static Context()
        {
            GetVariable("IssuerName", ref issuerName);
            GetVariable("IssuerSecret", ref issuerSecret);
            GetVariable("Namespace", ref serviceBusNamespace);
            Context.Cache = new ConcurrentDictionary<string, ServiceBusCollection>();
        }

        public static ConcurrentDictionary<string, ServiceBusCollection> Cache { get; private set; }

        static void GetVariable(string variableName, ref string outVariable)
        {
            if (string.IsNullOrEmpty(outVariable))
            {
                outVariable = ConfigurationManager.AppSettings[variableName];
            }
        }

        static void ReadVariables(SessionState sessionState)
        {
            var variable = sessionState.PSVariable.Get("IssuerName");
            if (variable != null && variable.Value != null)
            {
                issuerName = variable.Value.ToString();
            }

            variable = sessionState.PSVariable.Get("IssuerSecret");
            if (variable != null && variable.Value != null)
            {
                issuerSecret = variable.Value.ToString();
            }

            variable = sessionState.PSVariable.Get("Namespace");
            if (variable != null && variable.Value != null)
            {
                serviceBusNamespace = variable.Value.ToString();
            }
        }        

        public static NamespaceManager GetServiceBusNamespaceClient(SessionState sessionState)
        {
            if (serviceBusNamespaceClient != null)
            {
                return serviceBusNamespaceClient;
            }

            if (string.IsNullOrEmpty(Context.issuerSecret) || string.IsNullOrEmpty(Context.issuerName) || string.IsNullOrEmpty(Context.serviceBusNamespace))
            {
                ReadVariables(sessionState);
            }

            if (string.IsNullOrEmpty(issuerSecret) || string.IsNullOrEmpty(issuerName) || string.IsNullOrEmpty(serviceBusNamespace))
            {
                throw new ArgumentException("Use Set-Credentials commandlet to set you ACS creds before attempting to navigate your namespace, you can alternatively modify "+
                                            "powershell config at %SystemRoot%\\WindowsPowerShell\\v1.0\\powershell.exe.config and add to the appSettings the following keys"+
                                             "\n\n<add key=\"IssuerName\" value=\"YourIssuerName\"/>"+
                                             "\n<add key=\"IssuerSecret\" value=\"YourIssuerSecret\"/>" +
		                                     "\n<add key=\"Namespace\" value=\"YourNamespace\"/>");
            }
          
            TokenProvider credentials =
                TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);

            return new NamespaceManager(ServiceBusEnvironment.CreateServiceUri("https", serviceBusNamespace, String.Empty), credentials);
        }

        public static MessagingFactory GetMessagingFactory(SessionState sessionState)
        {
            if (messagingFactory != null)
            {
                return messagingFactory;
            }

            ReadVariables(sessionState);

            TokenProvider credentials =
                TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);

            messagingFactory = MessagingFactory.Create(
                ServiceBusEnvironment.CreateServiceUri("sb", serviceBusNamespace, String.Empty),
                    credentials);

            return messagingFactory;        
        }

        public static void ClearContext()
        {
            Context.serviceBusNamespaceClient = null;
            Context.issuerSecret = null;
            Context.issuerName = null;
            Context.serviceBusNamespace = null;
            Context.Cache.Clear();
        }

        public static string[] ChunkPath(PathInfo pathInfo)
        {
            return Context.ChunkPath(pathInfo.Drive, pathInfo.ProviderPath);
        }        

        public static string[] ChunkPath(PSDriveInfo driveInfo, string path)
        {
            // Return the path with the drive name and first path 
            // separator character removed, split by the path separator.
            string pathNoDrive = path.Replace(driveInfo.Root + Context.PathSeparator, string.Empty);
            return pathNoDrive.Split(Context.PathSeparator.ToCharArray());
        }

        public static void Remove(this ConcurrentDictionary<string, ServiceBusCollection> cache, string key)
        {
            ServiceBusCollection notUsed;
            cache.TryRemove(key, out notUsed);
        }
    }
}
