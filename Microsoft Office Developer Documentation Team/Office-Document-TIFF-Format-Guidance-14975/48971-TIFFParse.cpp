#include <stdio.h>
#include <windows.h>

//
// NOTE: to compile and run this code you must supply your own version of zlib.
// You may or may not need to change these defines based on your implementation.
//
#define ZEXPORTVA __cdecl
#define ZEXPORT __stdcall
#define ZCALLBACK __stdcall
#include "zlib.h"

#define INITGUID
#include <guiddef.h>

#define tagText 37679 
#define tagProp 37681

#define typeUNDEFINED 7 // from the TIFF spec

void DbgDumpStmLayout(IStream *pstrmText, IStream *pstmLayout);

//
// IFD Entry
//
struct IFDE
{
	unsigned short tag;
	unsigned short type; // what type are values?
	unsigned long cVal; // number of values
	unsigned long bVal; // offset to array of values
};

//
// IFD (Image File Directory)
//
struct IFD
{
	unsigned short cifde;
	unsigned long bifdNext;
	IFDE rgifde[1]; // will be dynamically sized when we load.
};

//
// EP_TAGDATA_HEADER - header for compressed data
// 
struct EP_TAGDATA_HEADER
{
	DWORD dwVersion ;
	DWORD cbFullSize ;
	DWORD dwCompression ;
	DWORD offReserved ;
	DWORD cbReserved ;
} ;

//
// FVerifyTIFFHeader: verify that this is a little-endian TIFF.
// 
// if it returns TRUE, the *pbIFD is the offset of the first IFD
// in the file.  If it returns FALSE, *pbIFD is undefined.
//
int FVerifyTIFFHeader(FILE *f, int *pbIFD)
{
	char rgchFirst[2];
	short sw42;
	
	// Little endian TIFF's start with two 'I' characters
	if (fread(rgchFirst, 1, 2, f) != 2)
		return FALSE;
		
	if (rgchFirst[0] != 'I' || rgchFirst[1] != 'I')
		return FALSE;
		
	// Next two bytes are 42 (in the appropriate byte order)
	if (fread(&sw42, 2, 1, f) != 1)
		return FALSE;
		
	if (sw42 != 42)
		return FALSE;
		
	// Next 4 bytes are the offset of the first IFD (in the appropriate byte order)
	if (fread(pbIFD, 4, 1, f) != 1)
		return FALSE;
		
	return TRUE;
}

//
// PifdRead: Allocate an IFD and read from offset bIFD
//
IFD *PifdRead(FILE *f, int bIFD)
{
	unsigned short cifde;
	IFD *pifd;
	
	if (fseek(f, bIFD, 0) != 0) // fseek returns 0 on success
		return NULL;
	
	if (fread(&cifde, 2, 1, f) != 1)
		return NULL;

	// allocate variable-sized IFD.  Note that we use "cifde - 1" because
	// the IFD has room for 1 IFDE in it.
	pifd = (IFD *)(malloc(sizeof(IFD) + (cifde - 1) * sizeof(IFDE)));
	if (pifd == NULL)
		return NULL;
	pifd->cifde = cifde;

	if (fread(pifd->rgifde, sizeof(IFDE), cifde, f) != cifde)
		{
		free(pifd);
		return NULL;
		}
		
	if (fread(&pifd->bifdNext, 4, 1, f) != 1)
		{
		free(pifd);
		return NULL;
		}
		
	return pifd;
}

//
// PstmFromIFDE - given an IFDE, decompress the referenced data and wrap it in an IStream
//
IStream *PstmFromIFDE(FILE *f, const IFDE *pifde)
{
	HGLOBAL hTag = NULL;
	void *pvTag = NULL;
	IStream *pstm = NULL;

	if (pifde == NULL)
		goto _LRet;

	if (pifde->cVal < sizeof(EP_TAGDATA_HEADER))
		goto _LRet; // not enough data.  corrupt tag.

	if (fseek(f, pifde->bVal, 0) != 0) // fseek returns 0 on success
		goto _LRet;

	hTag = GlobalAlloc(GMEM_MOVEABLE, pifde->cVal);
	if (hTag == NULL)
		goto _LRet;

	pvTag = GlobalLock(hTag);
	if (pvTag == NULL)
		goto _LRet;

	if (fread(pvTag, 1, pifde->cVal, f) != pifde->cVal)
		goto _LRet;

	GlobalUnlock(hTag);
	pvTag = NULL; // we just need hTag from here on out.

	// treat the uncompressed data as an IStream
	if (FAILED(CreateStreamOnHGlobal(hTag, TRUE /*DeleteOnRelease*/, &pstm)))
		{
		goto _LRet;
		}
	hTag = NULL; // releasing pstm will free h.
	
_LRet:
	if (pvTag != NULL)
		GlobalUnlock(hTag);
	if (hTag != NULL)
		GlobalFree(hTag);
	return pstm;
}

