

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Thu Jun 17 14:45:22 2010
 */
/* Compiler settings for .\OSCProvider_CPP.idl:
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


#ifndef __OSCProvider_CPP_i_h__
#define __OSCProvider_CPP_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __OSCProvider_FWD_DEFINED__
#define __OSCProvider_FWD_DEFINED__

#ifdef __cplusplus
typedef class OSCProvider OSCProvider;
#else
typedef struct OSCProvider OSCProvider;
#endif /* __cplusplus */

#endif 	/* __OSCProvider_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_OSCProvider_CPP_0000_0000 */
/* [local] */ 

#pragma once


extern RPC_IF_HANDLE __MIDL_itf_OSCProvider_CPP_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_OSCProvider_CPP_0000_0000_v0_0_s_ifspec;


#ifndef __OSCProvider_CPPLib_LIBRARY_DEFINED__
#define __OSCProvider_CPPLib_LIBRARY_DEFINED__

/* library OSCProvider_CPPLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_OSCProvider_CPPLib;

EXTERN_C const CLSID CLSID_OSCProvider;

#ifdef __cplusplus

class DECLSPEC_UUID("29A3703F-D234-4571-B601-DA03A1E12F60")
OSCProvider;
#endif
#endif /* __OSCProvider_CPPLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


