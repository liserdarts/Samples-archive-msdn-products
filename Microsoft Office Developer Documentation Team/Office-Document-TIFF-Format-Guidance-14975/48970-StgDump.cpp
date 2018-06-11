
#include <windows.h>
#include <stdio.h>
#include <vector>

using namespace std;

const int cList = 4; // number of Element Types to keep track of
const DWORD dwMaxFaceName = 30; //const Max length of the font face name
typedef ULONG ORIENTATION;  //Orientation angle

//Format of text buffer
const unsigned short gusTextFormat  = 0x0001;   //UTF8

//Seperator
const WCHAR WCH_LINE_SEPERATOR          =       L'\n';	//Line seperator
const WCHAR WCH_WORD_SEPERATOR          =		L' ';	//Word seperator
const WCHAR WCH_REGION_SEPERATOR        =		L'\r';  //Region seperator
const WCHAR WCH_PAGE_SEPERATOR          =		L'\0';  //Region seperator
const WCHAR WCH_PARAMGRAPH_SYMBOL       =       L'\f';
const WCHAR WCH_TAB_SYMBOL              =       L'\t';

//
// ReadString - read a null-termianted string from pstm
//
HRESULT ReadString(IStream *pstm, CHAR *rgc, long cSize)
{
	HRESULT hr;

	while (cSize > 0)
	{
		if (FAILED(hr = pstm->Read(rgc, sizeof(CHAR), NULL /*pcbRead*/)))
			return hr;

		if (*rgc == '\0')
			return S_OK;

		rgc++;
		cSize--;
	}

	return E_ABORT;
}

struct EP_OCR_STATISTICS {
	LONG    lcbSize;              //size of the structure in bytes
	LONG    cCharsRecognized;     //number of characters recognized
	LONG    cCharsRejected;       //number of characters rejected
	LONG    cCharsCorrected;      //number of characters corrected
	LONG    cWordsRecognized;     //number of words recognized
	LONG    cWordsRejected;       //number of words rejected
	LONG    cWordsCorrected;      //number of words corrected
	LONG    cLineRecognized;      //number of lines recognized
	LONG    cParagraphRecognized; //number of paragraphs recognized
	SHORT   sOcrQuality;          //OCR Quality. (range 0-1000)
};

//
// DbgDumpStats - Dump an EP_OCR_STATISTICS struct to the debug output
//
void DbgDumpStats(EP_OCR_STATISTICS *pstat)
{
	char sz[255];

	OutputDebugString("--Begin OCR Statistics--\n");
	sprintf_s(sz, "characters recognized: %u\n", pstat->cCharsRecognized);
	OutputDebugString(sz);

	sprintf_s(sz, "characters rejected: %u\n", pstat->cCharsRejected);
	OutputDebugString(sz);

	sprintf_s(sz, "characters corrected: %u\n", pstat->cCharsCorrected);
	OutputDebugString(sz);

	sprintf_s(sz, "words recognized: %u\n", pstat->cWordsRecognized);
	OutputDebugString(sz);

	sprintf_s(sz, "words rejected: %u\n", pstat->cWordsRejected);
	OutputDebugString(sz);

	sprintf_s(sz, "words corrected: %u\n", pstat->cWordsCorrected);
	OutputDebugString(sz);

	sprintf_s(sz, "lines recognized: %u\n", pstat->cLineRecognized);
	OutputDebugString(sz);

	sprintf_s(sz, "paragrahs recognized: %u\n", pstat->cParagraphRecognized);
	OutputDebugString(sz);

	sprintf_s(sz, "OCR quality (0 - 1000): %u\n", pstat->sOcrQuality);
	OutputDebugString(sz);

	OutputDebugString("--End OCR Statistics--\n");
}

// part of EP_OCR_PROPERTIES
struct XFORM_COM 
{
	float eM11;
	float eM12;
	float eM21;
	float eM22;
	float eDx;
	float eDy;
};

struct EP_OCR_PROPERTIES
{
	LONG                lcbSize;            // size of the structure in bytes
	LCID                lcid;               // locale ID
	wchar_t             wchRejectionSymbol; // 0000 if none
	wchar_t             wchPadding;         // // MODI always writes 0 for this
	RECT                rect;               // Bounding rectangle
	ULONG               orientation;        // The skew angle relative to the edge of the original paper
	ULONG               ulResolutionX;      // (DPI) The number of measurements units in one inch in X direction.
	ULONG               ulResolutionY;      // (DPI) The number of measurements units in one inch in Y direction.
	XFORM_COM           xfTransformation;   // transformation matrix from OCR coordinates to original image
										    //Point in OCR coordinate * xfTransformation = Point in Original Image
};

