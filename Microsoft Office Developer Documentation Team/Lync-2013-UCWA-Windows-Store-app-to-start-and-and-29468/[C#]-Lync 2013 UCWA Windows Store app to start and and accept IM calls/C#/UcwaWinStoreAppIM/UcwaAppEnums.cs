using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStoreUcwaAppIM
{
    /// <summary>
    /// To enable the "Windows" authentication type, do the following before the app is built:
    ///     opens the Package.appxmanifest file,
    ///     Select the Capabilities tab and check the "Enterprise Authentication" option.
    ///     
    /// </summary>
    public enum UcwaAppAuthenticationTypes { Password, Windows, Passive, Annonymous }
}
