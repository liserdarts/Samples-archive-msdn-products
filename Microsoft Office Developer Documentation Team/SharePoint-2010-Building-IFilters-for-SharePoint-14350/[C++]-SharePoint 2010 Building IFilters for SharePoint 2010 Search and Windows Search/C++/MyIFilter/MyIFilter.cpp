// MyIFilter.cpp : Implementation of DLL Exports.


#include "stdafx.h"
#include "resource.h"
#include "MyIFilter_i.h"
#include "dllmain.h"
#include "Filter.h"
#include "ChunkValue.h"
#include "ObjIdl.h"
#include "Initguid.h"
#include "XEventLog.h"
#include "CrawledProperties.h"
#include "MyIFilter.h"


// Used to determine whether the DLL can be unloaded by OLE.
STDAPI DllCanUnloadNow(void)
{
			AFX_MANAGE_STATE(AfxGetStaticModuleState());
	return (AfxDllCanUnloadNow()==S_OK && _AtlModule.GetLockCount()==0) ? S_OK : S_FALSE;
	}

// Returns a class factory to create an object of the requested type.
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
		return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}

// DllRegisterServer - Adds entries to the system registry.
STDAPI DllRegisterServer(void)
{
	// registers object, typelib and all interfaces in typelib
	HRESULT hr = _AtlModule.DllRegisterServer();
		return hr;
}

// DllUnregisterServer - Removes entries from the system registry.
STDAPI DllUnregisterServer(void)
{
	HRESULT hr = _AtlModule.DllUnregisterServer();
		return hr;
}

// DllInstall - Adds/Removes entries to the system registry per user per machine.
STDAPI DllInstall(BOOL bInstall, LPCWSTR pszCmdLine)
{
	HRESULT hr = E_FAIL;
	static const wchar_t szUserSwitch[] = L"user";

	if (pszCmdLine != NULL)
	{
		if (_wcsnicmp(pszCmdLine, szUserSwitch, _countof(szUserSwitch)) == 0)
		{
			ATL::AtlSetPerUserRegistration(true);
		}
	}

	if (bInstall)
	{	
		hr = DllRegisterServer();
		if (FAILED(hr))
		{
			DllUnregisterServer();
		}
	}
	else
	{
		hr = DllUnregisterServer();
	}

	return hr;
}


// MyIFilter.cpp : Implementation of CMyIFilter




// Not implemented interface methods
SCODE STDMETHODCALLTYPE CMyIFilter::BindRegion( FILTERREGION origPos,
  REFIID riid,
  void ** ppunk)
{ 
	return E_NOTIMPL;
}


SCODE STDMETHODCALLTYPE CMyIFilter::GetClassID( CLSID * pClassID )
{
	return E_NOTIMPL;
}

SCODE STDMETHODCALLTYPE CMyIFilter::IsDirty()
{
	return E_NOTIMPL;
}

SCODE STDMETHODCALLTYPE CMyIFilter::Save( LPCWSTR pszFileName,
  BOOL fRemember )
{
	return E_NOTIMPL;
}

SCODE STDMETHODCALLTYPE CMyIFilter::SaveCompleted( LPCWSTR pszFileName )
{
	return E_NOTIMPL;
}

SCODE STDMETHODCALLTYPE CMyIFilter::GetCurFile( LPWSTR  * ppszFileName )
{ 
	return E_NOTIMPL;
}

HRESULT STDMETHODCALLTYPE CMyIFilter::Save(__RPC__in_opt IStream *pStm, BOOL fClearDirty)
{ 
	return E_NOTIMPL;
}

HRESULT STDMETHODCALLTYPE CMyIFilter::GetSizeMax(__RPC__out ULARGE_INTEGER *pcbSize)
{
	return E_NOTIMPL;
}

// End... Not implemented interface methods


CMyIFilter::CMyIFilter() : m_dwChunkId(0), m_iText(0), m_pStream(NULL)
{ 

}

HRESULT STDMETHODCALLTYPE CMyIFilter::Load( __RPC__in_opt IStream *pStm)
{ 
	if (m_pStream)
	{
		m_pStream->Release();
	}
	m_pStream = pStm;
	m_pStream->AddRef();
	return S_OK;
}

