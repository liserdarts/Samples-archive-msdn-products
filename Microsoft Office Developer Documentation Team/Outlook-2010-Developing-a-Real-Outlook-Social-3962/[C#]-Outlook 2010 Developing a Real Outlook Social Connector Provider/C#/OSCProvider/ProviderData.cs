using System;
using OSCProvider.Schema;

namespace OSCProvider
{
    public class ProviderData
    {
        /// <summary>
        /// This Guid should always be the same for your provider. It shouldn't change even between versions.
        /// </summary>
        public Guid NetworkGuid;

        /// <summary>
        /// The friendly name for your network (e.g. "Facebook", "Twitter", etc)
        /// </summary>
        public string NetworkName;

        /// <summary>
        /// The version number of your provider. Should be in the y.xxxx format.
        /// </summary>
        public string Version;

        /// <summary>
        /// Byte array representing your provider's icon. 16x16 pixels works best. Can be bmp, jpg, or png format.
        /// </summary>
        public byte[] Icon;

        /// <summary>
        /// The Urls for your network.
        /// </summary>
        public string[] Urls;

        /// <summary>
        /// The URL of the webpage to be displayed by the OSC for forms-based authentication. If using Basic authentication or auto-configured sessions, you do not need to set this value.
        /// </summary>
        public string LogonUrl;

        public ProviderSchemaVersion SchemaVersion = ProviderSchemaVersion.v1_0;

        /// <summary>
        /// The capabilities the provider supports.
        /// </summary>
        public Capabilities ProviderCapabilities;

        public void SetIconFromUrl(Uri url)
        {
            Icon = Helpers.GetBytesFromUrl(url);
        }

        internal bool m_CalledAfterLoad = false;
    }

    public enum ProviderSchemaVersion
    {
        v1_0,
        v1_1
    }
}
