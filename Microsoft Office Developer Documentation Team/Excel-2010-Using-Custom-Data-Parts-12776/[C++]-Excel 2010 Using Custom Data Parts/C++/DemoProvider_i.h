

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Tue Feb 16 14:13:53 2010
 */
/* Compiler settings for .\DemoProvider.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__


#ifndef __DemoProvider_i_h__
#define __DemoProvider_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __MyProvider_FWD_DEFINED__
#define __MyProvider_FWD_DEFINED__

#ifdef __cplusplus
typedef class MyProvider MyProvider;
#else
typedef struct MyProvider MyProvider;
#endif /* __cplusplus */

#endif 	/* __MyProvider_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __DemoProviderLib_LIBRARY_DEFINED__
#define __DemoProviderLib_LIBRARY_DEFINED__

/* library DemoProviderLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_DemoProviderLib;

EXTERN_C const CLSID CLSID_MyProvider;

#ifdef __cplusplus

class DECLSPEC_UUID("58FC4998-E801-4A1F-A6A1-AA9C13B529BE")
MyProvider;
#endif
#endif /* __DemoProviderLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


