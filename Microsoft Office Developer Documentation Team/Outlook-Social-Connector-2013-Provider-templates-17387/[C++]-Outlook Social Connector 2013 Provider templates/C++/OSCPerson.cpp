// OSCPerson.cpp : Implementation of COSCPerson

#include "stdafx.h"
#include "OSCPerson.h"

// Not supported since OSC 2013
STDMETHODIMP COSCPerson::GetActivities(DATE startTime, BSTR * activities)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Return an XML stream of the person's friends/colleagues and their details
STDMETHODIMP COSCPerson::GetFriendsAndColleagues(BSTR * personsCollection)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Return an array of IDs of the person's friends/colleagues
STDMETHODIMP COSCPerson::GetFriendsAndColleaguesIDs(SAFEARRAY * * friendsIDs)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Get the person's picture in a byte array
STDMETHODIMP COSCPerson::GetPicture(SAFEARRAY * * picture)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Not supported
STDMETHODIMP COSCPerson::GetStatus(BSTR * status)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Get an XML stream of the person's details
STDMETHODIMP COSCPerson::GetDetails(BSTR * details)
{
	return OSC_E_NOT_IMPLEMENTED;
}
