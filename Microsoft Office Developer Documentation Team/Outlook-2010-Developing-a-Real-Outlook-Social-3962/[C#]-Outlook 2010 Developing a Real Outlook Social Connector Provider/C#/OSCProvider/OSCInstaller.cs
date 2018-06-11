using System.Collections;
using System.ComponentModel;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace OSCProvider
{
    [RunInstaller(true)]
    [CLSCompliant(true)]
    public abstract partial class OSCInstaller : System.Configuration.Install.Installer
    {

        public OSCInstaller()
        {
            InitializeComponent();
        }


        public abstract string FriendlyName {get;}
        public abstract string Url { get; }
        public abstract string ProgId { get; }

        private static string GetRegAsmPath()
        {

            bool os64 = System.Environment.Is64BitOperatingSystem;
            bool office64 = IsOffice64Bit(os64);

            // Get the location of regasm

            string runtimeVersion = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();
            string runtimeFolder32 = null;
            string runtimeFolder64 = null;


            if (os64)
            {
                RegistryKey hklmKey64 = null;
                RegistryKey hklmKey32 = null;
                try
                {
                    hklmKey64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                    hklmKey32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

                    runtimeFolder64 = hklmKey64.OpenSubKey(
                        @"Software\Microsoft\.NETFramework").GetValue(
                        "InstallRoot",
                        null).ToString();


                    runtimeFolder32 = hklmKey32.OpenSubKey(
                        @"Software\Wow6432Node\Microsoft\.NETFramework").GetValue(
                        "InstallRoot",
                        null).ToString();
                }
                finally
                {
                    hklmKey32.Dispose();
                    hklmKey64.Dispose();
                }
            }
            else
            {
                runtimeFolder32 = Registry.GetValue(
                     @"HKEY_LOCAL_MACHINE\Software\Microsoft\.NETFramework",
                     "InstallRoot",
                     null).ToString();
            }


            // Execute regasm
            string regasmPath = System.IO.Path.Combine(
                office64 ? runtimeFolder64 : runtimeFolder32,
                runtimeVersion,
                "regasm.exe");

            return regasmPath;
        }

        public static bool Is64BitOS()
        {
            return System.Environment.Is64BitOperatingSystem;
        }

        public static bool IsOffice64Bit(bool os64)
        {
            bool office64 = false;
            string bitness = string.Empty;
            try
            {
                RegistryKey hklmKey = null;
                try
                {
                    int iOfficeVersion = GetOfficeVersion(os64);
                    hklmKey = os64? RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64):RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
 
                    bitness = hklmKey.OpenSubKey(string.Format(@"Software\Microsoft\Office\{0}.0\Outlook", iOfficeVersion)).GetValue("Bitness", string.Empty).ToString();
                }
                catch { }
                finally
                {
                    hklmKey.Dispose();
                }
                office64 = (bitness == "x64");
            }
            catch
            {

            }
            return office64;
        }

        public static int GetOfficeVersion(bool os64)
        {
            RegistryKey hklmKey = null;
            int iOfficeVersion = 0;
            try
            {
                hklmKey = os64 ? RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64) : RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);

                iOfficeVersion = (int)hklmKey.OpenSubKey(@"Software\Microsoft\Office\Common").GetValue("LastAccessInstall", 0);
            }
            finally
            {
                hklmKey.Dispose();
            }
            return iOfficeVersion;
        }


        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(System.Collections.IDictionary stateSaver)
        {
             base.Install(stateSaver);
        }


        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void  Commit(IDictionary savedState)
{
    
 	         base.Commit(savedState);

            // Get the location of our DLL
            string componentPath = this.GetType().Assembly.Location;
            string regasmPath = GetRegAsmPath();
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(regasmPath, "/codebase \"" + componentPath + "\"");
            p.WaitForExit(15000);//wait a max of 15 seconds for regasm to complete.

            RegistryKey keyOutlook = null;
            try
            {
                keyOutlook = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Outlook", true);
            }
            catch { }
            if (keyOutlook != null)
            {
                RegistryKey keyOSC =
                    keyOutlook.CreateSubKey(@"SocialConnector\SocialProviders\" + ProgId);
                if (keyOSC != null)
                {
                    keyOSC.SetValue("FriendlyName", FriendlyName, RegistryValueKind.String);
                    keyOSC.SetValue("Url", Url, RegistryValueKind.String);
                }
            }

        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
            try
            {
                RegistryKey keyOutlook = null;
                try
                {
                    keyOutlook = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Outlook", true);
                }
                catch { }
                if (keyOutlook != null)
                {
                    RegistryKey keyOSC =
                        keyOutlook.CreateSubKey(@"SocialConnector\SocialProviders");
                    if (keyOSC != null)
                    {
                        keyOSC.DeleteSubKeyTree(ProgId, false);
                    }
                }

                string componentPath = this.GetType().Assembly.Location;
                string regasmPath = GetRegAsmPath();
                //System.IO.File.WriteAllText("c:\\temp\\uninstall.txt", componentPath + " " + regasmPath);
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(regasmPath, "/unregister \"" + componentPath + "\"");
                p.WaitForExit();
            }
            catch
            {
                
            }


        }

    }
}
