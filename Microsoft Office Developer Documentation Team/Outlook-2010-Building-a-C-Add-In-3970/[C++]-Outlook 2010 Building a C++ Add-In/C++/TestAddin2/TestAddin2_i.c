

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


 /* File created by MIDL compiler version 7.00.0550 */
/* at Fri Sep 25 09:11:45 2009
 */
/* Compiler settings for TestAddin2.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 7.00.0550 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


#ifdef __cplusplus
extern "C"{
#endif 


#include <rpc.h>
#include <rpcndr.h>

#ifdef _MIDL_USE_GUIDDEF_

#ifndef INITGUID
#define INITGUID
#include <guiddef.h>
#undef INITGUID
#else
#include <guiddef.h>
#endif

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        DEFINE_GUID(name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8)

#else // !_MIDL_USE_GUIDDEF_

#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        const type name = {l,w1,w2,{b1,b2,b3,b4,b5,b6,b7,b8}}

#endif !_MIDL_USE_GUIDDEF_

MIDL_DEFINE_GUID(IID, IID_IRibbonCallback,0xCE895442,0x9981,0x4315,0xAA,0x85,0x4B,0x9A,0x5C,0x77,0x39,0xD8);


MIDL_DEFINE_GUID(IID, IID_IConnect,0x0B80B66C,0x58B7,0x434B,0xAE,0x44,0x79,0x0E,0xAE,0x62,0x55,0x14);


MIDL_DEFINE_GUID(IID, LIBID_TestAddin2Lib,0x0C94B6BE,0x7D35,0x46DC,0x92,0xD1,0xD6,0xFD,0x51,0x87,0xEC,0xF2);


MIDL_DEFINE_GUID(CLSID, CLSID_Connect,0xD7A48F42,0xCB48,0x4BB9,0x86,0x7C,0x7C,0xEE,0x38,0x7F,0xB2,0x2A);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



