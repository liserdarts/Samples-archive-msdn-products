// dllmain.h : Declaration of module class.

class CDemoProviderModule : public CAtlDllModuleT< CDemoProviderModule >
{
public :
	DECLARE_LIBID(LIBID_DemoProviderLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_DEMOPROVIDER, "{8EC63ACB-909A-4EF2-AA54-11A90D670F75}")
};

extern class CDemoProviderModule _AtlModule;
