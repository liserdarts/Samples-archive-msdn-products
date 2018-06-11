// OSCProvider.h : Declaration of the COSCProvider

#pragma once
#include "resource.h"       // main symbols
#include "ids.h"

#include "OSCProvider_CPP_i.h"

// COSCProvider

class ATL_NO_VTABLE COSCProvider :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<COSCProvider, &CLSID_OSCProvider>,
	public ISocialProvider
{
	public:
		COSCProvider()
		{
		}

		// Setup the registration found in addin.rgs
		static HRESULT WINAPI UpdateRegistry(BOOL bRegister) throw()
		{
			ATL::_ATL_REGMAP_ENTRY regMapEntries[] =
			{ { OLESTR("PROVIDER_CLASS_FRIENDLY_NAME"), PROVIDER_CLASS_FRIENDLY_NAME_STR }
			, { OLESTR("PROVIDER_CLSID"), PROVIDER_CLSID_STR }
			, { OLESTR("PROVIDER_PROGID"), PROVIDER_PROGID_STR }
			, { OLESTR("TYPELIB_GUID"), TYPELIB_GUID_STR }
			, { NULL, NULL }
			};

			return ATL::_pAtlModule->UpdateRegistryFromResource(IDR_OSCPROVIDER, bRegister, regMapEntries);
		}
		
		BEGIN_COM_MAP(COSCProvider)
			COM_INTERFACE_ENTRY(ISocialProvider)
		END_COM_MAP()

		DECLARE_PROTECT_FINAL_CONSTRUCT()

		HRESULT FinalConstruct()
		{
			return S_OK;
		}

		void FinalRelease()
		{
		}

		// ISocialProvider Methods
		STDMETHOD(GetCapabilities)(BSTR * capabilities);
		STDMETHOD(get_Version)(BSTR * Version);
		STDMETHOD(GetSession)(ISocialSession * * session);
		STDMETHOD(GetAutoConfiguredSession)(ISocialSession * * session);
		STDMETHOD(get_DefaultSiteUrls)(SAFEARRAY * * siteURLs);
		STDMETHOD(get_SocialNetworkIcon)(SAFEARRAY * * networkIcon);
		STDMETHOD(get_SocialNetworkName)(BSTR * networkName);
		STDMETHOD(get_SocialNetworkGuid)(GUID* guid);
		STDMETHOD(GetStatusSettings)(BSTR * statusDefault, int * maxStatusLength);
		STDMETHOD(Load)(BSTR socialProviderInterfaceVersion, BSTR languageTag);
};

OBJECT_ENTRY_AUTO(__uuidof(OSCProvider), COSCProvider)