//
// Dump an EP_OCR_PROPERTIES struct to the debug output
//
void DbgDumpProps(EP_OCR_PROPERTIES *pprops)
{
	char sz[255];

	OutputDebugString("--Begin OCR Properties--\n");

	sprintf_s(sz, "lcid: %u\n", pprops->lcid);
	OutputDebugString(sz);
	sprintf_s(sz, "wchRejectionSymbol: 0x%02x\n", pprops->wchRejectionSymbol);
	OutputDebugString(sz);
	sprintf_s(sz, "wchPadding: 0x%02x\n", pprops->wchPadding);
	OutputDebugString(sz);
	sprintf_s(sz, "Bounding Rectangle, top: %u bottom: %u left: %u right: %u\n", pprops->rect.top, pprops->rect.bottom, pprops->rect.left, pprops->rect.right);
	OutputDebugString(sz);
	sprintf_s(sz, "Skew angle: %u\n", pprops->orientation);
	OutputDebugString(sz);
	sprintf_s(sz, "X resolution: %u\n", pprops->ulResolutionX);
	OutputDebugString(sz);
	sprintf_s(sz, "Y resolution: %u\n", pprops->ulResolutionY);
	OutputDebugString(sz);

	sprintf_s(sz, "Transformation Matrix, eM11: %f, eM12: %f, eM21: %f, eM22: %f, eDx: %f, eDy: %f\n",
	        pprops->xfTransformation.eM11, pprops->xfTransformation.eM12, pprops->xfTransformation.eM21,
	        pprops->xfTransformation.eM22, pprops->xfTransformation.eDx, pprops->xfTransformation.eDy);
	OutputDebugString(sz);

	OutputDebugString("--End OCR Properties--\n");
}

enum MiOcrCharset
{
	EP_OCR_ANSI_CHARSET            = 0,
	EP_OCR_DEFAULT_CHARSET         = 1,
	EP_OCR_SYMBOL_CHARSET         = 2,
	EP_OCR_SHIFTJIS_CHARSET       = 128,
	EP_OCR_HANGEUL_CHARSET        = 129,
	EP_OCR_HANGUL_CHARSET         = 129,
	EP_OCR_GB2312_CHARSET         = 134,
	EP_OCR_CHINESEBIG5_CHARSET    = 136,
	EP_OCR_OEM_CHARSET            = 255
};

enum MiOcrFF
{
	// Font Families
	EP_OCR_FF_DONTCARE         = (0<<4), // Don't care or don't know.
	EP_OCR_FF_ROMAN            = (1<<4), // Variable stroke width, serifed.
	                                     // Times Roman, Century Schoolbook, etc.
	EP_OCR_FF_SWISS            = (2<<4), // Variable stroke width, sans-serifed.
	                                     // Helvetica, Swiss, etc.
	EP_OCR_FF_MODERN           =(3<<4),  // Constant stroke width, serifed or sans-serifed.
	                                     // Pica, Elite, Courier, etc.
	EP_OCR_FF_SCRIPT           =(4<<4),  // Cursive, etc.
	EP_OCR_FF_DECORATIVE       =(5<<4)   // Old English, etc.
};

enum MiOcrPitch
{
	// Font Pitch
	EP_OCR_DEFAULT_PITCH           =0,
	EP_OCR_FIXED_PITCH             =1,
	EP_OCR_VARIABLE_PITCH          =2
};


enum MiOcrFCE
{
	//Font character enhancement
	EP_OCR_BOLD = 1,
	EP_OCR_ITALIC = 2,
	EP_OCR_UNDERLINE = 4,
	EP_OCR_SUBSCRIPT = 8,
	EP_OCR_SUPERSCRIPT = 16
};

struct EP_OCR_FONT
{
	LONG lcbSize; // Size of the structure in bytes
	SHORT lfSize; // Size of the font in points (1/72 inch)
	BYTE lfCharSet; // Specifies the font’s character set. The following constants and values are predefined:
	/*
	EP_OCR_ANSI_CHARSET
	EP_OCR_SYMBOL_CHARSET
	EP_OCR_SHIFTJIS_CHARSET
	EP_OCR_GB2312_CHARSET
	EP_OCR_CHINESEBIG5_CHARSET
	EP_OCR_OEM_CHARSET
	*/
	BYTE lfPitchAndFamily;
	/*
	Specifies the pitch and family of the font. The two low-order bits specify the pitch of the font and can be any one of the following values:

	E_OCR_DEFAULT_PITCH
	E_OCR_VARIABLE_PITCH
	E_OCR_FIXED_PITCH

	The four high-order bits of the parameter specify the font family and can be any one of the following values:

	E_OCR_FF_DECORATIVE   Novelty fonts: Old English, for example.
	E_OCR_FF_DONTCARE   Don’t care or don’t know.
	E_OCR_FF_MODERN   Fonts with constant stroke width (fixed-pitch), with or without serifs. Fixed-pitch fonts are usually modern faces. Pica, Elite, and Courier New are examples.
	E_OCR_FF_ROMAN   Fonts with variable stroke width (proportionally spaced) and with serifs. Times New Roman and Century Schoolbook are examples
	E_OCR_FF_SCRIPT   Fonts designed to look like handwriting. Script and Cursive are examples.
	E_OCR_FF_SWISS   Fonts with variable stroke width (proportionally spaced) and without serifs. MS Sans Serif is an example.
	An application can specify a value for nPitchAndFamily by using the Boolean OR operator to join a pitch constant with a family constant.

	Font families describe the look of a font in a general way. They are intended for specifying fonts when the exact typeface desired is not available.
	*/

