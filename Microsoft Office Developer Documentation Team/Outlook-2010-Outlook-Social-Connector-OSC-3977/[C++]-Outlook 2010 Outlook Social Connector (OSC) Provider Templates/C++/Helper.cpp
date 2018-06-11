// Helper.cpp : Helper functions

#include "stdafx.h"

extern HINSTANCE g_hInstance;

HRESULT GetPicture(SAFEARRAY * * picture)
{
	HRSRC          hRsrc = NULL;
	HGLOBAL        hGlobal = NULL;

	LPVOID         pResourceData = NULL;
	DWORD          dwSizeInBytes = 0;

	SAFEARRAYBOUND sab;
	SAFEARRAY      *psa = NULL;
	void           *pArrayData = NULL;

	//
	// Load the picture resource
	//
	if (!g_hInstance)
		return E_FAIL;

	hRsrc = FindResource(g_hInstance, _T("OSC.png"), _T("PNG"));
	if (!hRsrc)
		return E_FAIL;

	hGlobal = LoadResource(g_hInstance, hRsrc);
	if (!hGlobal)
		return E_FAIL;

	dwSizeInBytes = SizeofResource(g_hInstance, hRsrc);
	pResourceData = LockResource(hGlobal);

	//
	// Create an OLE SAFEARRAY
	//
	sab.lLbound = 0;
	sab.cElements = dwSizeInBytes;

	psa = SafeArrayCreate(VT_UI1,1,&sab);

	if (psa == NULL)
		return OSC_E_OUT_OF_MEMORY;

	// Get a safe pointer to the array
	if (FAILED(SafeArrayAccessData(psa, &pArrayData)))
		return E_FAIL;

	// Copy the data from the resource to the array
	memcpy(pArrayData, pResourceData, dwSizeInBytes);

	SafeArrayUnaccessData(psa);

	*picture = psa;

	return S_OK;
}