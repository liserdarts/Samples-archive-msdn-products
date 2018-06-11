// OSCProfile.h : Declaration of the COSCProfile

#pragma once
#include "resource.h"       // main symbols

#include "OSCProvider_CPP_i.h"

// COSCProfile

class ATL_NO_VTABLE COSCProfile :
	public CComObjectRootEx<CComSingleThreadModel>,
	public ISocialProfile
{
	public:
		COSCProfile()
		{
		}

		BEGIN_COM_MAP(COSCProfile)
			COM_INTERFACE_ENTRY(ISocialProfile)
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
		
		// ISocialProfile Methods
		STDMETHOD(AreFriendsOrColleagues)(SAFEARRAY * userIDs, SAFEARRAY * * results);
		STDMETHOD(GetActivitiesOfFriendsAndColleagues)(DATE startTime, BSTR * activitiesCollection);
		STDMETHOD(SetStatus)(BSTR status);
};