	BYTE lfEnhancement; // Bitmask for character enhancement. The following flags can be OR together:
	/*
	EP_OCR_BOLD
	EP_OCR_ITALIC
	EP_OCR_UNDERLINE
	EP_OCR_SUBSCRIPT
	EP_OCR_SUPERSCRIPT
	*/

	CHAR szFaceName[dwMaxFaceName + 1]; //pointer to a char array containing a null-terminated string that 
	                                    //specifies the typeface name of the font. The length of this string must not exceed 30 characters. 
};

//
// LoadFontTable - Load the font table from pstmLayout into pfonts
//
HRESULT LoadFontTable(vector<EP_OCR_FONT> *pfonts, IStream *pstmLayout)
{
	SHORT scFonts;
	HRESULT hr;
	
	if (FAILED(hr = pstmLayout->Read(&scFonts, sizeof(scFonts), NULL /*pcbRead*/)))
		return hr;

	for (int i = 0; i < scFonts; i++)
	{
		EP_OCR_FONT font;

		font.lcbSize = sizeof(font);

		if (FAILED(hr = pstmLayout->Read(&font.lfSize, sizeof(font.lfSize), NULL /*pcbRead*/)))
			return hr;

		if (FAILED(hr = pstmLayout->Read(&font.lfCharSet, sizeof(font.lfCharSet), NULL /*pcbRead*/)))
			return hr;

		if (FAILED(hr = pstmLayout->Read(&font.lfPitchAndFamily, sizeof(font.lfPitchAndFamily), NULL /*pcbRead*/)))
			return hr;

		if (FAILED(hr = pstmLayout->Read(&font.lfEnhancement, sizeof(font.lfEnhancement), NULL /*pcbRead*/)))
			return hr;

		if (FAILED(hr = ReadString(pstmLayout, font.szFaceName, dwMaxFaceName + 1)))
			return hr;

		pfonts->push_back(font);
	}

	return S_OK;
}

//
// DbgDumpFonts - dump pfonts to debug output
//
void DbgDumpFonts(vector<EP_OCR_FONT> *pfonts)
{
	char sz[255];

	OutputDebugString("--Begin Font Properties--\n");

	sprintf_s(sz, "Count: %u\n", pfonts->size());
	OutputDebugString(sz);

	int count = 0;
	for(vector<EP_OCR_FONT>::iterator i=pfonts->begin(); i != pfonts->end(); ++i)
	{
		sprintf_s(sz, "  Font Index: %u\n", count++);
		OutputDebugString(sz);
		sprintf_s(sz, "  Font Size: %u\n", i->lfSize);
		OutputDebugString(sz);
		sprintf_s(sz, "  Character Set: %u\n", i->lfCharSet);
		OutputDebugString(sz);
		sprintf_s(sz, "  Pitch: %u\n", i->lfPitchAndFamily & 0x03);
		OutputDebugString(sz);
		sprintf_s(sz, "  Family: %u\n", i->lfPitchAndFamily & 0xF0);
		OutputDebugString(sz);
		sprintf_s(sz, "  Character Enhancement: 0x%02x\n", i->lfEnhancement);
		OutputDebugString(sz);
		sprintf_s(sz, "  Name: %s\n", i->szFaceName);
		OutputDebugString(sz);
	}

	OutputDebugString("--End Font Properties--\n");
}

enum EP_OCR_ELEMENTTYPE
{
	EP_OCR_ELEMENT_REGION = 0,
	EP_OCR_ELEMENT_LINE,
	EP_OCR_ELEMENT_WORD,
	EP_OCR_ELEMENT_CHAR,
	EP_OCR_ELEMENT_UNKNOWN,
	EP_OCR_ELEMENT_INVALID
};

