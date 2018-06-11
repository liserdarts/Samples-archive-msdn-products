// ***************************************************************
// <copyright file="StreamUtilities.h" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// Declarations for simple stream types
// </summary>
// ***************************************************************

/////////////////////////////////////////////////////////////////////////////
//
// This is an empty stream implementation, all methods return E_UNEXPECTED. To be used 
// as a base class to avoid redefining unsupported methods.
//
class ATL_NO_VTABLE CBaseStream : public CComObjectRootEx<CComSingleThreadModel>, public IStream
{
public:

    CBaseStream()
    {
    }

    DECLARE_NOT_AGGREGATABLE(CBaseStream)

    BEGIN_COM_MAP(CBaseStream)
        COM_INTERFACE_ENTRY(IStream)                // .
        COM_INTERFACE_ENTRY(ISequentialStream)
    END_COM_MAP()                                   // .

// ISequencialStream overrides
public:

    STDMETHOD( Read )( void * pv, ULONG cb, ULONG * pcbRead )
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

    STDMETHOD( Write )( void const* pv, ULONG cb, ULONG * pcbWritten )
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

// IStream overrides
public:

    STDMETHOD(Seek)( LARGE_INTEGER dlibMove, DWORD dwOrigin, ULARGE_INTEGER *plibNewPosition )
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

    STDMETHOD(SetSize)( ULARGE_INTEGER libNewSize )
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

    STDMETHOD(CopyTo)( IStream *pstm, ULARGE_INTEGER cb, ULARGE_INTEGER *pcbRead, ULARGE_INTEGER *pcbWritten)
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

    STDMETHOD(Commit)( DWORD grfCommitFlags )
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

    STDMETHOD(Revert)()
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

    STDMETHOD(LockRegion)( ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, DWORD dwLockType )
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

    STDMETHOD(UnlockRegion)(ULARGE_INTEGER libOffset,ULARGE_INTEGER cb,DWORD dwLockType )
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

    STDMETHOD(Stat)( STATSTG *pstatstg, DWORD grfStatFlag )
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

    STDMETHOD(Clone)(IStream **ppstm)
    {
        ATLASSERT( !"unexpected stream use" );
        return E_UNEXPECTED;
    }

protected:

    ~CBaseStream()
    {
    }
};



////////////////////////////////////////////////////////////////////////////////////
class ATL_NO_VTABLE CReadOnlyMemoryStream : public CBaseStream
{
public:

    CReadOnlyMemoryStream() :
        CBaseStream(),
        m_cbData(0),
        m_iData(0),
        m_pbData(NULL)
    {
    }

    ~CReadOnlyMemoryStream()
    {
    }

// IStream overrides
public:
    STDMETHODIMP Read( LPVOID, ULONG, ULONG * );
    STDMETHODIMP Seek( LARGE_INTEGER, DWORD, ULARGE_INTEGER * );
    STDMETHODIMP Stat( STATSTG *, DWORD );

public:

    void    Init( void * pvData, ULONG cbData )
    {
        m_pbData = (BYTE*) pvData;
        m_cbData = cbData;
    }

private:

    ULONG           m_cbData;              // Number of valid bytes in m_pbData
    ULONG           m_iData;               // Current data index
    BYTE *          m_pbData;              // Pointer to data buffer
};

////////////////////////////////////////////////////////////////////////////////////
// stream that owns the underlying RgBytes (array of bytes allocated with new[]).

class ATL_NO_VTABLE CReadOnlyRgBytesStream : public CReadOnlyMemoryStream
{
public:

    CReadOnlyRgBytesStream() : CReadOnlyMemoryStream(), m_rg(NULL) {}

    void    Init( const BYTE * rgBytes, ULONG cbBytes )
    {
        // NOTE: Init transfers the ownership of passed-in string to the stream object.

        CReadOnlyMemoryStream::Init( (void*) rgBytes, cbBytes );

        m_rg = rgBytes;
    }

protected:

    ~CReadOnlyRgBytesStream()
    { 
        delete m_rg; 
    }

    const BYTE *    m_rg;
};
