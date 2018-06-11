// MyIFilter.h : Declaration of the CMyIFilter

#pragma once
#include "resource.h"       // main symbols
#include "Filter.h"
#include "ObjIdl.h"
#include "Initguid.h"
#include "ChunkValue.h"


#include "MyIFilter_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CMyIFilter

class ATL_NO_VTABLE CMyIFilter :
	public CComObjectRootEx<CComMultiThreadModel>,
	public CComCoClass<CMyIFilter, &CLSID_MyIFilter>,
	public IDispatchImpl<IMyIFilter, &IID_IMyIFilter, &LIBID_MyIFilterLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IFilter,
	public IPersistStream,
	public IPersistFile
{
public:
	CMyIFilter();

DECLARE_REGISTRY_RESOURCEID(IDR_MYIFILTER1)


BEGIN_COM_MAP(CMyIFilter)
	COM_INTERFACE_ENTRY(IMyIFilter)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IFilter)
	COM_INTERFACE_ENTRY(IPersistStream)
	COM_INTERFACE_ENTRY(IPersistFile)
END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

	virtual  SCODE STDMETHODCALLTYPE  Init( ULONG grfFlags,
                                            ULONG cAttributes,
                                            FULLPROPSPEC const * aAttributes,
                                            ULONG * pFlags );

    virtual  SCODE STDMETHODCALLTYPE  GetChunk( STAT_CHUNK * pStat);

    virtual  SCODE STDMETHODCALLTYPE  GetText( ULONG * pcwcBuffer,
                                               WCHAR * awcBuffer );

    virtual  SCODE STDMETHODCALLTYPE  GetValue( PROPVARIANT * * ppPropValue );

    virtual  SCODE STDMETHODCALLTYPE  BindRegion( FILTERREGION origPos,
                                                  REFIID riid,
                                                  void ** ppunk);

    virtual  SCODE STDMETHODCALLTYPE  GetClassID( CLSID * pClassID );

    virtual  SCODE STDMETHODCALLTYPE  IsDirty();

    virtual  SCODE STDMETHODCALLTYPE  Load( LPCWSTR pszFileName,
                                            DWORD dwMode);

    virtual  SCODE STDMETHODCALLTYPE  Save( LPCWSTR pszFileName,
                                            BOOL fRemember );

    virtual  SCODE STDMETHODCALLTYPE  SaveCompleted( LPCWSTR pszFileName );

    virtual  SCODE STDMETHODCALLTYPE  GetCurFile( LPWSTR  * ppszFileName );
    
    virtual HRESULT STDMETHODCALLTYPE Load( 
        /* [unique][in] */ __RPC__in_opt IStream *pStm);
    
    virtual HRESULT STDMETHODCALLTYPE Save( 
        /* [unique][in] */ __RPC__in_opt IStream *pStm,
        /* [in] */ BOOL fClearDirty);
    
    virtual HRESULT STDMETHODCALLTYPE GetSizeMax( 
        /* [out] */ __RPC__out ULARGE_INTEGER *pcbSize);

	virtual HRESULT GetNextChunkValue(CChunkValue &chunkValue);


private:
    IStream*                    m_pStream;         // stream of this document
    long _uRefs;
    DWORD                       m_dwChunkId;        // Current chunk id
    DWORD                       m_iText;            // index into ChunkValue
    CChunkValue                 m_currentChunk;     // the current chunk value
    void HandleError(LPCTSTR message, HRESULT hr);
	CString ReadLine();								// utility function to read a line from the text file

};

OBJECT_ENTRY_AUTO(__uuidof(MyIFilter), CMyIFilter)