SCODE STDMETHODCALLTYPE CMyIFilter::Load( LPCWSTR pszFileName,
  DWORD dwMode)
{ 
	CString msg;
	
	IStream *stream;
	USES_CONVERSION;
	HRESULT hResult = SHCreateStreamOnFile(pszFileName, STGM_READ, &stream);
	if (FAILED(hResult))
	{
		msg.FormatMessage(_T("SHCreateStreamOnFile failed for file %1."),pszFileName);
		HandleError(msg,hResult);
		return hResult;
	}
	//Use the load method for the stream
	return Load(stream); 
}

SCODE STDMETHODCALLTYPE CMyIFilter::GetText( ULONG * pcwcBuffer,
	 WCHAR * awcBuffer )
{ 
	HRESULT hr = S_OK;

	if ((pcwcBuffer == NULL) || (*pcwcBuffer == 0))
	{
		return E_INVALIDARG;
	}

	if (!m_currentChunk.IsValid())
	{
		return FILTER_E_NO_MORE_TEXT;
	}

	if (m_currentChunk.GetChunkType() != CHUNK_TEXT)
	{
		return FILTER_E_NO_TEXT;
	}

	ULONG cchTotal = static_cast<ULONG>(wcslen(m_currentChunk.GetString()));
	ULONG cchLeft = cchTotal - m_iText;
	ULONG cchToCopy = min(*pcwcBuffer - 1, cchLeft);

	if (cchToCopy > 0)
	{
		PCWSTR psz = m_currentChunk.GetString() + m_iText;

		// copy the chars
		StringCchCopyNW(awcBuffer, *pcwcBuffer, psz, cchToCopy);

		// null terminate it
		awcBuffer[cchToCopy] = '\0';

		// set how much data is copied
		*pcwcBuffer = cchToCopy;

		// remember we copied it
		m_iText += cchToCopy;
		cchLeft -= cchToCopy;

		if (cchLeft == 0)
		{
			hr = FILTER_S_LAST_TEXT;
		}
	}
	else
	{
		hr = FILTER_E_NO_MORE_TEXT;
	}

	return hr;

}

SCODE STDMETHODCALLTYPE CMyIFilter::GetChunk( STAT_CHUNK * pStat)
{ 
	HRESULT hr = S_OK;

	// A return of S_FALSE indicates the chunk should be skipped and we should
	// try to get the next chunk.

	int cIterations = 0;
	hr = S_FALSE;

	while ((S_FALSE == hr) && (~cIterations & 0x0020))  // Limit to 256 iterations for safety
	{
		pStat->idChunk = m_dwChunkId;
		m_iText = 0;
		hr = GetNextChunkValue(m_currentChunk);
		++cIterations;
	}

	if (hr == S_OK)
	{
		if (m_currentChunk.IsValid())
		{
			// copy out the STAT_CHUNK
			m_currentChunk.CopyChunk(pStat);

			// and set the id to be the sequential chunk
			pStat->idChunk = ++m_dwChunkId;
		}
		else
		{
			HandleError(_T("Current chunk is invalid"),E_INVALIDARG);
			hr = E_INVALIDARG;
		}
	}

	return hr;
}

SCODE STDMETHODCALLTYPE CMyIFilter::GetValue( PROPVARIANT * * ppPropValue )
{ 
	HRESULT hr = S_OK;

	// if this is not a value chunk they shouldn't be calling this
	if (m_currentChunk.GetChunkType() != CHUNK_VALUE)
	{
		return FILTER_E_NO_MORE_VALUES;
	}

	if (ppPropValue == NULL)
	{
		return E_INVALIDARG;
	}

	if (m_currentChunk.IsValid())
	{
		// return the value of this chunk as a PROPVARIANT ( they own freeing it properly )
		hr = m_currentChunk.GetValue(ppPropValue);
		m_currentChunk.Clear();
	}
	else
	{
		// we have already return the value for this chunk, go away
		hr = FILTER_E_NO_MORE_VALUES;
	}

	return hr;
}

