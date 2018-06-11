using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Text;


namespace OSCProvider
{
    [CLSCompliant(true)]
    [RunInstaller(true)]
    public abstract partial class OSCInstaller : System.Configuration.Install.Installer
    {
        public abstract string FriendlyName { get; }
        public abstract string Url { get; }
        public abstract string ProgId { get; }


        public OSCInstaller()
        {
            InitializeComponent();
        }


        public static bool Is64BitOS()
        {
            return (GetPlatform() == Platform.X64);
        }
        private static bool BitnessMismatch()
        {
            bool os64 = Is64BitOS();
            return (os64 && IsOffice64Bit(os64));
        }
        private static string GetRegAsmPath()
        {

            bool os64 = Is64BitOS();
            bool office64 = IsOffice64Bit(os64);

            // Get the location of regasm

            string runtimeVersion = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();
            string runtimeFolder32 = null;
            string runtimeFolder64 = null;


            if (os64)
            {


                runtimeFolder64 = ReadRegKey(HKEY_LOCAL_MACHINE,
                    @"Software\Microsoft\.NETFramework",
                    "InstallRoot",
                    Platform.X64).ToString();


                runtimeFolder32 = ReadRegKey(HKEY_LOCAL_MACHINE,
                    @"Software\Microsoft\.NETFramework",
                    "InstallRoot",
                    Platform.X86).ToString();

            }
            else
            {
                runtimeFolder32 = ReadRegKey(HKEY_LOCAL_MACHINE,
                    @"Software\Microsoft\.NETFramework",
                    "InstallRoot",
                    Platform.X86).ToString();
            }


            // Execute regasm
            string regasmPath = System.IO.Path.Combine(
                office64 ? runtimeFolder64 : runtimeFolder32,
                runtimeVersion);
            regasmPath = System.IO.Path.Combine(regasmPath,
            "regasm.exe");

            return regasmPath;
        }
        public static int GetOfficeVersion(bool os64)
        {
            return Convert.ToInt32(ReadIntRegKey(HKEY_LOCAL_MACHINE, @"Software\Microsoft\Office\Common", "LastAccessInstall", os64 ? Platform.X64 : Platform.X86));
        }
        public static bool IsOffice64Bit(bool os64)
        {
            if (!os64) return false;

            bool office64 = false;
            string bitness = string.Empty;
            try
            {
                //Make crazy Win32 API calls because we're not sure if Office is 32 bit or not
                int iOfficeVersion = GetOfficeVersion(os64);
                bitness = ReadRegKey(HKEY_LOCAL_MACHINE, string.Format(@"Software\Microsoft\Office\{0}.0\Outlook", iOfficeVersion), "Bitness", os64 ? Platform.X64 : Platform.X86);
            }
            catch { }

            office64 = (bitness == "x64");
            return office64;
        }
        
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);


            // Get the location of our DLL
            string componentPath = this.GetType().Assembly.Location;
            string regasmPath = GetRegAsmPath();
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(regasmPath, "/codebase \"" + componentPath + "\"");
            p.WaitForExit(15000);//wait a max of 15 seconds for regasm to complete.

