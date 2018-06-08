// 
// (c) Microsoft Corporation. All rights reserved.
// 
namespace sbaztool
{
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.AccessControlExtensions;
    using Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement;

    class Program
    {
        const string settingsFileName = "settings";
        static Action action;
        static string logo = "SBAzTool Service Bus Authorization Tool\n(c) Microsoft Corporation\n";
        static string managementKey;
        static string @namespace;
        

        static string usage = "SBAzTool allows managing service identities and authorization rules associated\n" +
                              "with a Windows Azure Service Bus namespace.\n\n" + "The command structure is generally as follows:\n" +
                              "  sbaztool.exe [command] [command-arg] ... [command-arg] {option} {option}\n\n" +
                              "Options are generally applicable across commands and supply information such as\n" +
                              "namespace names or access keys. The command \"storeoptions\" allows storing the\n" +
                              "options in the user context for subsequent command invocations. The commands\n" +
                              "\"showoptions\" and \"clearoptions\" allow showing and clearing the stored\n" + "options.\n" +
                              "\nThe following options are defined:\n" + "  -n <namespace>            <namespace> is the Service Bus namespace to operate\n" +
                              "                            on. Required.\n" +
                              "  -k <key>                  <key> is the Access Control management key for the \n" +
                              "                            Access Control <namespace>-sb namespace. Required.\n" +
                              "\nThe following commands are defined:\n\n" +
                              "  makeid <name> [<key>]     Creates a new service identity with <name>\n" +
                              "                            and a 32-byte, base64-encoded <key>. If <key> is\n" +
                              "                            not provided, it is generated and displayed.\n" +
                              "  showid <name>             Gets details for the service identity with <name>\n" +
                              "  deleteid <name>           Deletes the service identity with <name>\n" +
                              "  grant <op> <path> <name>  Grants operation <op> on <path> for identity\n" +
                              "                            <name>. See remarks below.\n" +
                              "  revoke <op> <path> <name> Revokes permission for operation <op> on <path> for\n" +
                              "                            service identity <name>. See remarks below.\n" +
                              "  show <path>               Shows all permissions effective for <path>\n" +
                              "  storeoptions              Stores the options provided with the command in the\n" +
                              "                            user's context. Stored options are sticky across\n" +
                              "                            command line sessions and reboots until cleared.\n" +
                              "  showoptions               Shows the stored options.\n" + "  clearoptions              Clears the stored options.\n" +
                              "\nNotes:\n" + " The defined operations for the \"grant\" and \"revoke\" command are\n\n" +
                              "    Send      Sending into a queue, topic or relay endpoint.\n" +
                              "    Listen    Receiving from a queue or subscription or listening on the relay.\n" +
                              "    Manage    Creating or deleting queues, topics, or subscriptions.\n\n" +
                              " Details about the associated rights can be found in the product documentation.\n" +
                              " The <path> expression is a relative path on the Service Bus namespace, \n" +
                              " e.g. /myqueue or /my/endpoint. The leading slash is optional.";