struct EP_OCR_REGION 
{
	// Region properties
	ORIENTATION ucOrientation; // Angle of orientaion of the region relative to the page
	DWORD flowdir; // Elements flow direction
	DWORD dwType; // Type of the region
};

struct EP_OCR_LINE 
{
	// Line properties
	DWORD  dwReserved; // Reserved field. Must be set to 0
};

struct EP_OCR_WORD
{
	// Word properities
	UCHAR  ubConfidence;     // Confidence in the accuracy of the word in scale of 0-100
};

struct EP_OCR_CHARACTER
{
	// Character properties
	UCHAR iFont; // index to the font associated with this character;
	UCHAR ubConfidence; // Confidence in the accuracy of the word in scale of 0-100
};

struct EP_OCR_ELEMENT
{
	LONG lcbSize; // Size of the structure in bytes
	RECT rect; // Bounding rectangle
	LONG idParent; // The index of the parent element. The value is -1 if if there is no parent;
	LONG idChildStart; // The smallest index of this element's chidren. The value is -1 if if there is no child
	LONG idChildEnd; // The biggest index of this element's chidren. The value is -1 if if there is no child
	EP_OCR_ELEMENTTYPE lElementType; // The type of this element
	
	union
	{
		EP_OCR_REGION       uRegion;    // Region properties
		EP_OCR_LINE         uLine;      // Line properties
		EP_OCR_WORD         uWord;      // Word properties
		EP_OCR_CHARACTER    uCharacter; // Character properties
	};
};

typedef vector<EP_OCR_ELEMENT> EleVector;

struct EP_OCR_PAGEELEMENTLIST
{
	EleVector items;
	EP_OCR_ELEMENTTYPE itemType;
	EP_OCR_ELEMENTTYPE parentType;
	EP_OCR_ELEMENTTYPE childType;
};

//
// LoadRects - load plist from pstmLayout.  Assumes plist->items.size() contains the number
// of elements to be read
//
HRESULT LoadRects(EP_OCR_PAGEELEMENTLIST *plist, IStream *pstmLayout)
{
	SHORT *pBuf;
	SHORT preValueX = 0, preValueY = 0;
	LONG cItems = (LONG)(plist->items.size());

	HRESULT hr = S_OK;

	if ( cItems != 0 )
	{
		pBuf = new SHORT[4 * cItems];
		
		if ( pBuf == NULL )
			return E_OUTOFMEMORY;

		const SHORT *pXBuf = pBuf;
		const SHORT *pYBuf = pBuf + 2 * cItems;

		// rectangles are stored as an array of vertical edges followed by an array of
		// horizontal edges.  So the first two SHORT's are the left and right edge of the first
		// rectangle.  The 2*cItems and 2*cItems+1 elements are the top and bottom edge of the first
		// rectangle.
		// Additionally, each item in each array is expressed as a delta from the previous
		// item.
		// For example:  consider two rectangles.  The first is:
		//     left: 10, top: 10, right: 25, bottom: 40
		// and the second is
		//     left: 36, top: 41, right: 50, bottom: 50
		// cItems would be 2, so there would be 8 entries in the array.  And they would be:
		// 10, 15, 11, 14, 10, 30, 1, 9
		// That is to say that the vertical edges are at 10, 25, 36, and 50.  And that the horizontal
		// edgese are at 10, 40, 41, and 50.
		// the loop below converts these arrays into rectangles in absoltue coordinates.
		if (FAILED(hr = pstmLayout->Read(pBuf, 4 * cItems * sizeof(SHORT), NULL)))
			return hr;
		
		//Convert the x,y cordinate to deltas
		for(EleVector::iterator i=plist->items.begin(); i != plist->items.end(); ++i)
		{
			i->rect.left = (*pXBuf++) + preValueX;
			i->rect.right = (*pXBuf++) + i->rect.left;
			preValueX = (short)(i->rect.right);

			i->rect.top = (*pYBuf++) + preValueY;
			i->rect.bottom = (*pYBuf++) + i->rect.top;
			preValueY = (short)(i->rect.bottom);
		}

		delete [] pBuf;
	}

	return hr;
}

