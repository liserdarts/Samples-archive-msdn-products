// C++ Provider Template for the Microsoft Outlook Social Connector (OSC)
// Copyright © 2009-2010 Microsoft Corporation
// Use this template to develop a provider for the OSC

// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

// OSCProvider.cpp : Implementation of COSCProvider

#include "stdafx.h"
#include "OSCProvider.h"
#include "OSCSession.h"
#include "Helper.h"
#include "ids.h"

// Return the XML capabilities blob for this provider
STDMETHODIMP COSCProvider::GetCapabilities(BSTR * capabilities)
{
	//You must define your capabilities based on the schema and documentation for OutlookSocialProvider.xsd
	*capabilities = SysAllocString(	L"<?xml version=\"1.0\" encoding=\"utf-16\" ?>"
									L"<capabilities xmlns=\"http://schemas.microsoft.com/office/outlook/2010/06/socialprovider.xsd\">"
									L"	<getFriends>true</getFriends>"
									L"	<cacheFriends>true</cacheFriends>"
									L"	<followPerson>true</followPerson>"
									L"	<doNotFollowPerson>true</doNotFollowPerson>"
									L"	<getActivities>true</getActivities>"
									L"	<cacheActivities>true</cacheActivities>"
									L"	<dynamicActivitiesLookup>false</dynamicActivitiesLookup>"
									L"	<displayUrl>false</displayUrl>"
									L"	<useLogonWebAuth>false</useLogonWebAuth>"
									L"	<hideHyperlinks>true</hideHyperlinks>"
									L"	<supportsAutoConfigure>false</supportsAutoConfigure>"
									L"	<dynamicActivitiesLookupEx>false</dynamicActivitiesLookupEx>"
									L"	<dynamicContactsLookup>true</dynamicContactsLookup>"
									L"	<useLogonCached>false</useLogonCached>"
									L"	<hideRememberMyPassword>false</hideRememberMyPassword>"
									L"	<createAccountUrl>http://</createAccountUrl>"
									L"	<forgotPasswordUrl>http://</forgotPasswordUrl>"
									L"	<showOnDemandActivitiesWhenMinimized>true</showOnDemandActivitiesWhenMinimized>"
									L"	<showOnDemandContactsWhenMinimized>true</showOnDemandContactsWhenMinimized>"
									L"  <hashFunction>MD5</hashFunction>"
									L"</capabilities>");
	return (*capabilities != NULL) ? S_OK : OSC_E_OUT_OF_MEMORY;
}

// Get the version number of this provider in the #.# format
STDMETHODIMP COSCProvider::get_Version(BSTR * Version)
{
	*Version = SysAllocString(PROVIDER_VERSION);
	return (*Version != NULL) ? S_OK : OSC_E_OUT_OF_MEMORY;
}

// Get a ISocialSession object
STDMETHODIMP COSCProvider::GetSession(ISocialSession * * session)
{
	CComObject<COSCSession> *pSession;
	CComObject<COSCSession>::CreateInstance(&pSession);

	return pSession->QueryInterface(__uuidof(ISocialSession), (void **)session);
}

// Get an ISocialSession object that has already been configured to log on
STDMETHODIMP COSCProvider::GetAutoConfiguredSession(ISocialSession * * session)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Get a list of URLs that may be shown to the user as possible URL selections
// In OSC 1.0, only the first elemnt of the array is currently used and shown
// as the pre-filled URL in the account configuration dialog
STDMETHODIMP COSCProvider::get_DefaultSiteUrls(SAFEARRAY * * siteURLs)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Get the icon in a byte array for this provider
// A PNG with transparency is suggested, but BMP and JPG work as well
STDMETHODIMP COSCProvider::get_SocialNetworkIcon(SAFEARRAY * * networkIcon)
{
	return GetPicture(networkIcon);
}

// Get the name of this social network
STDMETHODIMP COSCProvider::get_SocialNetworkName(BSTR * networkName)
{
	*networkName = SysAllocString(SOCIAL_NETWORK_NAME);
	return (*networkName != NULL) ? S_OK : OSC_E_OUT_OF_MEMORY;
}

// Get the immutable GUID for this social network
STDMETHODIMP COSCProvider::get_SocialNetworkGuid(GUID * guid)
{
	static const GUID CLSID_TEMP = CLSID_SOCIAL_NETWORK_GUID;

	*guid = CLSID_TEMP;
	return S_OK;
}

// Get status settings (Not used in OSC 1.0)
STDMETHODIMP COSCProvider::GetStatusSettings(BSTR * statusDefault, int * maxStatusLength)
{
	*statusDefault = SysAllocString(L"");
	*maxStatusLength = 256;
	return (*statusDefault != NULL) ? S_OK : OSC_E_OUT_OF_MEMORY;
}

// Perform any needed initialization. Any lengthy operation (such as but
// not limited to network operations) is highly discouraged
STDMETHODIMP COSCProvider::Load(BSTR socialProviderInterfaceVersion, BSTR languageTag)
{
	return S_OK;
}