        static void Main(string[] args)
        {
            LoadOptions();
            if (args.Length == 0)
            {
                Console.WriteLine(logo);
                Console.WriteLine(usage);
                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-") || args[i].StartsWith("/"))
                {
                    if (args[i].Length != 2)
                    {
                        Console.Error.WriteLine("Unknown or incomplete option '{0}'", args[i]);
                        return;
                    }
                    switch (args[i][1])
                    {
                        case 'n':
                        case 'N':
                            i++;
                            if (i >= args.Length)
                            {
                                Console.Error.WriteLine("Missing key value for option -n");
                                return;
                            }
                            @namespace = args[i];
                            if (@namespace.Any((c) => !char.IsLetterOrDigit(c) && c != '_' && c != '-'))
                            {
                                Console.Error.WriteLine("Invalid namespace name for option -n '{0}'", args[i]);
                                return;
                            }

                            break;
                        case 'k':
                        case 'K':
                            {
                                i++;
                                if (i >= args.Length)
                                {
                                    Console.Error.WriteLine("Missing key value for option -k");
                                    return;
                                }
                                managementKey = args[i];
                                try
                                {
                                    byte[] bytes = Convert.FromBase64String(managementKey);
                                    if (bytes.Length != 32)
                                    {
                                        Console.Error.WriteLine("Decoded base64 key does not yield a 32-byte (256-bit) key -k '{0}'", args[i]);
                                        return;
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.Error.WriteLine("Invalid base64 key for -k '{0}'", args[i]);
                                    return;
                                }
                            }
                            break;
                        default:
                            Console.Error.WriteLine("Unknown option '{0}'", args[i]);
                            return;
                    }
                }
                else
                {
                    // commands
                    if (args[i].Equals("makeid", StringComparison.OrdinalIgnoreCase))
                    {
                        int n = ++i;
                        if (n >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <name> value for 'showid' command");
                            return;
                        }
                        action = () => CreateServiceIdentity(args[n]);
                        int k = ++i;
                        if (k < args.Length)
                        {
                            try
                            {
                                byte[] bytes = Convert.FromBase64String(args[k]);
                                if (bytes.Length != 32)
                                {
                                    Console.Error.WriteLine("Decoded base64 key does not yield a 32-byte (256-bit) key -k '{0}'", args[i]);
                                    return;
                                }
                                action = () => CreateServiceIdentity(args[n], bytes);
                            }
                            catch (FormatException)
                            {
                                i--;
                            }
                        }
                    }
                    else if (args[i].Equals("showid", StringComparison.OrdinalIgnoreCase))
                    {
                        int n = ++i;
                        if (n >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <name> value for 'showid' command");
                            return;
                        }
                        action = () => GetServiceIdentity(args[n]);
                    }
                    else if (args[i].Equals("deleteid", StringComparison.OrdinalIgnoreCase))
                    {
                        int n = ++i;
                        if (n >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <name> value for 'deleteid' command");
                            return;
                        }
                        action = () => DeleteServiceIdentity(args[n]);
                    }
                    else if (args[i].Equals("grant", StringComparison.OrdinalIgnoreCase))
                    {
                        int o = ++i;
                        if (o >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <op> value for 'grant' command");
                            return;
                        }
                        int p = ++i;
                        if (p >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <path> value for 'grant' command");
                            return;
                        }
                        int n = ++i;
                        if (n >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <name> value for 'grant' command");
                            return;
                        }
                        action = () => GrantOperationRight(args[o], args[p], args[n]);
                    }
                    else if (args[i].Equals("revoke", StringComparison.OrdinalIgnoreCase))
                    {
                        int o = ++i;
                        if (o >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <op> value for 'revoke' command");
                            return;
                        }
                        int p = ++i;
                        if (p >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <path> value for 'revoke' command");
                            return;
                        }
                        int n = ++i;
                        if (n >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <name> value for 'revoke' command");
                            return;
                        }
                        action = () => RevokeOperationRight(args[o], args[p], args[n]);
                    }
                    else if (args[i].Equals("show", StringComparison.OrdinalIgnoreCase))
                    {
                        int q = ++i;
                        if (q >= args.Length)
                        {
                            Console.Error.WriteLine("Missing <path> value for 'show' command");
                            return;
                        }
                        action = () => ShowRights(args[q]);
                    }
                    else if (args[i].Equals("storeoptions", StringComparison.OrdinalIgnoreCase))
                    {
                        action = StoreOptions;
                    }
                    else if (args[i].Equals("showoptions", StringComparison.OrdinalIgnoreCase))
                    {
                        action = ShowOptions;
                    }
                    else if (args[i].Equals("clearoptions", StringComparison.OrdinalIgnoreCase))
                    {
                        action = ClearOptions;
                    }
                    else
                    {
                        Console.WriteLine(usage);
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(@namespace))
            {
                Console.Error.WriteLine("Missing -n option");
                return;
            }
            if (string.IsNullOrWhiteSpace(managementKey))
            {
                Console.Error.WriteLine("Missing -k option");
                return;
            }

            if (action == null)
            {
                Console.WriteLine(usage);
            }
            else
            {
                action();
            }
        }

        static void CreateServiceIdentity(string name, byte[] bytes)
        {
            AccessControlSettings settings = new AccessControlSettings(@namespace, managementKey);
            AccessControlServiceIdentity identity = AccessControlServiceIdentity.Create(settings, name);
            identity.Key = bytes;
            identity.Save();
            Console.WriteLine("Name: {0}\nKey: {1}", identity.Name, Convert.ToBase64String(identity.Key));
        }

        static void CreateServiceIdentity(string name)
        {
            AccessControlSettings settings = new AccessControlSettings(@namespace, managementKey);
            AccessControlServiceIdentity identity = AccessControlServiceIdentity.Create(settings, name);
            identity.Save();
            Console.WriteLine("Name: {0}\nKey: {1}", identity.Name, Convert.ToBase64String(identity.Key));
        }

        static void DeleteServiceIdentity(string name)
        {
            AccessControlSettings settings = new AccessControlSettings(@namespace, managementKey);
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);
            serviceClient.DeleteServiceIdentityIfExists(name);
            serviceClient.SaveChanges();
            Console.WriteLine("Deleted.");
        }

        static void GetServiceIdentity(string name)
        {
            AccessControlSettings settings = new AccessControlSettings(@namespace, managementKey);
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);
            ServiceIdentity si = serviceClient.GetServiceIdentityByName(name);
            if (si != null)
            {
                ServiceIdentityKey symmKey =
                    (from sk in si.ServiceIdentityKeys where sk.Type == ServiceIdentityKeyType.Symmetric.ToString() select sk).FirstOrDefault();
                if (symmKey != null)
                {
                    Console.WriteLine("Name: {0}\nKey: {1}", si.Name, Convert.ToBase64String(symmKey.Value));
                    return;
                }
            }
            Console.Error.WriteLine("Service identity '{0}' not found or key not found", name);
        }


        static void GrantOperationRight(string operation, string path, string name)
        {
            AccessControlSettings settings = new AccessControlSettings(@namespace, managementKey);
            Uri uri = ServiceBusEnvironment.CreateServiceUri("http", @namespace, path);
            AccessControlList list = NamespaceAccessControl.GetAccessControlList(uri, settings);
            IdentityReference identityReference = IdentityReference.CreateServiceIdentityReference(name);
            if (operation.Equals("Send", StringComparison.OrdinalIgnoreCase))
            {
                AccessControlRule existing = list.FirstOrDefault((r) => r.Condition.Equals(identityReference) && r.Right.Equals(ServiceBusRight.Send));
                if (existing == null)
                {
                    list.AddRule(identityReference, ServiceBusRight.Send);
                    list.SaveChanges();
                }
                else
                {
                    Console.Error.WriteLine("The right '{0}' on '{1}' has already been granted to identity '{2}'", operation, path, name);
                }
            }
            else if (operation.Equals("Listen", StringComparison.OrdinalIgnoreCase))
            {
                AccessControlRule existing = list.FirstOrDefault((r) => r.Condition.Equals(identityReference) && r.Right.Equals(ServiceBusRight.Listen));
                if (existing == null)
                {
                    list.AddRule(identityReference, ServiceBusRight.Listen);
                    list.SaveChanges();
                }
                else
                {
                    Console.Error.WriteLine("The right '{0}' on '{1}' has already been granted to identity '{2}'", operation, path, name);
                }
            }
            else if (operation.Equals("Manage", StringComparison.OrdinalIgnoreCase))
            {
                AccessControlRule existing = list.FirstOrDefault((r) => r.Condition.Equals(identityReference) && r.Right.Equals(ServiceBusRight.Manage));
                if (existing == null)
                {
                    list.AddRule(identityReference, ServiceBusRight.Manage);
                    list.SaveChanges();
                }
                else
                {
                    Console.Error.WriteLine("The right '{0}' on '{1}' has already been granted to identity '{2}'", operation, path, name);
                }
            }
            else
            {
                Console.Error.WriteLine("Unknown operation '{0}'", operation);
            }
        }

        static void RevokeOperationRight(string operation, string path, string name)
        {
            AccessControlSettings settings = new AccessControlSettings(@namespace, managementKey);
            Uri uri = ServiceBusEnvironment.CreateServiceUri("http", @namespace, path);
            AccessControlList list = NamespaceAccessControl.GetAccessControlList(uri, settings);
            IdentityReference identityReference = IdentityReference.CreateServiceIdentityReference(name);
            if (operation.Equals("Send", StringComparison.OrdinalIgnoreCase))
            {
                AccessControlRule existing = list.FirstOrDefault((r) => r.Condition.Equals(identityReference) && r.Right.Equals(ServiceBusRight.Send));
                if (existing != null)
                {
                    if (existing.Inherited)
                    {
                        Console.Error.WriteLine("Cannot revoke inherited rules.");
                        return;
                    }
                    list.RemoveRule(existing);
                    list.SaveChanges();
                }
                else
                {
                    Console.Error.WriteLine("The right '{0}' on '{1}' has not been granted to identity '{2}'", operation, path, name);
                }
            }
            else if (operation.Equals("Listen", StringComparison.OrdinalIgnoreCase))
            {
                AccessControlRule existing = list.FirstOrDefault((r) => r.Condition.Equals(identityReference) && r.Right.Equals(ServiceBusRight.Listen));
                if (existing != null)
                {
                    list.RemoveRule(existing);
                    list.SaveChanges();
                }
                else
                {
                    Console.Error.WriteLine("The right '{0}' on '{1}' has not been granted to identity '{2}'", operation, path, name);
                }
            }
            else if (operation.Equals("Manage", StringComparison.OrdinalIgnoreCase))
            {
                AccessControlRule existing = list.FirstOrDefault((r) => r.Condition.Equals(identityReference) && r.Right.Equals(ServiceBusRight.Manage));
                if (existing != null)
                {
                    list.RemoveRule(existing);
                    list.SaveChanges();
                }
                else
                {
                    Console.Error.WriteLine("The right '{0}' on '{1}' has not been granted to identity '{2}'", operation, path, name);
                }
            }
            else
            {
                Console.Error.WriteLine("Unknown operation '{0}'", operation);
            }
        }

        static void ShowRights(string path)
        {
            AccessControlSettings settings = new AccessControlSettings(@namespace, managementKey);
            Uri uri = ServiceBusEnvironment.CreateServiceUri("http", @namespace, path);
            AccessControlList list = NamespaceAccessControl.GetAccessControlList(uri, settings);
            Console.WriteLine("Path {0}", path);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("{0,-6} {1,-25} {2,-4}", "Right", "Assigned To", "Inherited");
            Console.WriteLine("------------------------------------------");
            foreach (AccessControlRule rule in list)
            {
                Console.WriteLine("{0,-6} {1,-25} {2,-4}", rule.Right.ClaimValue, rule.Condition.ClaimValue, rule.Inherited);
            }
        }

        static void StoreOptions()
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForAssembly();
            using (IsolatedStorageFileStream file = store.OpenFile(settingsFileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                // truncate
                file.SetLength(0);
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine(@namespace);
                    sw.WriteLine(managementKey);
                    sw.Flush();
                }
            }
        }

        static void ShowOptions()
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForAssembly();
            if (store.FileExists(settingsFileName))
            {
                using (IsolatedStorageFileStream file = store.OpenFile(settingsFileName, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        Console.WriteLine("Namespace: {0}", sr.ReadLine());
                        Console.WriteLine("Key: {0}", sr.ReadLine());
                    }
                }
            }
        }

        static void LoadOptions()
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForAssembly();
            if (store.FileExists(settingsFileName))
            {
                using (IsolatedStorageFileStream file = store.OpenFile(settingsFileName, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        @namespace = sr.ReadLine();
                        managementKey = sr.ReadLine();
                    }
                }
            }
        }

        static void ClearOptions()
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForAssembly();
            if (store.FileExists(settingsFileName))
            {
                store.DeleteFile(settingsFileName);
            }
        }
    }
}