//
// LoadRegions - load plist from pstmLayout.  Assumes plist->items.size() already contains the
// number of items to read.
//
HRESULT LoadRegions(EP_OCR_PAGEELEMENTLIST *plist, IStream *pstmLayout)
{
	HRESULT hr = S_OK;
	EleVector::iterator i;
	ULONG cItems = (ULONG)(plist->items.size());
	ULONG cBytes;
	BYTE *pbBuf = NULL;

	if ( cItems != 0 )
	{
		pbBuf = new BYTE[cItems << 2];
		const BYTE *pb = pbBuf;
		const DWORD *pdw  = (const DWORD *)pbBuf;
		if(!pbBuf)
		{
			return E_OUTOFMEMORY;
		}

		//Load Orientation
		//Orientation takes only one byte
		hr = pstmLayout->Read(pbBuf, cItems, &cBytes);
		if(FAILED(hr) || cBytes != cItems)
		{
			hr = E_UNEXPECTED;
			goto error;
		}
			
		for(i=plist->items.begin(); i != plist->items.end(); ++i)
		{
			i->lcbSize = sizeof(EP_OCR_ELEMENT);
			i->uRegion.ucOrientation = *pb++;
		}

		//Load flowDir
		hr = pstmLayout->Read(pbBuf, cItems << 2, &cBytes);
		if(FAILED(hr) || cBytes != (cItems << 2))
		{
			hr = E_UNEXPECTED;
			goto error;
		}
			
		for(i=plist->items.begin(); i != plist->items.end(); ++i)
		{
			i->uRegion.flowdir = *pdw++;
		}

		//Load dwType
		pdw = (DWORD *)pbBuf;
		hr = pstmLayout->Read(pbBuf, cItems << 2, &cBytes);
		if(FAILED(hr) || cBytes != (cItems << 2))
		{
			hr = E_UNEXPECTED;
			goto error;
		}
			
		for(i=plist->items.begin(); i != plist->items.end(); ++i)
		{
			i->uRegion.dwType = *pdw++;
		}
	}

error:
	if ( pbBuf != NULL )
		delete [] pbBuf;

	return hr;
}

//
// LoadLines - load plist from pstmLayout.  Assumes plist->items.size() already contains the
// number of items to read.
//
HRESULT LoadLines(EP_OCR_PAGEELEMENTLIST *plist, IStream *pstmLayout)
{
	HRESULT hr = S_OK;
	EleVector::iterator i;
	ULONG cItems = (ULONG)(plist->items.size());
	ULONG cBytes;
	BYTE *pbBuf = NULL;
	
	if ( cItems != 0 )
	{
		pbBuf = new BYTE[cItems << 2];
		const DWORD *pdw  = (const DWORD *)pbBuf;
		if(!pbBuf)
		{
			return E_OUTOFMEMORY;
		}

		//Load dwReserved
		hr = pstmLayout->Read(pbBuf, cItems << 2, &cBytes);
		if(FAILED(hr) || cBytes != (cItems << 2))
		{
			hr = E_UNEXPECTED;
			goto error;
		}
			
		for(i=plist->items.begin(); i != plist->items.end(); ++i)
		{
			i->lcbSize = sizeof(EP_OCR_ELEMENT);
			i->uLine.dwReserved = *pdw++;
		}
	}

error:
	if ( pbBuf != NULL )
		delete [] pbBuf;

	return hr;
}

//
// LoadWords - load plist from pstmLayout.  Assumes plist->items.size() already contains the
// number of items to read.
//
HRESULT LoadWords(EP_OCR_PAGEELEMENTLIST *plist, IStream *pstmLayout)
{
	HRESULT hr = S_OK;
	EleVector::iterator i;
	ULONG cItems = (ULONG)(plist->items.size());
	ULONG cBytes;
	BYTE *pbBuf = NULL;
	
	if ( cItems != 0 )
	{
		pbBuf = new BYTE[cItems << 2];
		const BYTE *pb  = pbBuf;
		if(!pbBuf)
		{
			return E_OUTOFMEMORY;
		}

		//Load ubConfidence
		hr = pstmLayout->Read(pbBuf, cItems, &cBytes);
		if(FAILED(hr) || cBytes != (cItems))
		{
			hr = E_UNEXPECTED;
			goto error;
		}
			
		for(i=plist->items.begin(); i != plist->items.end(); ++i)
		{
			i->lcbSize = sizeof(EP_OCR_ELEMENT);
			i->uWord.ubConfidence = *pb++;
		}
	}

error:
	if ( pbBuf != NULL )
		delete [] pbBuf;

	return hr;
}

