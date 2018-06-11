// OSCPerson.h : Declaration of the COSCPerson

#pragma once
#include "resource.h"       // main symbols

#include "OSCProvider_CPP_i.h"

// COSCPerson

class ATL_NO_VTABLE COSCPerson :
	public CComObjectRootEx<CComSingleThreadModel>,
	public ISocialPerson
{
	public:
		COSCPerson()
		{
		}

		BEGIN_COM_MAP(COSCPerson)
			COM_INTERFACE_ENTRY(ISocialPerson)
		END_COM_MAP()

		DECLARE_PROTECT_FINAL_CONSTRUCT()

		HRESULT FinalConstruct()
		{
			return S_OK;
		}

		void FinalRelease()
		{
		}

		// ISocialPerson Methods
		STDMETHOD(GetActivities)(DATE startTime, BSTR * activities);
		STDMETHOD(GetFriendsAndColleagues)(BSTR * personsCollection);
		STDMETHOD(GetFriendsAndColleaguesIDs)(SAFEARRAY * * friendsIDs);
		STDMETHOD(GetPicture)(SAFEARRAY * * picture);
		STDMETHOD(GetStatus)(BSTR * status);
		STDMETHOD(GetDetails)(BSTR * details);
};
