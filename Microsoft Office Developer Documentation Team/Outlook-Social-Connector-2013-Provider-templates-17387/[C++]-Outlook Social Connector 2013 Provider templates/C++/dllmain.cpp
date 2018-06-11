// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "OSCProvider_CPP_i.h"
#include "dllmain.h"

COSCProvider_CPPModule _AtlModule;
HINSTANCE g_hInstance = NULL;

// DLL Entry Point
extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	g_hInstance = hInstance;
	return _AtlModule.DllMain(dwReason, lpReserved); 
}