//
// LoadChars - load plist from pstmLayout.  Assumes plist->items.size() already contains the
// number of items to read.
//
HRESULT LoadChars(EP_OCR_PAGEELEMENTLIST *plist, IStream *pstmLayout)
{
	HRESULT hr = S_OK;
	EleVector::iterator i;
	ULONG cItems = (ULONG)(plist->items.size());
	ULONG cBytes;
	BYTE *pbBuf = NULL;

	if ( cItems != 0 )
	{
		pbBuf = new BYTE[cItems ];
		const BYTE *pb = pbBuf;
		if(!pbBuf)
		{
			return E_OUTOFMEMORY;
		}

		//Load ubConfidence
		//ubConfidence takes only one byte; 
		hr = pstmLayout->Read(pbBuf, cItems, &cBytes);
		if(FAILED(hr) || cBytes != cItems)
		{
			goto error;
		}
		for(i=plist->items.begin(); i != plist->items.end(); ++i)
		{
			i->uCharacter.ubConfidence = *pb++ ;
		}
		
		//Load font
		pb = pbBuf;
		hr = pstmLayout->Read(pbBuf, cItems , &cBytes);

		if(FAILED(hr) || cBytes != (cItems ))
		{
			goto error;
		}
		for(i=plist->items.begin(); i != plist->items.end(); ++i)
		{
			i->uCharacter.iFont = *pb++ ;
		}
	}

error:
	if ( pbBuf != NULL )
		delete [] pbBuf;

	return hr;
}

//
// LoadList - loads a list of objects of type itemType.  Assumes plist->items.size() already contains the
// number of items to read.
//
HRESULT LoadList(EP_OCR_ELEMENTTYPE itemType, EP_OCR_PAGEELEMENTLIST *plist, IStream *pstmLayout)
{
	HRESULT hr = S_OK;

	if(itemType != EP_OCR_ELEMENT_CHAR)
	{
		if (FAILED(hr = LoadRects(plist, pstmLayout)))
			return hr;
	}
	
	EleVector::iterator i;

	//Fill size field
	for(i=plist->items.begin(); i != plist->items.end(); ++i)
	{
		i->lcbSize = sizeof(EP_OCR_ELEMENT);
	}

	switch(itemType)
	{
		case EP_OCR_ELEMENT_REGION:
			hr = LoadRegions(plist, pstmLayout);
			break;
		case EP_OCR_ELEMENT_LINE:
			hr = LoadLines(plist, pstmLayout);
			break;
		case EP_OCR_ELEMENT_WORD:
			hr = LoadWords(plist, pstmLayout);
			break;
		case EP_OCR_ELEMENT_CHAR:
			hr = LoadChars(plist, pstmLayout);
			break;
		default:
			return E_UNEXPECTED;
	}

	return hr;
}

//
// LoadTextFromStream - load UTF8 text from pstm
//
HRESULT LoadTextFromStream(IStream *pStm, WCHAR **ppwcText, long *pcChars)
{
	USHORT usTextFormat;
	char *pchBuf = NULL;
	DWORD dwBytes = 0;

	*ppwcText = NULL;
	*pcChars = 0;

	//Read the format of the buffer
	HRESULT hr = pStm->Read(&usTextFormat, sizeof(USHORT), 0);
	if(SUCCEEDED(hr))
	{
		//Check if format is what we can handle
		if(usTextFormat != 1)
		{
			hr = E_INVALIDARG;
		}
	}

	//Read length of the buffer
	if(SUCCEEDED(hr))
	{
		hr = pStm->Read(&dwBytes, sizeof(DWORD), 0);
	}

	if ( dwBytes != 0 )
	{
		//Read UTF8 text
		if(SUCCEEDED(hr))
		{
			pchBuf = new char[dwBytes];
			*ppwcText = new WCHAR[dwBytes];
			if(pchBuf && *ppwcText)
			{
				hr = pStm->Read(pchBuf, dwBytes, 0);
			}
			else
			{
				hr = E_OUTOFMEMORY;
			}
		}
		
		//Convert to UNICODE
		if(SUCCEEDED(hr))
		{
			*pcChars = MultiByteToWideChar(CP_UTF8, 0, pchBuf, dwBytes, *ppwcText, dwBytes);
			if(*pcChars == 0)
			{
				hr = HRESULT_FROM_WIN32(::GetLastError());
				delete [] *ppwcText;
				*ppwcText = NULL;
			}
		}
	}

	if(pchBuf)
	{
		delete [] pchBuf;
	}

	return hr;

}

