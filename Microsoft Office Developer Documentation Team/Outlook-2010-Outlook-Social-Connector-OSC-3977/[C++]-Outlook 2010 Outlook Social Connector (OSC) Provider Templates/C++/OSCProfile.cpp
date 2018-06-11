// OSCProfile.cpp : Implementation of COSCProfile

#include "stdafx.h"
#include "OSCProfile.h"

// Return the set of activities for the person
STDMETHODIMP COSCProfile::GetActivities(DATE startTime, BSTR * activities)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Return an XML stream of the person's friends/colleagues and their details
STDMETHODIMP COSCProfile::GetFriendsAndColleagues(BSTR * personsCollection)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Return an array of IDs of the person's friends/colleagues
STDMETHODIMP COSCProfile::GetFriendsAndColleaguesIDs(SAFEARRAY * * friendsIDs)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Get the person's picture in a byte array
STDMETHODIMP COSCProfile::GetPicture(SAFEARRAY * * picture)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Not supported in OSC version 1.0 and version 1.1
STDMETHODIMP COSCProfile::GetStatus(BSTR * status)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Get an XML stream of the person's details
STDMETHODIMP COSCProfile::GetDetails(BSTR * details)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Not supported in OSC version 1.0 and version 1.1
STDMETHODIMP COSCProfile::AreFriendsOrColleagues(SAFEARRAY * userIDs, SAFEARRAY * * results)
{
	return OSC_E_NOT_IMPLEMENTED;
}


// Get an XML stream for activities of friends/colleagues
STDMETHODIMP COSCProfile::GetActivitiesOfFriendsAndColleagues(DATE startTime, BSTR * activitiesCollection)
{
	return OSC_E_NOT_IMPLEMENTED;
}

// Not supported in OSC version 1.0 and version 1.1
STDMETHODIMP COSCProfile::SetStatus(BSTR status)
{
	return OSC_E_NOT_IMPLEMENTED;
}
