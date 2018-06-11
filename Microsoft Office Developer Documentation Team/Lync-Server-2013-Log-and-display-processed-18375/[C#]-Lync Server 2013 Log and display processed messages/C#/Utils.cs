/*++

Copyright © Microsoft Corporation

Module Name:

    Utils.cs
	
Abstract:

    This module implements the URI helper functions.
	
--*/
using System;
using System.Management;
using System.Diagnostics;


namespace Microsoft.Rtc.Sip.SDK.Samples.Utils
{
	#region URIParser

	/// <summary>
	/// A simple utility class to parse SIP From and To headers,
	/// and extract the user@host uri
	/// </summary>
	public class SipUriParser
	{

		/// <summary>
		/// Parse a SIP address header (specifically From or To)
		/// and return the user@host 
		/// </summary>
		/// <returns>user@host if parsable, null if not</returns>
		public static string GetUserAtHost(string sipAddressHeader)
		{
			if (sipAddressHeader == null) return null;
			
			string uri = null;

			/// If the header has < > present, then extract the uri
			/// else treat the input as uri
			int index1 = sipAddressHeader.IndexOf('<');

			if (index1 != -1)
			{	
				int index2 = sipAddressHeader.IndexOf('>');
				///address, extract uri
				uri = sipAddressHeader.Substring(index1 + 1, index2 - index1 - 1);
			}
			else
			{
				uri = sipAddressHeader;
			}
	
			///chop off all parameters. we assume that there is no
			///semicolon in the user part (which is allowed in some cases!)
			index1 = uri.IndexOf(';');
			if (index1 != -1)
			{
				uri = uri.Substring(0, index1 - 1);
			}

			///we will process only SIP uri (thus no sips or tel)
			///and wont accept those without user names
			if (uri.StartsWith("sip:") == false || 
				uri.IndexOf('@') == -1) 
				return null;
			
			///now we have sip:user@host most likely, with some exceptions that
			/// are ignored
			///  1) user part contains semicolon separated user parameters
			///  2) user part also has the password (as in sip:user:pwd@host)
			///  3) some hex escaped characters are present in user part
			///  4) the host part also has the port (Contact header for example)

			return uri.Substring("sip:".Length /* uri.Substring(4) */);
		}
	}

	#endregion URIParser
}