//
// LoadFromText - load map from pStm.  Pstm is assumed to point at the beginning of the text buffer
//
HRESULT LoadFromText(IStream *pStm, EP_OCR_PAGEELEMENTLIST *map)
{
	LONG iLine=0, iWord=0, iChar=0;
	LONG lcLine=0, lcWord=0, lcChar=0;
	HRESULT hr = S_OK;
	WCHAR wchValue;
	EP_OCR_ELEMENT elRegion ={0}, elLine = {0},  elWord ={0}, elChar = {0};
	elRegion.lcbSize = elLine.lcbSize = elWord.lcbSize = elChar.lcbSize = sizeof(EP_OCR_ELEMENT);
	elRegion.idParent = -1;
	elChar.idChildEnd = elChar.idChildStart = -1;
	elRegion.lElementType = EP_OCR_ELEMENT_REGION;
	elLine.lElementType = EP_OCR_ELEMENT_LINE;
	elWord.lElementType = EP_OCR_ELEMENT_WORD;
	elChar.lElementType = EP_OCR_ELEMENT_CHAR;

	WCHAR *pwcBuf = NULL;
	long   cChars = 0;
	//Load Text Buffer
	hr = LoadTextFromStream(pStm, &pwcBuf, &cChars);

	if(SUCCEEDED(hr) && cChars)
	{
		const WCHAR *pwcBufEnd = pwcBuf + cChars;
		//Keep reading token from buffer 
		for(WCHAR *pwcCur = pwcBuf; pwcCur < pwcBufEnd; pwcCur ++)
		{
			wchValue = *pwcCur;

			if(wchValue == WCH_PAGE_SEPERATOR)
			{
				break;
			}
			switch(wchValue)
			{
			case WCH_WORD_SEPERATOR:
				// We check if the child list has grown since adding the last element.
				if ( lcChar != iChar )
				{
					elWord.idParent = (int)(map[EP_OCR_ELEMENT_LINE].items.size());
					elWord.idChildStart = iChar;
					lcChar = iChar = (int)(map[EP_OCR_ELEMENT_CHAR].items.size());
					elWord.idChildEnd = lcChar - 1;
					map[EP_OCR_ELEMENT_WORD].items.push_back(elWord);
					lcWord++;
				} else {  
					hr = E_FAIL; // Unexpected seperator 
				}
				break;
			case WCH_LINE_SEPERATOR:
				if ( lcWord != iWord )
				{
					elLine.idParent = (int)(map[EP_OCR_ELEMENT_REGION].items.size());
					elLine.idChildStart = iWord;
					lcWord = iWord = (int)(map[EP_OCR_ELEMENT_WORD].items.size());
					elLine.idChildEnd = lcWord - 1;
					map[EP_OCR_ELEMENT_LINE].items.push_back(elLine);
					lcLine++;
				} else {
					hr = E_FAIL; // Unexpected seperator
				}
				break;
			case WCH_REGION_SEPERATOR:
				if ( lcLine != iLine )
				{
					elRegion.idChildStart = iLine;
					lcLine = iLine = (int)(map[EP_OCR_ELEMENT_LINE].items.size());
					elRegion.idChildEnd = lcLine - 1;
					map[EP_OCR_ELEMENT_REGION].items.push_back(elRegion);
				} else {
					hr = E_FAIL; // Unexpected seperator 
				}
				break;
			default:
				//Characters
				elChar.idParent = (int)(map[EP_OCR_ELEMENT_WORD].items.size());
				map[EP_OCR_ELEMENT_CHAR].items.push_back(elChar);
				lcChar++;
				break;
			}
			
			if ( FAILED(hr) )
				break;
		}
	}

	if(pwcBuf)
	{
		delete [] pwcBuf;
	}

	return hr;

}

//
// OutputElementData - dump an elemene to debug output
//
void OutputElementData(EP_OCR_ELEMENT *elem)
{
	char sz[255];

	switch(elem->lElementType)
	{
		case EP_OCR_ELEMENT_REGION:
			sprintf_s(sz, "    orientation: %u\n    flowdir: %u\n    dwType: %u\n", elem->uRegion.ucOrientation, elem->uRegion.flowdir, elem->uRegion.dwType);
			OutputDebugString(sz);
			break;
		case EP_OCR_ELEMENT_LINE:
			sprintf_s(sz, "    dwReserved (always 0): %u\n", elem->uLine.dwReserved);
			OutputDebugString(sz);
			break;
		case EP_OCR_ELEMENT_WORD:
			sprintf_s(sz, "    ubConfidence (0 - 100): %u\n", elem->uWord.ubConfidence);
			OutputDebugString(sz);
			break;
		case EP_OCR_ELEMENT_CHAR:
			sprintf_s(sz, "    iFont: %u\n    ubConfidence (0 - 100): %u\n", elem->uCharacter.iFont, elem->uCharacter.ubConfidence);
			OutputDebugString(sz);
			break;
	}
}

