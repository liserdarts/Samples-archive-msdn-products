// OSCSession.cpp : Implementation of COSCSession

#include "stdafx.h"
#include "OSCSession.h"
#include "OSCProfile.h"
#include "OSCPerson.h"

// Log on using the given information
STDMETHODIMP COSCSession::Logon(BSTR userName, BSTR password)
{
	// This sample provider does not need to do any work to log on
	return S_OK;
}

// Pass the URL typed in the account configuration dialog to the provider
STDMETHODIMP COSCSession::put_SiteUrl(BSTR url)
{
	// The sample provider does not use the URL field
	return OSC_E_NOT_IMPLEMENTED;
}

// Log on using the given opaque binary data in connectIn.
// connectOut is the output string that the OSC should pass back in for connectIn
// on the next attempt at authentication
STDMETHODIMP COSCSession::LogonWeb(BSTR connectIn, BSTR * connectOut)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Get the URL we should open in order to allow the user to authenticate
// to this provider.
// After the user interacts with the given Url, the LogonWeb method will be
// called with an empty ConnectIn parameter.  This call to LogonWeb is expected
// to succeed assuming the user entered proper logon information.
STDMETHODIMP COSCSession::GetLogonUrl(BSTR * url)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Get a string that distinguishes an instance of the network from other instances
// This could be the siteURL value to distinguish a session from other sessions to
// other servers.
STDMETHODIMP COSCSession::GetNetworkIdentifier(BSTR * networkIdentifier)
{
	// We only have one network, so we will return an empty string
	*networkIdentifier = SysAllocString(L"");
	return (*networkIdentifier != NULL) ? S_OK : OSC_E_OUT_OF_MEMORY;
}

// Return an ISocialProfile representing the currently logged on user
STDMETHODIMP COSCSession::GetLoggedOnUser(ISocialProfile * * user)
{
	CComObject<COSCProfile> *pProfile;
	CComObject<COSCProfile>::CreateInstance(&pProfile);

	return pProfile->QueryInterface(__uuidof(ISocialProfile), (void **)user);
}

// Return the userID of the logged in user
STDMETHODIMP COSCSession::get_LoggedOnUserID(BSTR * userID)
{
	*userID = SysAllocString(L"user1");
	return (*userID != NULL) ? S_OK : OSC_E_OUT_OF_MEMORY;
}

// Return the user name of the logged in user
STDMETHODIMP COSCSession::get_LoggedOnUserName(BSTR * userName)
{
	*userName = SysAllocString(L"User 1");
	return (*userName != NULL) ? S_OK : OSC_E_OUT_OF_MEMORY;
}

// Get the person with the given userID
STDMETHODIMP COSCSession::GetPerson(BSTR userID, ISocialPerson * * person)
{
	CComObject<COSCPerson> *pPerson;
	CComObject<COSCPerson>::CreateInstance(&pPerson);

	return pPerson->QueryInterface(__uuidof(ISocialPerson), (void **)person);
}

// Find the person with the given userID
STDMETHODIMP COSCSession::FindPerson(BSTR userID, BSTR * result)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Not supported since OSC 1.1
STDMETHODIMP COSCSession::GetActivities(SAFEARRAY * emailAddresses, DATE startTime, BSTR * activities)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Find the person with the given userID
STDMETHODIMP COSCSession::FollowPerson(BSTR emailAddress)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Find the person with the given userID
STDMETHODIMP COSCSession::UnFollowPerson(BSTR userID)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Returns a string that contains a collection of person and picture details
// for the users specified by the personsAddresses string
STDMETHODIMP COSCSession::GetPeopleDetails(BSTR personsAddresses, BSTR *personsCollection)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Gets a string that represents a collection of activities for the person 
// specified by the hashedAddresses parameter.
STDMETHODIMP COSCSession::GetActivitiesEx(SAFEARRAY * hashedAddresses, DATE startTime, BSTR *activities)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Follows the person identified by the emailAddresses and displayName 
// parameters on the social network.
STDMETHODIMP COSCSession::FollowPersonEx(SAFEARRAY * emailAddresses, BSTR displayName)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Logs on to the social network site using cached credentials.
STDMETHODIMP COSCSession::LogonCached(BSTR connectIn, BSTR userName, BSTR password, BSTR * connectOut)
{
	return OSC_E_NOT_IMPLEMENTED;
}