//
// PstmLayoutFromIFDECompressed: creates an IStream from an IFDE with tag set
//                               to tagProp.  The data begins with an EP_TAGDATA_HEADER and then
//                               has a bunch of LZ compressed data after it.
//
IStream *PstmLayoutFromIFDECompressed(FILE *f, const IFDE *pifde)
{
	HGLOBAL hDecomp = NULL;
	void *pvTag = NULL;
	void *pvCompData = NULL;
	void *pvDecomp = NULL;
	EP_TAGDATA_HEADER header;
	IStream *pstm = NULL;

	if (pifde == NULL
			|| pifde->tag != tagProp
			|| pifde->type != typeUNDEFINED)
		{
		goto _LRet;
		}

	if (pifde->cVal < sizeof(EP_TAGDATA_HEADER))
		goto _LRet; // not enough data.  corrupt tag.

	if (fseek(f, pifde->bVal, 0) != 0) // fseek returns 0 on success
		goto _LRet;

	pvTag = malloc(pifde->cVal);
	if (pvTag == NULL)
		goto _LRet;

	if (fread(pvTag, 1, pifde->cVal, f) != pifde->cVal)
		goto _LRet;

	// read the header and point pvCompData at the start of the compressed data
	header = *(EP_TAGDATA_HEADER *)pvTag;
	pvCompData = ((char *)pvTag) + sizeof(header);

	if (header.dwVersion != 2)
		goto _LRet; // this sample code pertains to version 2

	if (header.dwCompression != 1)
		goto _LRet; // corrupt file - this should always be 1

	// allocate the decompression buffer.  We allocated cbFullSize+1 so that we can make
	// sure we didn't run out of space for decompression.
	hDecomp = GlobalAlloc(GMEM_MOVEABLE, header.cbFullSize + 1);
	if (hDecomp == NULL)
		goto _LRet;

	pvDecomp = GlobalLock(hDecomp);
	if (pvDecomp == NULL)
		goto _LRet;

	// initialize the decompression stream
	z_stream zs;

	zs.next_in = (Bytef *)pvCompData;
	zs.avail_in = pifde->cVal - sizeof(header); // because we advanced pvCompData above
	zs.total_in = 0;
	zs.next_out = (Bytef *)pvDecomp;
	zs.avail_out = header.cbFullSize + 1;
	zs.total_out = 0;
	zs.msg = NULL;
	zs.state = NULL;
	zs.zalloc = Z_NULL;
	zs.zfree = Z_NULL;
	zs.opaque = 0;
	zs.data_type = Z_BINARY;
	zs.adler = 0;
	zs.reserved = 0;

	if (inflateInit(&zs) != Z_OK)
		goto _LRet;

	// perform the decompression
	if (inflate(&zs, Z_SYNC_FLUSH) != Z_STREAM_END)
		goto _LRet;

	// zs.avail_out should not be 0 because we allocated 1 more byte than we needed.
	if (zs.avail_out == 0)
		goto _LRet;

	if (inflateEnd(&zs) != Z_OK)
		goto _LRet;

	GlobalUnlock(pvDecomp);
	pvDecomp = NULL; // we just need hDecomp from here on out.

	// treat the uncompressed data as an IStream
	if (FAILED(CreateStreamOnHGlobal(hDecomp, TRUE /*DeleteOnRelease*/, &pstm)))
		{
		goto _LRet;
		}
	hDecomp = NULL; // releasing pstm will free h.
	
_LRet:
	if (pvTag != NULL)
		free(pvTag);
	if (pvDecomp != NULL)
		GlobalUnlock(hDecomp);
	if (hDecomp != NULL)
		GlobalFree(hDecomp);
	return pstm;
}

//
// main - process command line arguments, dump tag data
//
int main(int argc, char *argv[])
{
	FILE *f = NULL;
	int bIFD;
	IFD *pifd;
	IStream *pstmText = NULL;
	IStream *pstmLayout = NULL;
	char sz[255];
	int iRet = 1;

	if (argc < 2)
		goto _LRet;
	
	iRet = 2;
	if (fopen_s(&f, argv[1], "rb") != 0)
		goto _LRet;

	iRet = 3;
	if (!FVerifyTIFFHeader(f, &bIFD))
		goto _LRet;

	iRet = 4;

	int pageNo = 1;

	while (bIFD != 0
		   && (pifd = PifdRead(f, bIFD)) != NULL)
		{
		int iifde;
		
		for (iifde = 0; iifde < pifd->cifde; iifde++)
			{
			switch (pifd->rgifde[iifde].tag)
				{
				case tagProp:
					if (pstmText == NULL)
						goto _LRet; // Text should come before layout

					pstmLayout = PstmLayoutFromIFDECompressed(f, &pifd->rgifde[iifde]);
					if (pstmLayout == NULL)
						goto _LRet;

					sprintf_s(sz, "DEBUG OUTPUT FOR PAGE %u\n", pageNo++);
					OutputDebugString(sz);
					DbgDumpStmLayout(pstmText, pstmLayout);

					pstmLayout->Release();
					pstmText->Release();
					pstmLayout = pstmText = NULL;
					break;
				case tagText:
					pstmText = PstmFromIFDE(f, &pifd->rgifde[iifde]);
					if (pstmText == NULL)
						goto _LRet;
					break;
				}
			}
		bIFD = pifd->bifdNext;
		free(pifd);
		}

	iRet = 0;
_LRet:
	if (pstmLayout != NULL)
		pstmLayout->Release();
	if (pstmText != NULL)
		pstmText->Release();

	return iRet;
}