SCODE STDMETHODCALLTYPE CMyIFilter::Init( ULONG grfFlags,
  ULONG cAttributes,
  FULLPROPSPEC const * aAttributes,
  ULONG * pFlags )
{
	//This pointer is not set to any value. If you do not set it to 0
	//the IFilter will not work.
	*pFlags = 0;

	// Common initialization
	m_dwChunkId = 0;
	m_iText = 0;
	m_currentChunk.Clear();
	
	return S_OK;
}

//Reads a line from the buffer
CString CMyIFilter::ReadLine()
{
	BYTE buffer[10001];
	BOOL eof = false;
	ULONG bytesRead;
	HRESULT hr;
	int i;

	for (i=0;!eof && i < 10000;i++)
	{
		hr = m_pStream->Read(buffer + i,1,&bytesRead);
		if (FAILED(hr) || 0 == bytesRead)
		{
			return NULL;
		}
		else if (i > 0 && (char)buffer[i] == '\n')
		{
			eof = TRUE;
		}
	}
	buffer[i] = 0;
	CString ret((char*)buffer);
	return ret;
}

HRESULT CMyIFilter::GetNextChunkValue(CChunkValue &chunkValue)
{
	const int MAX_LINES = 128;
	chunkValue.Clear();
	CString line;
	CString propertyValue;
	int ndx = 0;

	
	for (int lines=0;lines < MAX_LINES;lines++)
	{
		line = ReadLine();
		
		if (line.Find(_T("Customer Name:")) >= 0)
		{
			//Found customer, lets save the value
			ndx = line.Find(_T(":"));
			propertyValue = line.Mid(ndx+1,1000); //arbitrary long integer, it will read to end of the line
			propertyValue = propertyValue.Trim();
			chunkValue.SetTextValue(PKEY_BASIC_200_CUSTOMER_NAME,propertyValue);
			return S_OK;
		}
		if (line.Find(_T("Favorite Sport:")) >= 0)
		{
			//found the favorite sport
			ndx = line.Find(_T(":"));
			propertyValue = line.Mid(ndx+1,1000); //arbitrary long integer, it will read to end of the line
			propertyValue = propertyValue.Trim();
			chunkValue.SetTextValue(PKEY_BASIC_202_FAV_SPORT,propertyValue);
			return S_OK;
		}
		if (line.Find(_T("Height (Inches):")) >= 0)
		{
			//found height
			ndx = line.Find(_T(":"));
			propertyValue = line.Mid(ndx+1,1000); //arbitrary long integer, it will read to end of the line
			propertyValue = propertyValue.Trim();
			int height = _ttoi(propertyValue);
			chunkValue.SetIntValue(PKEY_BASIC_203_HEIGHT,height);
			return S_OK;
		}
		if (line.Find(_T("DOB:")) >= 0)
		{
			//found date of birth
			ndx = line.Find(_T(":"));
			propertyValue = line.Mid(ndx+1,1000); //arbitrary long integer, it will read to end of the line
			propertyValue = propertyValue.Trim();

			//now, convert the date string to a systime
			FILETIME filetime;
			SYSTEMTIME systime;
			int month=0;
			int day=0;
			int year=0;
			ZeroMemory(&systime,sizeof(systime));
			USES_CONVERSION;
			char* aString = T2A(propertyValue.GetBuffer());
			if (sscanf(aString,"%d/%d/%d",&day,&month,&year) == 3)
			{
				systime.wMonth = month;
				systime.wDay = day;
				systime.wYear = year;

				SystemTimeToFileTime(&systime,&filetime);
			}

			//Add the date to the chunk value and return
			chunkValue.SetFileTimeValue(PKEY_BASIC_201_DOB,filetime);
			return S_OK;
		}
		//Didn't find anything interesting, so we must be out of chunks
		return FILTER_E_END_OF_CHUNKS;
	}
}

void CMyIFilter::HandleError(LPCTSTR message, HRESULT hr)
{
	//Change this to whatever app name you want
	const LPCTSTR AppName = _T("My IFilter");

	CString msg;
	msg.FormatMessage(_T("%1 HResult = %2!d!"),message,hr);
	CXEventLog eventLog(AppName);
	eventLog.Write(EVENTLOG_ERROR_TYPE,msg);
}

// CMyIFilter

