#include "stdafx.h"
#include "atlbase.h"
#include "atlconv.h"
#include "ChunkValue.h"
#include <string>

CChunkValue::CChunkValue() : m_fIsValid(false), m_pszValue(NULL)
{
	PropVariantInit(&m_propVariant);
    Clear();
}

CChunkValue::~CChunkValue()
{
    Clear();
};

// clear the ChunkValue
void CChunkValue::Clear()
{
    m_fIsValid = false;
    ZeroMemory(&m_chunk, sizeof(m_chunk));
    PropVariantClear(&m_propVariant);
    CoTaskMemFree(m_pszValue);
    m_pszValue = NULL;
}

// Is this propvalue valid
BOOL CChunkValue::IsValid()
{
    return m_fIsValid;
}


// get the value as an allocated PROPVARIANT
HRESULT CChunkValue::GetValue(PROPVARIANT **ppPropVariant)
{
    HRESULT hr = S_OK;
    if (ppPropVariant == NULL)
    {
        return E_INVALIDARG;
    }

    *ppPropVariant = NULL;

    PROPVARIANT *pPropVariant = static_cast<PROPVARIANT*>(CoTaskMemAlloc(sizeof(PROPVARIANT)));

    if (pPropVariant)
    {
        hr = PropVariantCopy(pPropVariant, &m_propVariant);
        if (SUCCEEDED(hr))
        {
            // detach and return this as the value
            *ppPropVariant = pPropVariant;
        }
        else
        {
            CoTaskMemFree(pPropVariant);
        }
    }
    else
    {
        hr = E_OUTOFMEMORY;
    }

    return hr;
}

// get the string value
PWSTR CChunkValue::GetString()
{
    return m_pszValue;
};

// copy the chunk
HRESULT CChunkValue::CopyChunk(STAT_CHUNK *pStatChunk)
{
    if (pStatChunk == NULL)
    {
        return E_INVALIDARG;
    }

    *pStatChunk = m_chunk;
    return S_OK;
}

// get the type of chunk
CHUNKSTATE CChunkValue::GetChunkType()
{
    return m_chunk.flags;
}

// set the property by key to a unicode string
HRESULT CChunkValue::SetTextValue(REFPROPERTYKEY pkey, PCWSTR pszValue, CHUNKSTATE chunkType,
                     LCID locale, DWORD cwcLenSource, DWORD cwcStartSource,
                     CHUNK_BREAKTYPE chunkBreakType)
{
    if (pszValue == NULL)
    {
        return E_INVALIDARG;
    }


    HRESULT hr = SetChunk(pkey, chunkType, locale, cwcLenSource, cwcStartSource, chunkBreakType);
    if (SUCCEEDED(hr))
    {
        size_t cch = wcslen(pszValue) + 1;
        PWSTR pszCoTaskValue = static_cast<PWSTR>(CoTaskMemAlloc(cch * sizeof(WCHAR)));
        if (pszCoTaskValue)
        {
			StringCchCopyW(pszCoTaskValue, cch, pszValue);
            //StringCchCopy(pszCoTaskValue, cch, pszValue);
            m_fIsValid = true;
            if (chunkType == CHUNK_VALUE)
            {
                m_propVariant.vt = VT_LPWSTR;
                m_propVariant.pwszVal = pszCoTaskValue;
            }
            else
            {
                m_pszValue = pszCoTaskValue;
            }
        }
        else
        {
            hr = E_OUTOFMEMORY;
        }
    }
    return hr;
};

// set the property by key to a bool
HRESULT CChunkValue::SetBoolValue(REFPROPERTYKEY pkey, BOOL bVal, CHUNKSTATE chunkType, LCID locale,
                     DWORD cwcLenSource, DWORD cwcStartSource, CHUNK_BREAKTYPE chunkBreakType)
{
    return SetBoolValue(pkey, bVal ? VARIANT_TRUE : VARIANT_FALSE, chunkType, locale, cwcLenSource, cwcStartSource, chunkBreakType);
};

// set the property by key to a variant bool
HRESULT CChunkValue::SetBoolValue(REFPROPERTYKEY pkey, VARIANT_BOOL bVal, CHUNKSTATE chunkType, LCID locale,
                     DWORD cwcLenSource, DWORD cwcStartSource, CHUNK_BREAKTYPE chunkBreakType)
{
    HRESULT hr = SetChunk(pkey, chunkType, locale, cwcLenSource, cwcStartSource, chunkBreakType);
    if (SUCCEEDED(hr))
    {
        m_propVariant.vt = VT_BOOL;
        m_propVariant.boolVal = bVal;
        m_fIsValid = true;
    }
    return hr;
};

