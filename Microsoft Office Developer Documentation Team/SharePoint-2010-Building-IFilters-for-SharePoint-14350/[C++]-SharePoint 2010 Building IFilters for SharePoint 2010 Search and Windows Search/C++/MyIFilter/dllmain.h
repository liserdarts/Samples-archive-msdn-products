// dllmain.h : Declaration of module class.

class CMyIFilterModule : public ATL::CAtlDllModuleT< CMyIFilterModule >
{
public :
	DECLARE_LIBID(LIBID_MyIFilterLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_MYIFILTER, "{DDD9192C-9D57-48EB-AC5D-9B3E35B5ADBA}")
};

extern class CMyIFilterModule _AtlModule;