//
// DbgDumpElements - dump elements in map
//
void DbgDumpElements(EP_OCR_PAGEELEMENTLIST *map)
{
	char sz[255];
	char *typeName;
	EleVector::iterator i;

	for (int itemType = 0; itemType < cList; itemType++)
	{
		switch(itemType)
		{
			case EP_OCR_ELEMENT_REGION:
				typeName = "Region";
				break;
			case EP_OCR_ELEMENT_LINE:
				typeName = "Line";
				break;
			case EP_OCR_ELEMENT_WORD:
				typeName = "Word";
				break;
			case EP_OCR_ELEMENT_CHAR:
				typeName = "Char";
				break;
			default:
				typeName = "Unknown";
				break;
		}

		sprintf_s(sz, "---Begin %s Elements---\n", typeName);
		OutputDebugString(sz);

		int count = 0;
		for(i = map[itemType].items.begin(); i != map[itemType].items.end(); ++i)
		{
			sprintf_s(sz, "  %s Index: %u\n", typeName, count++);
			OutputDebugString(sz);
			if(itemType != EP_OCR_ELEMENT_CHAR)
			{
				sprintf_s(sz, "  Bounding Rectangle, top: %u bottom: %u left: %u right: %u\n", i->rect.top, i->rect.bottom, i->rect.left, i->rect.right);
				OutputDebugString(sz);
			}
			sprintf_s(sz, "  idParent: %d idChildStart: %d idCHildEnd: %d\n", i->idParent, i->idChildStart, i->idChildEnd);
			OutputDebugString(sz);
			OutputElementData(&(*i)); // Make iterator a pointer
		}

		sprintf_s(sz, "---End %s Elements---\n", typeName);
		OutputDebugString(sz);
	}
}

//
// DbgDumpStmLayout - dump a layout stream to debug output
//
void DbgDumpStmLayout(IStream *pstmText, IStream *pstmLayout)
{
	char sz[255];
	DWORD dwVersion;
	EP_OCR_STATISTICS stat;
	EP_OCR_PROPERTIES props;
	vector<EP_OCR_FONT> fonts;

	EP_OCR_PAGEELEMENTLIST map[cList];

	struct list_hierarchy {
		EP_OCR_ELEMENTTYPE tParent;
		EP_OCR_ELEMENTTYPE tItem;
		EP_OCR_ELEMENTTYPE tChild;
	};

	list_hierarchy lists[] = {
		{EP_OCR_ELEMENT_INVALID, EP_OCR_ELEMENT_REGION, EP_OCR_ELEMENT_LINE},
		{EP_OCR_ELEMENT_REGION, EP_OCR_ELEMENT_LINE, EP_OCR_ELEMENT_WORD},
		{EP_OCR_ELEMENT_LINE, EP_OCR_ELEMENT_WORD, EP_OCR_ELEMENT_CHAR},
		{EP_OCR_ELEMENT_WORD, EP_OCR_ELEMENT_CHAR, EP_OCR_ELEMENT_INVALID}
	};

	for(int i = 0; i < cList; i++)
	{
		map[i].itemType = lists[i].tItem;
		map[i].parentType = lists[i].tParent;
		map[i].childType = lists[i].tChild;
	}

	LoadFromText(pstmText, map);

	OutputDebugString("---Begin Layout Stream---\n");

	// first 4 bytes are the version
	if (FAILED(pstmLayout->Read(&dwVersion, sizeof(dwVersion), NULL /*pcbRead*/)))
		goto _LRet;

	sprintf_s(sz, "major version: %x\n", dwVersion & 0xffff);
	OutputDebugString(sz);
	sprintf_s(sz, "minor version: %x\n", dwVersion >> 16);
	OutputDebugString(sz);

	// next 4 bytes are the size of props
	if (FAILED(pstmLayout->Read(&props.lcbSize, sizeof(props.lcbSize), NULL /*pcbRead*/)))
		goto _LRet;
	if (props.lcbSize != sizeof(props))
		goto _LRet;
	// next is the rest of EP_OCR_PROPERTIES
	if (FAILED(pstmLayout->Read(&props.lcid, sizeof(props) - sizeof(props.lcbSize), NULL /*pcbRead*/)))
		goto _LRet;

	DbgDumpProps(&props);

	// next 4 bytes is the size of EP_OCR_STATISTICS
	if (FAILED(pstmLayout->Read(&stat.lcbSize, sizeof(stat.lcbSize), NULL /*pcbRead*/)))
		goto _LRet;
	if (stat.lcbSize != sizeof(stat))
		goto _LRet;
	// next is the rest of EP_OCR_STATISTICS
	if (FAILED(pstmLayout->Read(&stat.cCharsRecognized, sizeof(stat) - sizeof(stat.lcbSize), NULL /*pcbRead*/)))
		goto _LRet;

	DbgDumpStats(&stat);

	if (FAILED(LoadFontTable(&fonts, pstmLayout)))
		goto _LRet;

	DbgDumpFonts(&fonts);

	for (int i = 0; i < cList; i++)
	{
		if (FAILED(LoadList((EP_OCR_ELEMENTTYPE)i, &map[i], pstmLayout)))
			goto _LRet;
	}

	DbgDumpElements(map);

_LRet:
	OutputDebugString("---End Layout Stream---\n");
}
