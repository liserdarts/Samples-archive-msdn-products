// dllmain.h : Declaration of module class.

#include "ids.h"

class COSCProvider_CPPModule : public CAtlDllModuleT< COSCProvider_CPPModule >
{
public :
	DECLARE_LIBID(LIBID_OSCProvider_CPPLib)

	static LPCOLESTR GetAppId() throw()
	{
		return APPID_STR;
	}
	static TCHAR* GetAppIdT() throw()
	{
		return APPID_STR_T;
	}
	static HRESULT WINAPI UpdateRegistryAppId(BOOL bRegister) throw()
	{
		ATL::_ATL_REGMAP_ENTRY aMapEntries [] =
		{
			{ OLESTR("APPID"), GetAppId() },
			{ OLESTR("PROVIDER_PROGNAME"), PROVIDER_PROGNAME_STR },
			{ NULL, NULL }
		};

		return ATL::_pAtlModule->UpdateRegistryFromResource(IDR_OSCPROVIDER_CPP, bRegister, aMapEntries);
	}

};

extern class COSCProvider_CPPModule _AtlModule;
