using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace OfficeTalkOSCProvider
{
    [RunInstaller(true)]
    public partial class OfficeTalkOSCProviderInstaller : System.Configuration.Install.Installer
    {
        public OfficeTalkOSCProviderInstaller()
        {
            InitializeComponent();
        }
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            //Register the OSC Provider.
            //Get the correct Regasm.exe version to use.
            String regasmPath = GetRegasmPath();

            //Create the Regasm.exe command-line arguments.
            String regasmArgs =
                String.Format(@"/codebase ""{0}""", this.GetType().Assembly.Location);

            //Call Regasm.exe to register the provider.
            Process process = Process.Start(regasmPath, regasmArgs);

            //Wait a maximum of 15 seconds for Regasm.exe to complete.
            process.WaitForExit(15000);


            //Add the registry keys for this OSC provider.
            //Get the location of the OSC Social Providers key.
            //The path to the OSC Social Providers key will be in the Wow6432Node 
            //    branch if 32-bit Office is installed on a 64-bit OS.
            String oscProvidersLocation = GetOscProvidersKeyLocation();

            //Add registry keys to the local machine hive if installing 
            //    for everyone; otherwise, install to the current user hive.
            RegistryKey hive = null;
            RegistryKey oscProvidersKey = null;
            if (this.Context.Parameters["InstallAllUsers"] == "1")
            {
                hive = Registry.LocalMachine;
            }
            else
            {
                hive = Registry.CurrentUser;
            }

            //Open the OSC Social Providers key.
            oscProvidersKey = hive.CreateSubKey(oscProvidersLocation);
            if (oscProvidersKey != null)
            {
                //Add the keys for this OSC provider.
                RegistryKey providerKey =
                    oscProvidersKey.CreateSubKey(@"OfficeTalkOSCProvider.OTProvider");

                if (providerKey != null)
                {
                    providerKey.SetValue("FriendlyName",
                        "OfficeTalkOSCProvider",
                        RegistryValueKind.String);
                    providerKey.SetValue("Url",
                        "http://yoururl",
                        RegistryValueKind.String);
                }
            }
        }
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);

            //Unregister the OSC Provider.
            //Get the correct Regasm.exe version to use.
            String regasmPath = GetRegasmPath();

            //Create the Regasm.exe command-line arguments.
            String regasmArgs =
                String.Format(@"/unregister ""{0}""", this.GetType().Assembly.Location);

            //Call Regasm.exe to unregister the provider.
            Process process = Process.Start(regasmPath, regasmArgs);

            //Wait a maximum of 15 seconds for Regasm.exe to complete.
            process.WaitForExit(15000);


            //Remove the registry keys for this OSC provider.
            //Get the location of the OSC Social Providers key.
            //The path to the OSC Social Providers key will be in the Wow6432Node 
            //    branch if 32-bit Office is installed on a 64-bit OS.
            string oscProvidersLocation = GetOscProvidersKeyLocation();

            //Remove registry keys from the local machine hive if installing
            //    for everyone; otherwise, remove from the current user hive.
            RegistryKey hive = null;
            RegistryKey oscProvidersKey = null;
            if (this.Context.Parameters["InstallAllUsers"] == "1")
            {
                hive = Registry.LocalMachine;
            }
            else
            {
                hive = Registry.CurrentUser;
            }

            //Open the OSC Social Providers key.
            oscProvidersKey = hive.OpenSubKey(oscProvidersLocation, true);

            if (oscProvidersKey != null)
            {
                //Remove the keys for this OSC provider.
                oscProvidersKey.DeleteSubKeyTree("OfficeTalkOSCProvider.OTProvider",
                    false);
            }
        }
        //Returns true when Office 64-bit is installed.
        private static bool IsOffice64Bit()
        {
            //Determine whether a 64-bit operating system is in use.
            bool is64bitOS = System.Environment.Is64BitOperatingSystem;

            //If the operating system is not 64-bit, Office cannot be 64-bit.
            if (!is64bitOS)
            {
                return false;
            }

            //Outlook bitness is stored within the Outlook version branch.
            //Get the version of Outlook currently installed.
            int officeVersion = 0;
            RegistryKey hive =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                RegistryView.Registry64);

            //Find the version of Office installed.
            //Check the 64-bit registry key.
            RegistryKey officeKey =
                hive.OpenSubKey(@"Software\Microsoft\Office\Common");
            officeVersion =
                (int)officeKey.GetValue("LastAccessInstall", 0);

            //If the Office version was not found, check the 32-bit registry key.
            if (officeVersion == 0)
            {
                officeKey =
                    hive.OpenSubKey(@"Software\Wow6432Node\Microsoft\Office\Common");
                officeVersion =
                    (int)officeKey.GetValue("LastAccessInstall", 0);
            }

            //Find the Outlook bitness.
            String officeKeyLocation =
                String.Format(@"Software\Microsoft\Office\{0}.0\Outlook",
                officeVersion);
            RegistryKey versionedKey = hive.OpenSubKey(officeKeyLocation);
            String bitness = versionedKey.GetValue("Bitness", String.Empty).ToString();

            //Return true if Outlook is 64-bit; otherwise, return false.
            if (bitness == "x64")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Returns the path to the version of Regasm.exe for registering the provider.
        private static string GetRegasmPath()
        {
            //Variables used to build the Regasm.exe path.
            string regasmPath = String.Empty;
            string runtimeFolder32 = String.Empty;
            string runtimeFolder64 = String.Empty;

            //Determine whether a 64-bit operating system is in use.
            bool is64bitOS = System.Environment.Is64BitOperatingSystem;

            //Determine wehether Office is 64-bit.
            bool isOutlook64bit = IsOffice64Bit();

            //If the operating system is 64-bit, locate both the 32-bit and
            //    64-bit versions of Regasm.exe.
            if (is64bitOS)
            {
                //Get the 32-bit runtime folder.
                RegistryKey hive32 =
                    RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                RegistryKey hklmRuntime32Key =
                    hive32.OpenSubKey(@"Software\Wow6432Node\Microsoft\.NETFramework");
                runtimeFolder32 =
                    hklmRuntime32Key.GetValue("InstallRoot", String.Empty).ToString();

                //Get the 64-bit runtime folder.
                RegistryKey hive64 =
                    RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey hklmRuntime64Key =
                    hive64.OpenSubKey(@"Software\Microsoft\.NETFramework");
                runtimeFolder64 =
                    hklmRuntime64Key.GetValue("InstallRoot", String.Empty).ToString();
            }
            else
            {
                //Get the 32-bit runtime folder.
                runtimeFolder32 = Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\Software\Microsoft\.NETFramework",
                    "InstallRoot",
                    String.Empty).ToString();
            }

            //Determine the current runtime, used as part of the path to Regasm.exe.
            string runtimeVersion =
                System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();

            //Build the path to the correct version of Regasm.exe.
            if (isOutlook64bit)
            {
                regasmPath
                    = System.IO.Path.Combine(runtimeFolder64, runtimeVersion, "regasm.exe");
            }
            else
            {
                regasmPath
                    = System.IO.Path.Combine(runtimeFolder32, runtimeVersion, "regasm.exe");
            }
            return regasmPath;
        }
        //Returns the location of the OSC Providers key based on 
        //    the Office and OS bitness.
        private static String GetOscProvidersKeyLocation()
        {
            //Determine whether the operating system is 64-bit.
            bool is64bitOS = System.Environment.Is64BitOperatingSystem;

            //Determine whether Office is 64-bit.
            bool isOutlook64bit = IsOffice64Bit();

            //Define the location of the OSC Social Providers key.
            //The location to the OSC Social Providers key
            //  will be in the Wow6432Node branch if 32-bit Office 
            //  is installed on a 64-bit OS.
            String oscProvidersLocation = String.Empty;
            if (is64bitOS && !isOutlook64bit)
            {
                oscProvidersLocation =
                    @"Software\Wow6432Node\Microsoft\Office\Outlook\SocialConnector\SocialProviders";
            }
            else
            {
                oscProvidersLocation =
                    @"Software\Microsoft\Office\Outlook\SocialConnector\SocialProviders";
            }
            return oscProvidersLocation;
        }

    }
}
