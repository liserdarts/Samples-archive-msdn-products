// MyProviderDS.h : Declaration of the CMyProviderSource

#pragma once

#include "resource.h"       // main symbols
#include "MyProviderRS.h"
#include "MyProviderSess.h"

#include <initguid.h>
#include "msmd.h"

// CMyProviderSource

class ATL_NO_VTABLE CMyProviderSource : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CMyProviderSource, &CLSID_MyProvider>,
	public IDBCreateSessionImpl<CMyProviderSource, CMyProviderSession>,
	public IDBInitializeImpl<CMyProviderSource>,
	public IDBPropertiesImpl<CMyProviderSource>,
	//public IPersistImpl<CMyProviderSource>,
	public IInternalConnectionImpl<CMyProviderSource>,
    public IMDEmbeddedData
{
public:
	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return FInit();
	}
	
	void FinalRelease() 
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_MYPROVIDER)
BEGIN_COM_MAP(CMyProviderSource)
	COM_INTERFACE_ENTRY(IDBCreateSession)
	COM_INTERFACE_ENTRY(IDBInitialize)
	COM_INTERFACE_ENTRY(IDBProperties)
	COM_INTERFACE_ENTRY(IPersist)
	COM_INTERFACE_ENTRY(IInternalConnection)
    COM_INTERFACE_ENTRY(IMDEmbeddedData)
END_COM_MAP()

BEGIN_PROPSET_MAP(CMyProviderSource)
    BEGIN_PROPERTY_SET(DBPROPSET_MDX_EXTENSIONS)
        PROPERTY_INFO_ENTRY_EX(MSMD_MDX_DDL_EXTENSIONS, VT_I4, 
            DBPROPFLAGS_DBINIT, DBPROPVAL_MDX_DDL_CREATECUBE,
            DBPROPOPTIONS_REQUIRED)
    END_PROPERTY_SET(DBPROPSET_MDX_EXTENSIONS)
    BEGIN_PROPERTY_SET_EX(DBPROPSET_MSOLAPINIT, UPROPSET_USERINIT)
        PROPERTY_INFO_ENTRY_EX(MSMD_EMBEDDED_DATA, VT_I4,
            DBPROPFLAGS_DBINIT, DBPROPVAL_EMBED_EMBEDDED,
            DBPROPOPTIONS_REQUIRED)
    END_PROPERTY_SET(DBPROPSET_MSOLAPINIT)
	BEGIN_PROPERTY_SET(DBPROPSET_DATASOURCEINFO)
		PROPERTY_INFO_ENTRY(ACTIVESESSIONS)
		PROPERTY_INFO_ENTRY(DATASOURCEREADONLY)
		PROPERTY_INFO_ENTRY(BYREFACCESSORS)
		PROPERTY_INFO_ENTRY(OUTPUTPARAMETERAVAILABILITY)
		PROPERTY_INFO_ENTRY(PROVIDEROLEDBVER)
		PROPERTY_INFO_ENTRY(DSOTHREADMODEL)
		PROPERTY_INFO_ENTRY(SUPPORTEDTXNISOLEVELS)
		PROPERTY_INFO_ENTRY(USERNAME)
	END_PROPERTY_SET(DBPROPSET_DATASOURCEINFO)
	BEGIN_PROPERTY_SET(DBPROPSET_DBINIT)
        PROPERTY_INFO_ENTRY(INIT_CATALOG)
        PROPERTY_INFO_ENTRY(AUTH_PASSWORD)
		PROPERTY_INFO_ENTRY(AUTH_PERSIST_SENSITIVE_AUTHINFO)
		PROPERTY_INFO_ENTRY(AUTH_USERID)
		PROPERTY_INFO_ENTRY(INIT_DATASOURCE)
		PROPERTY_INFO_ENTRY(INIT_HWND)
		PROPERTY_INFO_ENTRY(INIT_LCID)
		PROPERTY_INFO_ENTRY(INIT_LOCATION)
		PROPERTY_INFO_ENTRY(INIT_MODE)
		PROPERTY_INFO_ENTRY(INIT_PROMPT)
		PROPERTY_INFO_ENTRY(INIT_PROVIDERSTRING)
		PROPERTY_INFO_ENTRY(INIT_TIMEOUT)
	END_PROPERTY_SET(DBPROPSET_DBINIT)
	CHAIN_PROPERTY_SET(CMyProviderSession)
	CHAIN_PROPERTY_SET(CMyProviderCommand)
END_PROPSET_MAP()

    STDMETHOD(SetHosted)(BOOL in_fIsHosted)
    {
        HRESULT hr;
        hr = S_OK;
        return hr;
    }

    STDMETHOD(SetContainerURL)(BSTR in_bstrURL)
    {
        HRESULT hr;
        hr = S_OK;
        return hr;
    }

    STDMETHOD(GetStreamIdentifier)(BSTR *out_pbstrStreamId)
    {
        HRESULT hr;
        VARIANT vValue;

        hr = S_OK;

        // Check connection string first
        VariantInit(&vValue);        
        hr = this->GetPropValue(&DBPROPSET_DBINIT, DBPROP_INIT_CATALOG, &vValue);

        if (SUCCEEDED(hr))
        {
            if (!vValue.bstrVal || !wcscmp(vValue.bstrVal, OLESTR("(Default)")))
            {
                *out_pbstrStreamId = SysAllocString(OLESTR("MyProvider"));
                vValue.vt = VT_BSTR;
                vValue.bstrVal = *out_pbstrStreamId;
                this->SetPropValue(&DBPROPSET_DBINIT, DBPROP_INIT_CATALOG, &vValue);
            }
            else  // Excel is asking us if the stream is ours
            {
                int pos = wcscmp(vValue.bstrVal, OLESTR("TEST CDP"));
                if (pos != 0)
                {
                    return S_FALSE;
                }
            }
        }
        return hr;
    }

    STDMETHOD(SetTempDirPath)(BSTR in_bstrPath)
    {
        HRESULT hr;
        hr = S_OK;
        return hr;
    }

    STDMETHOD(IsDirty)()
    {
        HRESULT hr;

        // S_FALSE indicates not dirty
        // S_OK indicates dirty
        hr = S_OK;

        return hr;
    }

    STDMETHOD(Cancel)(void)
    {
        HRESULT hr;

        // perform necessary cleanup work here
        // return S_OK if cleanup succeeded
        // return E_FAIL if not
        hr = S_OK;

        return hr;
    }

    STDMETHOD(Load)(IStream *pStm)
    {
        HRESULT hr = S_OK;

        // Load data using from pStm

        return hr;
    }

    STDMETHOD(Save)(IStream *pStm, BOOL fClearDirty)
    {
        wchar_t* pwszText = NULL; 
        ULONG uWritten = 0, uLen;
        HRESULT hr = S_OK;

        pwszText = L"Hello";
        uLen = (ULONG)wcslen(pwszText);
        hr = pStm->Write((void*)pwszText, (ULONG)(sizeof(wchar_t) * uLen), &uWritten);
        //free(pwszText);

        if (fClearDirty)
        {
            // clear the dirty flag
        }

        return hr;
    }

    STDMETHOD(GetClassID)(CLSID *pClassID)
    {
        if (pClassID == NULL)
            return E_FAIL;
        *pClassID = GetObjectCLSID();
        return S_OK;
    }

    STDMETHOD(GetSizeMax)(ULARGE_INTEGER *pcbSize)
    {
        // set pcbSize to size needed to save the data, in bytes
        return S_OK;
    }

public:
};

OBJECT_ENTRY_AUTO(__uuidof(MyProvider), CMyProviderSource)
