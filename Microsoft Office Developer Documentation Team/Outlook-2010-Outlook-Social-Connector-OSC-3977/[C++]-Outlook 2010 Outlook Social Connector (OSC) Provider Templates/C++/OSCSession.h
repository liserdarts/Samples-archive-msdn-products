// OSCSession.h : Declaration of the COSCSession

#pragma once
#include "resource.h"       // main symbols

#include "OSCProvider_CPP_i.h"

// COSCSession

class ATL_NO_VTABLE COSCSession :
	public CComObjectRootEx<CComSingleThreadModel>,
	public ISocialSession,
	public ISocialSession2
{
	public:
		COSCSession()
		{
		}

		BEGIN_COM_MAP(COSCSession)
			COM_INTERFACE_ENTRY(ISocialSession)
			COM_INTERFACE_ENTRY(ISocialSession2)
		END_COM_MAP()

		DECLARE_PROTECT_FINAL_CONSTRUCT()

		HRESULT FinalConstruct()
		{
			return S_OK;
		}

		void FinalRelease()
		{
		}

		// ISocialSession Methods
		STDMETHOD(Logon)(BSTR userName, BSTR password);
		STDMETHOD(put_SiteUrl)(BSTR );
		STDMETHOD(LogonWeb)(BSTR connectIn, BSTR * connectOut);
		STDMETHOD(GetLogonUrl)(BSTR * url);
		STDMETHOD(GetNetworkIdentifier)(BSTR * networkIdentifier);
		STDMETHOD(GetLoggedOnUser)(ISocialProfile * * user);
		STDMETHOD(get_LoggedOnUserID)(BSTR * userID);
		STDMETHOD(get_LoggedOnUserName)(BSTR * userName);
		STDMETHOD(GetPerson)(BSTR userID, ISocialPerson * * person);
		STDMETHOD(FindPerson)(BSTR userID, BSTR * result);
		STDMETHOD(GetActivities)(SAFEARRAY * emailAddresses, DATE startTime, BSTR * activities);
		STDMETHOD(FollowPerson)(BSTR emailAddress);
		STDMETHOD(UnFollowPerson)(BSTR userID);

		// ISocialSession2 Methods
		STDMETHOD(GetPeopleDetails)(BSTR personsAddresses, BSTR *personsCollection);
		STDMETHOD(GetActivitiesEx)(SAFEARRAY * hashedAddresses, DATE startTime, BSTR * activities);
		STDMETHOD(FollowPersonEx)(SAFEARRAY * emailAddresses, BSTR displayName);
		STDMETHOD(LogonCached)(BSTR connectIn, BSTR userName, BSTR password, BSTR *connectOut);
};
