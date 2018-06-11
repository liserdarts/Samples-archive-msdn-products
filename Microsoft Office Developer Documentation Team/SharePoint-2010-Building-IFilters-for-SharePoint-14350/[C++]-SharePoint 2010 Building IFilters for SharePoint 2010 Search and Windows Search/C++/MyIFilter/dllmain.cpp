// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "MyIFilter_i.h"
#include "dllmain.h"

CMyIFilterModule _AtlModule;

class CMyIFilterApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CMyIFilterApp, CWinApp)
END_MESSAGE_MAP()

CMyIFilterApp theApp;

BOOL CMyIFilterApp::InitInstance()
{
	return CWinApp::InitInstance();
}

int CMyIFilterApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
