
namespace OSCProvider
{
    public enum OSCExceptions
    {
        /// <summary>
        /// (E_NOTIMPL) The request to the social network site is valid but has not been implemented by the social network site.
        /// </summary>
          OSC_E_NOT_IMPLEMENTED = unchecked((int)0x80004001),

        /// <summary>
        /// (E_OUTOFMEMORY) An out-of-memory error occurred.
        /// </summary>
           OSC_E_OUT_OF_MEMORY = unchecked((int)0x8007000E),

        /// <summary>
        /// (E_INVALIDARG) An invalid argument was passed to a function.
        /// </summary>
         OSC_E_INVALIDARG = unchecked((int)0x80070057),
        
        /// <summary>
        /// An internal error occurred because of an invalid operation.
        /// </summary>
         OSC_E_INTERNAL_ERROR = unchecked((int)0x80041400),

        /// <summary>
        /// The provider does not support this version of OSC provider extensibility.
        /// </summary>
         OSC_E_VERSION = unchecked((int)0x80041401),

        /// <summary>
        /// No connection is available to connect to the social network site.
        /// </summary>
         OSC_E_COULDNOTCONNECT = unchecked((int)0x80041402),

        /// <summary>
        /// The OSC provider denied permission for the resource.
        /// </summary>
         OSC_E_PERMISSION_DENIED = unchecked((int)0x80041403),

        /// <summary>
        /// Authentication failed on the network of the social network site.
        /// </summary>
         OSC_E_AUTH_ERROR = unchecked((int)0x80041404),

        /// <summary>
        /// A resource cannot be found.
        /// </summary>
         OSC_E_NOT_FOUND = unchecked((int)0x80041405),

        //I have no idea if this is right.
         OSC_E_NO_CHANGES = unchecked((int)0x80041406 )
    }
}
