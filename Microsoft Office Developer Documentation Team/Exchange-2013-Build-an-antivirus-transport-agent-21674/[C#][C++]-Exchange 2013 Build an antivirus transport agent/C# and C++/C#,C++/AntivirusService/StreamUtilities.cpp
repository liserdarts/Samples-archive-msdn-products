// ***************************************************************
// <copyright file="StreamUtilities.cpp" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// Implementations for simple stream types
// </summary>
// ***************************************************************

#include "stdafx.h"
#include "StreamUtilities.h"

/////////////////////////////////////////////////////////////////////////////
// CReadOnlyMemoryStream

STDMETHODIMP CReadOnlyMemoryStream::Read(LPVOID pv, ULONG cb, ULONG *pcbRead)
{
    ULONG cbRead = 0;
    
    ATLASSERT(pv && m_iData <= m_cbData);
    
    if (m_cbData > 0)
    {
        cbRead = min(cb - cbRead, m_cbData - m_iData);        
        CopyMemory( (LPBYTE)pv, m_pbData + m_iData, cbRead);        
        m_iData += cbRead;
    }
    
    if (pcbRead)
    {
        *pcbRead = cbRead;
    }
    
    return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
STDMETHODIMP CReadOnlyMemoryStream::Seek(LARGE_INTEGER dlibMove, DWORD dwOrigin, ULARGE_INTEGER *plibNew)
{
    LONG            lMove;
    ULONG           iNew;
    
    lMove = (LONG) dlibMove.QuadPart;

    ATLASSERT( (LONGLONG)lMove == dlibMove.QuadPart );
    
    switch (dwOrigin)
    {
    case STREAM_SEEK_SET:
        if (lMove < 0)
		{
            return E_FAIL;
		}
        iNew = lMove;
        break;
        
    case STREAM_SEEK_CUR:
        if (lMove < 0 && (ULONG)(abs(lMove)) > m_iData)
		{
            return E_FAIL;
		}
        iNew = m_iData + lMove;
        break;
        
    case STREAM_SEEK_END:
        if (lMove < 0 || (ULONG)lMove > m_cbData)
		{
            return E_FAIL;
		}
        iNew = m_cbData - lMove;
        break;
        
    default:
        return STG_E_INVALIDFUNCTION;
    }
    
    m_iData = min(iNew, m_cbData);
    
    if (plibNew)
	{
        plibNew->QuadPart = (LONGLONG) m_iData;
	}
    
    return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
STDMETHODIMP CReadOnlyMemoryStream::Stat(STATSTG *pStat, DWORD)
{
    ATLASSERT(pStat);
    
    ZeroMemory(pStat, sizeof(STATSTG));
    pStat->type = STGTY_STREAM;
    pStat->cbSize.QuadPart = m_cbData;

    return S_OK;
}

