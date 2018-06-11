#pragma once

#include <strsafe.h>
#include <shlwapi.h>
#include <propkey.h>
#include <propsys.h>
#include <filter.h>
#include <filterr.h>

// This is a class which simplifies both chunk and property value pair logic
// To use, you simply create a ChunkValue class of the right kind
// Example:
//      CChunkValue chunk;
//      hr = chunk.SetBoolValue(PKEY_IsAttachment, true);
//      or
//      hr = chunk.SetFileTimeValue(PKEY_ItemDate, ftLastModified);
class CChunkValue
{
public:
    CChunkValue();

    ~CChunkValue();

    // clear the ChunkValue
    void Clear();

    // Is this propvalue valid
    BOOL IsValid();


    // get the value as an allocated PROPVARIANT
    HRESULT GetValue(PROPVARIANT **ppPropVariant);

    // get the string value
    PWSTR GetString();

    // copy the chunk
    HRESULT CopyChunk(STAT_CHUNK *pStatChunk);

    // get the type of chunk
    CHUNKSTATE GetChunkType();

    // set the property by key to a unicode string
    HRESULT SetTextValue(REFPROPERTYKEY pkey, PCWSTR pszValue, CHUNKSTATE chunkType = CHUNK_VALUE,
                         LCID locale = 0, DWORD cwcLenSource = 0, DWORD cwcStartSource = 0,
                         CHUNK_BREAKTYPE chunkBreakType = CHUNK_NO_BREAK);

    // set the property by key to a bool
    HRESULT SetBoolValue(REFPROPERTYKEY pkey, BOOL bVal, CHUNKSTATE chunkType = CHUNK_VALUE, LCID locale = 0,
                         DWORD cwcLenSource = 0, DWORD cwcStartSource = 0, CHUNK_BREAKTYPE chunkBreakType = CHUNK_NO_BREAK);

    // set the property by key to a variant bool
    HRESULT SetBoolValue(REFPROPERTYKEY pkey, VARIANT_BOOL bVal, CHUNKSTATE chunkType = CHUNK_VALUE, LCID locale = 0,
                         DWORD cwcLenSource = 0, DWORD cwcStartSource = 0, CHUNK_BREAKTYPE chunkBreakType = CHUNK_NO_BREAK);

    // set the property by key to an int
    HRESULT SetIntValue(REFPROPERTYKEY pkey, int nVal, CHUNKSTATE chunkType = CHUNK_VALUE,
                        LCID locale = 0, DWORD cwcLenSource = 0, DWORD cwcStartSource = 0,
                        CHUNK_BREAKTYPE chunkBreakType = CHUNK_NO_BREAK);

    // set the property by key to a long
    HRESULT SetLongValue(REFPROPERTYKEY pkey, long lVal, CHUNKSTATE chunkType = CHUNK_VALUE, LCID locale = 0,
                         DWORD cwcLenSource = 0, DWORD cwcStartSource = 0, CHUNK_BREAKTYPE chunkBreakType = CHUNK_NO_BREAK);

    // set the property by key to a dword
    HRESULT SetDwordValue(REFPROPERTYKEY pkey, DWORD dwVal, CHUNKSTATE chunkType = CHUNK_VALUE, LCID locale = 0,
                          DWORD cwcLenSource = 0, DWORD cwcStartSource = 0, CHUNK_BREAKTYPE chunkBreakType = CHUNK_NO_BREAK);

    // set property by key to an int64
    HRESULT SetInt64Value(REFPROPERTYKEY pkey, __int64 nVal, CHUNKSTATE chunkType = CHUNK_VALUE, LCID locale = 0,
                          DWORD cwcLenSource = 0, DWORD cwcStartSource = 0, CHUNK_BREAKTYPE chunkBreakType = CHUNK_NO_BREAK);

    // set Property by key to a filetime
    HRESULT SetFileTimeValue(REFPROPERTYKEY pkey, FILETIME dtVal, CHUNKSTATE chunkType = CHUNK_VALUE,
                             LCID locale = 0, DWORD cwcLenSource = 0, DWORD cwcStartSource = 0,
                             CHUNK_BREAKTYPE chunkBreakType = CHUNK_NO_BREAK);

protected:
    // set the locale for this chunk
    HRESULT SetChunk(REFPROPERTYKEY pkey, CHUNKSTATE chunkType=CHUNK_VALUE, LCID locale=0, DWORD cwcLenSource=0, DWORD cwcStartSource=0, CHUNK_BREAKTYPE chunkBreakType=CHUNK_NO_BREAK);

    // member variables
private:
    bool m_fIsValid;
    STAT_CHUNK  m_chunk;
    PROPVARIANT m_propVariant;
    PWSTR m_pszValue;

};