            if (IsOffice64Bit(Is64BitOS()))
            {
                //We're running in a 32-bit process (no matter what the platform)
                //So if Office is 64-bit, we need to make sure we get the x64 registry
                string pathToProgId =  @"Software\Microsoft\Office\Outlook\SocialConnector\SocialProviders\" + ProgId;
                CreateKey(HKEY_CURRENT_USER, Platform.X64,pathToProgId);
                SetValue(HKEY_CURRENT_USER,pathToProgId, "FriendlyName", FriendlyName, RegistryValueKind.String,Platform.X64);
                SetValue(HKEY_CURRENT_USER, pathToProgId, "Url", Url, RegistryValueKind.String, Platform.X64);
            }
            else
            {
                //Office is 32-bit. Since we're in a 32-bit process, we're good to just use .net classes
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
                if (IsOffice64Bit(Is64BitOS()))
                {
                    //Office is 64-bit so we need to get to the 64-bit registry hive
                    //from a 32-bit process.
                    
                    DeleteKey(HKEY_CURRENT_USER, @"Software\Microsoft\Office\Outlook\SocialConnector\SocialProviders", ProgId, Platform.X64);
                }
                else
                {
                    //Office is 32-bit, so we can just use .net since we're in a 
                    //32-bit process
                    RegistryKey keyOutlook = null;
                    try
                    {
                        keyOutlook = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Outlook", true);
                    }
                    catch { }
                    if (keyOutlook != null)
                    {
                        RegistryKey keyOSC = keyOutlook.CreateSubKey(@"SocialConnector\SocialProviders");
                        if (keyOSC != null)
                        {
                            keyOSC.DeleteSubKeyTree(ProgId);
                        }
                    }
                }

                string componentPath = this.GetType().Assembly.Location;
                string regasmPath = GetRegAsmPath();
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(regasmPath, "/unregister \"" + componentPath + "\"");
                p.WaitForExit();
            }
            catch
            {

            }


        }

        #region 3_5_workarounds
        private enum Platform
        {
            X86,
            X64,
            Unknown
        }

        #region Win32API Constants
        private const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        private const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        private const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        private const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;
        private static UIntPtr HKEY_CURRENT_USER = (UIntPtr)0x80000001;
        private static UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr)0x80000002;
        private const uint KEY_QUERY_VALUE = 0x0001;
        private const uint KEY_SET_VALUE = 0x0002;
        private const uint KEY_CREATE_SUB_KEY = 0x0004;
        private const uint KEY_ENUMERATE_SUB_KEYS = 0x0008;
        private const uint KEY_WOW64_64KEY = 0x0100;
        private const uint KEY_WOW64_32KEY = 0x0200;
        private const uint KEY_READ = 0x20019;
        private const uint KEY_WRITE = 0x20006;
        #endregion
        #region Win32API
        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        };

        [DllImport("kernel32.dll")]
        static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyExW", SetLastError = true)]
        private static extern int RegOpenKeyEx(UIntPtr hKey, string subKey, uint options, uint sam, out UIntPtr phkResult);
        [DllImport("advapi32.dll", EntryPoint = "RegQueryValueEx")]
        private static extern int RegQueryValueEx(UIntPtr hKey, string lpValueName, int lpReserved, out uint lpType, System.Text.StringBuilder lpData, ref uint lpcbData);
        [DllImport("advapi32.dll", EntryPoint = "RegQueryValueEx")]
        private static extern int IntRegQueryValueEx(UIntPtr hKey, string lpValueName, int lpReserved, out uint lpType, out uint lpData, ref uint lpcbData);
       
        [DllImport("advapi32.dll", EntryPoint="RegCreateKeyExW", CharSet=CharSet.Unicode, ExactSpelling=true, SetLastError=true)]
        private static extern int RegCreateKey(UIntPtr key, string subkey, uint reserved, string className, uint options, uint desiredSam, uint securityAttributes, out UIntPtr openedKey, out uint disposition);
        [DllImport("advapi32.dll", EntryPoint = "RegCloseKey")]
        private static extern int RegCloseKey(UIntPtr hKey);
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int RegSetValueEx(UIntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string lpValueName, int Reserved, Microsoft.Win32.RegistryValueKind dwType, IntPtr lpData, int cbData);
        [DllImport("advapi32.dll", EntryPoint = "RegDeleteKeyEx", SetLastError = true)]
        private static extern int RegDeleteKeyEx(UIntPtr hKey, string lpSubKey, uint samDesired, uint Reserved);
        #endregion
        #region Win32API helpers
        private static Platform GetPlatform()
        {
            SYSTEM_INFO sysInfo = new SYSTEM_INFO();
            GetNativeSystemInfo(ref sysInfo);

            switch (sysInfo.wProcessorArchitecture)
            {
                case PROCESSOR_ARCHITECTURE_AMD64:
                    return Platform.X64;

                case PROCESSOR_ARCHITECTURE_INTEL:
                    return Platform.X86;

                default:
                    return Platform.Unknown;
            }
        }
        private static string ReadRegKey(UIntPtr rootKey, string keyPath, string valueName, Platform hive)
        {
            UIntPtr hKey = UIntPtr.Zero;
            uint sam = KEY_READ;
            if ((GetPlatform() == Platform.X64))
            {
                if (Platform.X64 == hive)
                    sam |= KEY_WOW64_64KEY;
                else if (Platform.X86 == hive)
                    sam |= KEY_WOW64_32KEY;
            }

            if (RegOpenKeyEx(rootKey, keyPath, 0, sam, out hKey) == 0)
            {
                uint size = 1024;
                uint type;
                string keyValue = null;
                StringBuilder keyBuffer = new StringBuilder(Convert.ToInt32(size));

                if (RegQueryValueEx(hKey, valueName, 0, out type, keyBuffer, ref size) == 0)
                    keyValue = keyBuffer.ToString();

                RegCloseKey(hKey);

                return (keyValue);
            }

            return (null);  // Return null if the value could not be read


        }
        private static uint ReadIntRegKey(UIntPtr rootKey, string keyPath, string valueName, Platform hive)
        {
            UIntPtr hKey = UIntPtr.Zero;
            uint sam = KEY_READ;
            if ((GetPlatform() == Platform.X64))
            {
                if (Platform.X64 == hive)
                    sam |= KEY_WOW64_64KEY;
                else if (Platform.X86 == hive)
                    sam |= KEY_WOW64_32KEY;
            }

            if (RegOpenKeyEx(rootKey, keyPath, 0, sam, out hKey) == 0)
            {
                uint size = 1024;
                uint type;

                uint keyBuffer;

                IntRegQueryValueEx(hKey, valueName, 0, out type, out keyBuffer, ref size);


                RegCloseKey(hKey);

                return (keyBuffer);
            }

            return (0);  // Return null if the value could not be read

        }
        private static int SetValue(UIntPtr rootKey, string Path, string Name, object Value, Platform hive)
        {
            return SetValue(rootKey, Path, Name, Value, RegistryValueKind.Unknown,hive);
        }
        private static int SetValue(UIntPtr rootKey, string Path, string Name, object Value, RegistryValueKind RegType, Platform hive)
        {
            GCHandle gch = new GCHandle();
            IntPtr ptr;
            int Size;
            //  So we have to figure out the type
            if ((RegType == RegistryValueKind.Unknown))
            {
                switch (Value.GetType().ToString())
                {
                    case "System.String":
                        RegType = RegistryValueKind.String;
                        break;
                    case "System.Int32":
                        RegType = RegistryValueKind.DWord;
                        break;
                    case "System.Int64":
                        RegType = RegistryValueKind.QWord;
                        break;
                    case "System.String[]":
                        RegType = RegistryValueKind.MultiString;
                        break;
                    case "System.Byte[]":
                        RegType = RegistryValueKind.Binary;
                        break;
                    default:
                        RegType = RegistryValueKind.String;
                        Value = Value.ToString();
                        break;
                }
            }
            switch (RegType)
            {
                case RegistryValueKind.Binary:
                    {
                        byte[] temp = (byte[])
                            Value;

                        Size = temp.Length;
                        gch = GCHandle.Alloc(temp, GCHandleType.Pinned);
                        ptr = Marshal.UnsafeAddrOfPinnedArrayElement(temp, 0);
                    }
                    break;
                case RegistryValueKind.DWord:
                    {
                        int temp = (int)Value;
                        Size = 4;
                        ptr = Marshal.AllocHGlobal(Size);
                        Marshal.WriteInt32(ptr, 0, temp);
                    }
                    break;
                case RegistryValueKind.ExpandString:
                    {
                        string temp = Value.ToString();
                        Size = ((temp.Length + 1) * Marshal.SystemDefaultCharSize);
                        ptr = Marshal.StringToHGlobalAuto(temp);
                    }
                    break;
                case RegistryValueKind.MultiString:
                    {
                        string[] lines = (string[])Value;
                        //  Calculate the total size, including the terminating null
                        Size = 0;
                        foreach (string s in lines)
                        {
                            Size +=  (s.Length + 1) * Marshal.SystemDefaultCharSize;
                        }
                        Size += Marshal.SystemDefaultCharSize;
                        ptr = Marshal.AllocHGlobal(Size);
                        int index = 0;
                        foreach (string s in lines)
                        {
                            IntPtr tempPtr;
                            char[] tempArray = s.ToCharArray();
                            tempPtr = new IntPtr(ptr.ToInt64() + index);
                            Marshal.Copy(tempArray, 0, tempPtr, tempArray.Length);
                            index += (tempArray.Length + 1) * Marshal.SystemDefaultCharSize;
                        }
                    }
                    break;
                case RegistryValueKind.QWord:
                    {
                        long temp = (long)Value;
                        Size = 8;
                        ptr = Marshal.AllocHGlobal(Size);
                        Marshal.WriteInt64(ptr, 0, temp);
                    }
                    break;
                case RegistryValueKind.String:
                    {
                        string temp = Value.ToString();
                        Size = ((temp.Length + 1) * Marshal.SystemDefaultCharSize);
                        ptr = Marshal.StringToHGlobalAuto(temp);
                    }
                    break;
                default:
                    throw new ApplicationException("Registry type of " + RegType + " is not supported");
                   
            }
            //  let's do it!


            UIntPtr hKey = UIntPtr.Zero;
            uint sam = KEY_SET_VALUE;
            if ((GetPlatform() == Platform.X64))
            {
                if (Platform.X64 == hive)
                    sam |= KEY_WOW64_64KEY;
                else if (Platform.X86 == hive)
                    sam |= KEY_WOW64_32KEY;
            }
            int HRES = 0;
            HRES = RegOpenKeyEx(rootKey, Path, 0, sam, out hKey);
            if (HRES == 0)
            {
                HRES = RegSetValueEx(hKey, Name, 0, RegType, ptr, Size);
                if ((HRES != 0))
                {
                    throw new Win32Exception(HRES);
                }
                //  clean up
                if (gch.IsAllocated)
                {
                    gch.Free();
                }
                else
                {
                    Marshal.FreeHGlobal(ptr);
                }
                RegCloseKey(hKey);
            }
            return HRES;
        }
        private int CreateKey(UIntPtr hKey, Platform hive, string subkey)
        {
            uint sam = KEY_CREATE_SUB_KEY | KEY_ENUMERATE_SUB_KEYS | KEY_SET_VALUE | KEY_READ;
            UIntPtr newKey = UIntPtr.Zero;
            uint disposition = 0;
            int HRES = 0;
            if ((GetPlatform() == Platform.X64))
            {
                if (Platform.X64 == hive)
                    sam |= KEY_WOW64_64KEY;
                else if (Platform.X86 == hive)
                    sam |= KEY_WOW64_32KEY;
            }
            HRES = RegCreateKey(hKey, subkey, 0, null, 0,sam, 0, out newKey, out disposition);
            RegCloseKey(newKey);
            return HRES;
        }
        private static int DeleteKey(UIntPtr rootKey,string parentKeyPath,string keyName,Platform hive){
                        UIntPtr hKey = UIntPtr.Zero;
            uint sam = KEY_WRITE | KEY_READ;
            int HRES = 0;
            if ((GetPlatform() == Platform.X64))
            {
                if (Platform.X64 == hive)
                    sam |= KEY_WOW64_64KEY;
                else if (Platform.X86 == hive)
                    sam |= KEY_WOW64_32KEY;
            }
            HRES = RegOpenKeyEx(rootKey, parentKeyPath, 0, sam, out hKey);
            if (HRES == 0)
            {
                HRES = RegDeleteKeyEx(hKey, keyName, sam, 0);
            }
            return HRES;
        }
        #endregion
        #endregion
    }
}