// set the property by key to an int
HRESULT CChunkValue::SetIntValue(REFPROPERTYKEY pkey, int nVal, CHUNKSTATE chunkType,
                    LCID locale, DWORD cwcLenSource, DWORD cwcStartSource,
                    CHUNK_BREAKTYPE chunkBreakType)
{
    HRESULT hr = SetChunk(pkey, chunkType, locale, cwcLenSource, cwcStartSource, chunkBreakType);
    if (SUCCEEDED(hr))
    {
        m_propVariant.vt = VT_I4;
        m_propVariant.lVal = nVal;
        m_fIsValid = true;
    }
    return hr;
};

// set the property by key to a long
HRESULT CChunkValue::SetLongValue(REFPROPERTYKEY pkey, long lVal, CHUNKSTATE chunkType, LCID locale,
                     DWORD cwcLenSource, DWORD cwcStartSource, CHUNK_BREAKTYPE chunkBreakType)
{
    HRESULT hr = SetChunk(pkey, chunkType, locale, cwcLenSource, cwcStartSource, chunkBreakType);
    if (SUCCEEDED(hr))
    {
        m_propVariant.vt = VT_I4;
        m_propVariant.lVal = lVal;
        m_fIsValid = true;
    }
    return hr;
};

// set the property by key to a dword
HRESULT CChunkValue::SetDwordValue(REFPROPERTYKEY pkey, DWORD dwVal, CHUNKSTATE chunkType, LCID locale,
                      DWORD cwcLenSource, DWORD cwcStartSource, CHUNK_BREAKTYPE chunkBreakType)
{
    HRESULT hr = SetChunk(pkey, chunkType, locale, cwcLenSource, cwcStartSource, chunkBreakType);
    if (SUCCEEDED(hr))
    {
        m_propVariant.vt = VT_UI4;
        m_propVariant.ulVal = dwVal;
        m_fIsValid = true;
    }
    return hr;
};

// set property by key to an int64
HRESULT CChunkValue::SetInt64Value(REFPROPERTYKEY pkey, __int64 nVal, CHUNKSTATE chunkType, LCID locale,
                      DWORD cwcLenSource, DWORD cwcStartSource, CHUNK_BREAKTYPE chunkBreakType)
{
    HRESULT hr = SetChunk(pkey, chunkType, locale, cwcLenSource, cwcStartSource, chunkBreakType);
    if (SUCCEEDED(hr))
    {
        m_propVariant.vt = VT_I8;
        m_propVariant.hVal.QuadPart = nVal;
        m_fIsValid = true;
    }
    return hr;
};


// set Property by key to a filetime
HRESULT CChunkValue::SetFileTimeValue(REFPROPERTYKEY pkey, FILETIME dtVal, CHUNKSTATE chunkType,
                         LCID locale, DWORD cwcLenSource, DWORD cwcStartSource,
                         CHUNK_BREAKTYPE chunkBreakType)
{
    HRESULT hr = SetChunk(pkey, chunkType, locale, cwcLenSource, cwcStartSource, chunkBreakType);
    if (SUCCEEDED(hr))
    {
        m_propVariant.vt = VT_FILETIME;
        m_propVariant.filetime = dtVal;
        m_fIsValid = true;
    }
    return hr;
};



// Initialize the STAT_CHUNK
inline HRESULT CChunkValue::SetChunk(REFPROPERTYKEY pkey,
                                     CHUNKSTATE chunkType/*=CHUNK_VALUE*/,
                                     LCID locale /*=0*/,
                                     DWORD cwcLenSource /*=0*/,
                                     DWORD cwcStartSource /*=0*/,
                                     CHUNK_BREAKTYPE chunkBreakType /*= CHUNK_NO_BREAK */)
{
    Clear();

    // initialize the chunk
    m_chunk.attribute.psProperty.ulKind = PRSPEC_PROPID;
    m_chunk.attribute.psProperty.propid = pkey.pid;
    m_chunk.attribute.guidPropSet = pkey.fmtid;
    m_chunk.flags = chunkType;
    m_chunk.locale = locale;
    m_chunk.cwcLenSource = cwcLenSource;
    m_chunk.cwcStartSource = cwcStartSource;
    m_chunk.breakType = chunkBreakType;

    return S_OK;
}