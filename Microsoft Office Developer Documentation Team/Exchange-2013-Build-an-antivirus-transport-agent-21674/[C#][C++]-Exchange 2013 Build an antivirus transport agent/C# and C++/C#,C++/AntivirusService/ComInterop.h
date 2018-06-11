

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0595 */
/* at Thu May 02 09:26:53 2013
 */
/* Compiler settings for ComInterop.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0595 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

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


#ifndef __ComInterop_h__
#define __ComInterop_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IComInvoke_FWD_DEFINED__
#define __IComInvoke_FWD_DEFINED__
typedef interface IComInvoke IComInvoke;

#endif 	/* __IComInvoke_FWD_DEFINED__ */


#ifndef __IComCallback_FWD_DEFINED__
#define __IComCallback_FWD_DEFINED__
typedef interface IComCallback IComCallback;

#endif 	/* __IComCallback_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __ComInterop_LIBRARY_DEFINED__
#define __ComInterop_LIBRARY_DEFINED__

/* library ComInterop */
/* [version][uuid] */ 




EXTERN_C const IID LIBID_ComInterop;

#ifndef __IComInvoke_INTERFACE_DEFINED__
#define __IComInvoke_INTERFACE_DEFINED__

/* interface IComInvoke */
/* [object][custom][oleautomation][version][uuid] */ 


EXTERN_C const IID IID_IComInvoke;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("7578C871-D9B3-455a-8371-A82F7D864D0D")
    IComInvoke : public IUnknown
    {
    public:
        virtual HRESULT __stdcall BeginVirusScan( 
            /* [in] */ IComCallback *callback) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IComInvokeVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IComInvoke * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IComInvoke * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IComInvoke * This);
        
        HRESULT ( __stdcall *BeginVirusScan )( 
            IComInvoke * This,
            /* [in] */ IComCallback *callback);
        
        END_INTERFACE
    } IComInvokeVtbl;

    interface IComInvoke
    {
        CONST_VTBL struct IComInvokeVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IComInvoke_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IComInvoke_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IComInvoke_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IComInvoke_BeginVirusScan(This,callback)	\
    ( (This)->lpVtbl -> BeginVirusScan(This,callback) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IComInvoke_INTERFACE_DEFINED__ */


#ifndef __IComCallback_INTERFACE_DEFINED__
#define __IComCallback_INTERFACE_DEFINED__

/* interface IComCallback */
/* [object][custom][oleautomation][version][uuid] */ 


EXTERN_C const IID IID_IComCallback;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("3E0D4ED3-898B-470f-AD77-921F2AE29F74")
    IComCallback : public IUnknown
    {
    public:
        virtual HRESULT __stdcall VirusScanCompleted( void) = 0;
        
        virtual HRESULT __stdcall GetContentStream( 
            /* [out] */ IStream **stream) = 0;
        
        virtual HRESULT __stdcall SetContentStream( 
            /* [in] */ IUnknown *stream) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IComCallbackVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IComCallback * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IComCallback * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IComCallback * This);
        
        HRESULT ( __stdcall *VirusScanCompleted )( 
            IComCallback * This);
        
        HRESULT ( __stdcall *GetContentStream )( 
            IComCallback * This,
            /* [out] */ IStream **stream);
        
        HRESULT ( __stdcall *SetContentStream )( 
            IComCallback * This,
            /* [in] */ IUnknown *stream);
        
        END_INTERFACE
    } IComCallbackVtbl;

    interface IComCallback
    {
        CONST_VTBL struct IComCallbackVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IComCallback_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IComCallback_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IComCallback_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IComCallback_VirusScanCompleted(This)	\
    ( (This)->lpVtbl -> VirusScanCompleted(This) ) 

#define IComCallback_GetContentStream(This,stream)	\
    ( (This)->lpVtbl -> GetContentStream(This,stream) ) 

#define IComCallback_SetContentStream(This,stream)	\
    ( (This)->lpVtbl -> SetContentStream(This,stream) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IComCallback_INTERFACE_DEFINED__ */

#endif /* __ComInterop_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


