

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0499 */
/* at Sat May 15 17:46:29 2010
 */
/* Compiler settings for socialprovider.idl:
    Oicf, W0, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
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


#ifndef __socialprovider_h__
#define __socialprovider_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ISocialSession_FWD_DEFINED__
#define __ISocialSession_FWD_DEFINED__
typedef interface ISocialSession ISocialSession;
#endif 	/* __ISocialSession_FWD_DEFINED__ */


#ifndef __ISocialPerson_FWD_DEFINED__
#define __ISocialPerson_FWD_DEFINED__
typedef interface ISocialPerson ISocialPerson;
#endif 	/* __ISocialPerson_FWD_DEFINED__ */


#ifndef __ISocialProfile_FWD_DEFINED__
#define __ISocialProfile_FWD_DEFINED__
typedef interface ISocialProfile ISocialProfile;
#endif 	/* __ISocialProfile_FWD_DEFINED__ */


#ifndef __ISocialProvider_FWD_DEFINED__
#define __ISocialProvider_FWD_DEFINED__
typedef interface ISocialProvider ISocialProvider;
#endif 	/* __ISocialProvider_FWD_DEFINED__ */


#ifndef __ISocialSession2_FWD_DEFINED__
#define __ISocialSession2_FWD_DEFINED__
typedef interface ISocialSession2 ISocialSession2;
#endif 	/* __ISocialSession2_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_socialprovider_0000_0000 */
/* [local] */ 

#define  OSC_E_OUT_OF_MEMORY                 E_OUTOFMEMORY
#define  OSC_E_INVALIDARG                    E_INVALIDARG
#define  OSC_E_NOT_IMPLEMENTED               E_NOTIMPL
#define  OSC_E_INTERNAL_ERROR                MAKE_HRESULT(SEVERITY_ERROR,FACILITY_ITF,0x1400)
#define  OSC_E_VERSION                       MAKE_HRESULT(SEVERITY_ERROR,FACILITY_ITF,0x1401)
#define  OSC_E_COULDNOTCONNECT               MAKE_HRESULT(SEVERITY_ERROR,FACILITY_ITF,0x1402)
#define  OSC_E_PERMISSION_DENIED             MAKE_HRESULT(SEVERITY_ERROR,FACILITY_ITF,0x1403)
#define  OSC_E_AUTH_ERROR                    MAKE_HRESULT(SEVERITY_ERROR,FACILITY_ITF,0x1404)
#define  OSC_E_NOT_FOUND                     MAKE_HRESULT(SEVERITY_ERROR,FACILITY_ITF,0x1405)
#define  OSC_E_SERVER_VERSION_NOT_SUPPORTED  MAKE_HRESULT(SEVERITY_ERROR,FACILITY_ITF,0x1406)


extern RPC_IF_HANDLE __MIDL_itf_socialprovider_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_socialprovider_0000_0000_v0_0_s_ifspec;


#ifndef __OutlookSocialProvider_LIBRARY_DEFINED__
#define __OutlookSocialProvider_LIBRARY_DEFINED__

/* library OutlookSocialProvider */
/* [helpstring][version][uuid] */ 





EXTERN_C const IID LIBID_OutlookSocialProvider;

#ifndef __ISocialSession_INTERFACE_DEFINED__
#define __ISocialSession_INTERFACE_DEFINED__

/* interface ISocialSession */
/* [helpstring][unique][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_ISocialSession;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("5AFE10C7-CAB3-4b77-89C9-2E4FD62E7A1F")
    ISocialSession : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Logon( 
            /* [in] */ BSTR userName,
            /* [in] */ BSTR password) = 0;
        
        virtual /* [propput] */ HRESULT STDMETHODCALLTYPE put_SiteUrl( 
            /* [in] */ BSTR url) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE LogonWeb( 
            /* [in] */ BSTR connectIn,
            /* [out] */ BSTR *connectOut) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetLogonUrl( 
            /* [retval][out] */ BSTR *url) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetNetworkIdentifier( 
            /* [retval][out] */ BSTR *networkIdentifier) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetLoggedOnUser( 
            /* [retval][out] */ ISocialProfile **user) = 0;
        
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_LoggedOnUserID( 
            /* [retval][out] */ BSTR *userID) = 0;
        
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_LoggedOnUserName( 
            /* [retval][out] */ BSTR *userName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetPerson( 
            /* [in] */ BSTR userID,
            /* [retval][out] */ ISocialPerson **person) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FindPerson( 
            /* [in] */ BSTR userID,
            /* [retval][out] */ BSTR *result) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetActivities( 
            /* [in] */ SAFEARRAY * emailAddresses,
            /* [in] */ DATE startTime,
            /* [retval][out] */ BSTR *activities) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FollowPerson( 
            /* [in] */ BSTR emailAddress) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE UnFollowPerson( 
            /* [in] */ BSTR userID) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISocialSessionVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISocialSession * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISocialSession * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISocialSession * This);
        
        HRESULT ( STDMETHODCALLTYPE *Logon )( 
            ISocialSession * This,
            /* [in] */ BSTR userName,
            /* [in] */ BSTR password);
        
        /* [propput] */ HRESULT ( STDMETHODCALLTYPE *put_SiteUrl )( 
            ISocialSession * This,
            /* [in] */ BSTR url);
        
        HRESULT ( STDMETHODCALLTYPE *LogonWeb )( 
            ISocialSession * This,
            /* [in] */ BSTR connectIn,
            /* [out] */ BSTR *connectOut);
        
        HRESULT ( STDMETHODCALLTYPE *GetLogonUrl )( 
            ISocialSession * This,
            /* [retval][out] */ BSTR *url);
        
        HRESULT ( STDMETHODCALLTYPE *GetNetworkIdentifier )( 
            ISocialSession * This,
            /* [retval][out] */ BSTR *networkIdentifier);
        
        HRESULT ( STDMETHODCALLTYPE *GetLoggedOnUser )( 
            ISocialSession * This,
            /* [retval][out] */ ISocialProfile **user);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_LoggedOnUserID )( 
            ISocialSession * This,
            /* [retval][out] */ BSTR *userID);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_LoggedOnUserName )( 
            ISocialSession * This,
            /* [retval][out] */ BSTR *userName);
        
        HRESULT ( STDMETHODCALLTYPE *GetPerson )( 
            ISocialSession * This,
            /* [in] */ BSTR userID,
            /* [retval][out] */ ISocialPerson **person);
        
        HRESULT ( STDMETHODCALLTYPE *FindPerson )( 
            ISocialSession * This,
            /* [in] */ BSTR userID,
            /* [retval][out] */ BSTR *result);
        
        HRESULT ( STDMETHODCALLTYPE *GetActivities )( 
            ISocialSession * This,
            /* [in] */ SAFEARRAY * emailAddresses,
            /* [in] */ DATE startTime,
            /* [retval][out] */ BSTR *activities);
        
        HRESULT ( STDMETHODCALLTYPE *FollowPerson )( 
            ISocialSession * This,
            /* [in] */ BSTR emailAddress);
        
        HRESULT ( STDMETHODCALLTYPE *UnFollowPerson )( 
            ISocialSession * This,
            /* [in] */ BSTR userID);
        
        END_INTERFACE
    } ISocialSessionVtbl;

    interface ISocialSession
    {
        CONST_VTBL struct ISocialSessionVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISocialSession_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISocialSession_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISocialSession_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISocialSession_Logon(This,userName,password)	\
    ( (This)->lpVtbl -> Logon(This,userName,password) ) 

#define ISocialSession_put_SiteUrl(This,url)	\
    ( (This)->lpVtbl -> put_SiteUrl(This,url) ) 

#define ISocialSession_LogonWeb(This,connectIn,connectOut)	\
    ( (This)->lpVtbl -> LogonWeb(This,connectIn,connectOut) ) 

#define ISocialSession_GetLogonUrl(This,url)	\
    ( (This)->lpVtbl -> GetLogonUrl(This,url) ) 

#define ISocialSession_GetNetworkIdentifier(This,networkIdentifier)	\
    ( (This)->lpVtbl -> GetNetworkIdentifier(This,networkIdentifier) ) 

#define ISocialSession_GetLoggedOnUser(This,user)	\
    ( (This)->lpVtbl -> GetLoggedOnUser(This,user) ) 

#define ISocialSession_get_LoggedOnUserID(This,userID)	\
    ( (This)->lpVtbl -> get_LoggedOnUserID(This,userID) ) 

#define ISocialSession_get_LoggedOnUserName(This,userName)	\
    ( (This)->lpVtbl -> get_LoggedOnUserName(This,userName) ) 

#define ISocialSession_GetPerson(This,userID,person)	\
    ( (This)->lpVtbl -> GetPerson(This,userID,person) ) 

#define ISocialSession_FindPerson(This,userID,result)	\
    ( (This)->lpVtbl -> FindPerson(This,userID,result) ) 

#define ISocialSession_GetActivities(This,emailAddresses,startTime,activities)	\
    ( (This)->lpVtbl -> GetActivities(This,emailAddresses,startTime,activities) ) 

#define ISocialSession_FollowPerson(This,emailAddress)	\
    ( (This)->lpVtbl -> FollowPerson(This,emailAddress) ) 

#define ISocialSession_UnFollowPerson(This,userID)	\
    ( (This)->lpVtbl -> UnFollowPerson(This,userID) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISocialSession_INTERFACE_DEFINED__ */


#ifndef __ISocialPerson_INTERFACE_DEFINED__
#define __ISocialPerson_INTERFACE_DEFINED__

/* interface ISocialPerson */
/* [helpstring][unique][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_ISocialPerson;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("B252A037-5455-429d-A380-B5E9ADA3D423")
    ISocialPerson : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetActivities( 
            /* [in] */ DATE startTime,
            /* [retval][out] */ BSTR *activities) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetFriendsAndColleagues( 
            /* [retval][out] */ BSTR *personsCollection) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetFriendsAndColleaguesIDs( 
            /* [retval][out] */ SAFEARRAY * *friendsIDs) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetPicture( 
            /* [retval][out] */ SAFEARRAY * *picture) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetStatus( 
            /* [retval][out] */ BSTR *status) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetDetails( 
            /* [retval][out] */ BSTR *details) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISocialPersonVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISocialPerson * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISocialPerson * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISocialPerson * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetActivities )( 
            ISocialPerson * This,
            /* [in] */ DATE startTime,
            /* [retval][out] */ BSTR *activities);
        
        HRESULT ( STDMETHODCALLTYPE *GetFriendsAndColleagues )( 
            ISocialPerson * This,
            /* [retval][out] */ BSTR *personsCollection);
        
        HRESULT ( STDMETHODCALLTYPE *GetFriendsAndColleaguesIDs )( 
            ISocialPerson * This,
            /* [retval][out] */ SAFEARRAY * *friendsIDs);
        
        HRESULT ( STDMETHODCALLTYPE *GetPicture )( 
            ISocialPerson * This,
            /* [retval][out] */ SAFEARRAY * *picture);
        
        HRESULT ( STDMETHODCALLTYPE *GetStatus )( 
            ISocialPerson * This,
            /* [retval][out] */ BSTR *status);
        
        HRESULT ( STDMETHODCALLTYPE *GetDetails )( 
            ISocialPerson * This,
            /* [retval][out] */ BSTR *details);
        
        END_INTERFACE
    } ISocialPersonVtbl;

    interface ISocialPerson
    {
        CONST_VTBL struct ISocialPersonVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISocialPerson_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISocialPerson_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISocialPerson_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISocialPerson_GetActivities(This,startTime,activities)	\
    ( (This)->lpVtbl -> GetActivities(This,startTime,activities) ) 

#define ISocialPerson_GetFriendsAndColleagues(This,personsCollection)	\
    ( (This)->lpVtbl -> GetFriendsAndColleagues(This,personsCollection) ) 

#define ISocialPerson_GetFriendsAndColleaguesIDs(This,friendsIDs)	\
    ( (This)->lpVtbl -> GetFriendsAndColleaguesIDs(This,friendsIDs) ) 

#define ISocialPerson_GetPicture(This,picture)	\
    ( (This)->lpVtbl -> GetPicture(This,picture) ) 

#define ISocialPerson_GetStatus(This,status)	\
    ( (This)->lpVtbl -> GetStatus(This,status) ) 

#define ISocialPerson_GetDetails(This,details)	\
    ( (This)->lpVtbl -> GetDetails(This,details) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISocialPerson_INTERFACE_DEFINED__ */


#ifndef __ISocialProfile_INTERFACE_DEFINED__
#define __ISocialProfile_INTERFACE_DEFINED__

/* interface ISocialProfile */
/* [helpstring][unique][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_ISocialProfile;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("359702A5-52C8-4eda-B9D0-47D95174057A")
    ISocialProfile : public ISocialPerson
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE AreFriendsOrColleagues( 
            SAFEARRAY * userIDs,
            /* [retval][out] */ SAFEARRAY * *results) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetActivitiesOfFriendsAndColleagues( 
            /* [in] */ DATE startTime,
            /* [retval][out] */ BSTR *activitiesCollection) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SetStatus( 
            BSTR status) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISocialProfileVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISocialProfile * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISocialProfile * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISocialProfile * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetActivities )( 
            ISocialProfile * This,
            /* [in] */ DATE startTime,
            /* [retval][out] */ BSTR *activities);
        
        HRESULT ( STDMETHODCALLTYPE *GetFriendsAndColleagues )( 
            ISocialProfile * This,
            /* [retval][out] */ BSTR *personsCollection);
        
        HRESULT ( STDMETHODCALLTYPE *GetFriendsAndColleaguesIDs )( 
            ISocialProfile * This,
            /* [retval][out] */ SAFEARRAY * *friendsIDs);
        
        HRESULT ( STDMETHODCALLTYPE *GetPicture )( 
            ISocialProfile * This,
            /* [retval][out] */ SAFEARRAY * *picture);
        
        HRESULT ( STDMETHODCALLTYPE *GetStatus )( 
            ISocialProfile * This,
            /* [retval][out] */ BSTR *status);
        
        HRESULT ( STDMETHODCALLTYPE *GetDetails )( 
            ISocialProfile * This,
            /* [retval][out] */ BSTR *details);
        
        HRESULT ( STDMETHODCALLTYPE *AreFriendsOrColleagues )( 
            ISocialProfile * This,
            SAFEARRAY * userIDs,
            /* [retval][out] */ SAFEARRAY * *results);
        
        HRESULT ( STDMETHODCALLTYPE *GetActivitiesOfFriendsAndColleagues )( 
            ISocialProfile * This,
            /* [in] */ DATE startTime,
            /* [retval][out] */ BSTR *activitiesCollection);
        
        HRESULT ( STDMETHODCALLTYPE *SetStatus )( 
            ISocialProfile * This,
            BSTR status);
        
        END_INTERFACE
    } ISocialProfileVtbl;

    interface ISocialProfile
    {
        CONST_VTBL struct ISocialProfileVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISocialProfile_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISocialProfile_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISocialProfile_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISocialProfile_GetActivities(This,startTime,activities)	\
    ( (This)->lpVtbl -> GetActivities(This,startTime,activities) ) 

#define ISocialProfile_GetFriendsAndColleagues(This,personsCollection)	\
    ( (This)->lpVtbl -> GetFriendsAndColleagues(This,personsCollection) ) 

#define ISocialProfile_GetFriendsAndColleaguesIDs(This,friendsIDs)	\
    ( (This)->lpVtbl -> GetFriendsAndColleaguesIDs(This,friendsIDs) ) 

#define ISocialProfile_GetPicture(This,picture)	\
    ( (This)->lpVtbl -> GetPicture(This,picture) ) 

#define ISocialProfile_GetStatus(This,status)	\
    ( (This)->lpVtbl -> GetStatus(This,status) ) 

#define ISocialProfile_GetDetails(This,details)	\
    ( (This)->lpVtbl -> GetDetails(This,details) ) 


#define ISocialProfile_AreFriendsOrColleagues(This,userIDs,results)	\
    ( (This)->lpVtbl -> AreFriendsOrColleagues(This,userIDs,results) ) 

#define ISocialProfile_GetActivitiesOfFriendsAndColleagues(This,startTime,activitiesCollection)	\
    ( (This)->lpVtbl -> GetActivitiesOfFriendsAndColleagues(This,startTime,activitiesCollection) ) 

#define ISocialProfile_SetStatus(This,status)	\
    ( (This)->lpVtbl -> SetStatus(This,status) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISocialProfile_INTERFACE_DEFINED__ */


#ifndef __ISocialProvider_INTERFACE_DEFINED__
#define __ISocialProvider_INTERFACE_DEFINED__

/* interface ISocialProvider */
/* [helpstring][unique][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_ISocialProvider;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("90D17471-0E8E-415b-ADE0-8B414CB3B96E")
    ISocialProvider : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetCapabilities( 
            /* [retval][out] */ BSTR *capabilities) = 0;
        
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_Version( 
            /* [retval][out] */ BSTR *version) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetSession( 
            /* [retval][out] */ ISocialSession **session) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetAutoConfiguredSession( 
            /* [retval][out] */ ISocialSession **session) = 0;
        
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_DefaultSiteUrls( 
            /* [retval][out] */ SAFEARRAY * *siteURLs) = 0;
        
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_SocialNetworkIcon( 
            /* [retval][out] */ SAFEARRAY * *networkIcon) = 0;
        
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_SocialNetworkName( 
            /* [retval][out] */ BSTR *networkName) = 0;
        
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_SocialNetworkGuid( 
            /* [retval][out] */ GUID *guid) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetStatusSettings( 
            /* [out] */ BSTR *statusDefault,
            /* [out] */ INT *maxStatusLength) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Load( 
            /* [in] */ BSTR socialProviderInterfaceVersion,
            /* [in] */ BSTR languageTag) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISocialProviderVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISocialProvider * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISocialProvider * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISocialProvider * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetCapabilities )( 
            ISocialProvider * This,
            /* [retval][out] */ BSTR *capabilities);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_Version )( 
            ISocialProvider * This,
            /* [retval][out] */ BSTR *version);
        
        HRESULT ( STDMETHODCALLTYPE *GetSession )( 
            ISocialProvider * This,
            /* [retval][out] */ ISocialSession **session);
        
        HRESULT ( STDMETHODCALLTYPE *GetAutoConfiguredSession )( 
            ISocialProvider * This,
            /* [retval][out] */ ISocialSession **session);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_DefaultSiteUrls )( 
            ISocialProvider * This,
            /* [retval][out] */ SAFEARRAY * *siteURLs);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_SocialNetworkIcon )( 
            ISocialProvider * This,
            /* [retval][out] */ SAFEARRAY * *networkIcon);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_SocialNetworkName )( 
            ISocialProvider * This,
            /* [retval][out] */ BSTR *networkName);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_SocialNetworkGuid )( 
            ISocialProvider * This,
            /* [retval][out] */ GUID *guid);
        
        HRESULT ( STDMETHODCALLTYPE *GetStatusSettings )( 
            ISocialProvider * This,
            /* [out] */ BSTR *statusDefault,
            /* [out] */ INT *maxStatusLength);
        
        HRESULT ( STDMETHODCALLTYPE *Load )( 
            ISocialProvider * This,
            /* [in] */ BSTR socialProviderInterfaceVersion,
            /* [in] */ BSTR languageTag);
        
        END_INTERFACE
    } ISocialProviderVtbl;

    interface ISocialProvider
    {
        CONST_VTBL struct ISocialProviderVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISocialProvider_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISocialProvider_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISocialProvider_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISocialProvider_GetCapabilities(This,capabilities)	\
    ( (This)->lpVtbl -> GetCapabilities(This,capabilities) ) 

#define ISocialProvider_get_Version(This,version)	\
    ( (This)->lpVtbl -> get_Version(This,version) ) 

#define ISocialProvider_GetSession(This,session)	\
    ( (This)->lpVtbl -> GetSession(This,session) ) 

#define ISocialProvider_GetAutoConfiguredSession(This,session)	\
    ( (This)->lpVtbl -> GetAutoConfiguredSession(This,session) ) 

#define ISocialProvider_get_DefaultSiteUrls(This,siteURLs)	\
    ( (This)->lpVtbl -> get_DefaultSiteUrls(This,siteURLs) ) 

#define ISocialProvider_get_SocialNetworkIcon(This,networkIcon)	\
    ( (This)->lpVtbl -> get_SocialNetworkIcon(This,networkIcon) ) 

#define ISocialProvider_get_SocialNetworkName(This,networkName)	\
    ( (This)->lpVtbl -> get_SocialNetworkName(This,networkName) ) 

#define ISocialProvider_get_SocialNetworkGuid(This,guid)	\
    ( (This)->lpVtbl -> get_SocialNetworkGuid(This,guid) ) 

#define ISocialProvider_GetStatusSettings(This,statusDefault,maxStatusLength)	\
    ( (This)->lpVtbl -> GetStatusSettings(This,statusDefault,maxStatusLength) ) 

#define ISocialProvider_Load(This,socialProviderInterfaceVersion,languageTag)	\
    ( (This)->lpVtbl -> Load(This,socialProviderInterfaceVersion,languageTag) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISocialProvider_INTERFACE_DEFINED__ */


#ifndef __ISocialSession2_INTERFACE_DEFINED__
#define __ISocialSession2_INTERFACE_DEFINED__

/* interface ISocialSession2 */
/* [helpstring][unique][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_ISocialSession2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("33A54789-8C4E-4b56-8452-EE3E0DBB0372")
    ISocialSession2 : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetPeopleDetails( 
            /* [in] */ BSTR personsAddresses,
            /* [retval][out] */ BSTR *personsCollection) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetActivitiesEx( 
            /* [in] */ SAFEARRAY * hashedAddresses,
            /* [in] */ DATE startTime,
            /* [retval][out] */ BSTR *activities) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FollowPersonEx( 
            /* [in] */ SAFEARRAY * emailAddresses,
            /* [in] */ BSTR displayName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE LogonCached( 
            /* [in] */ BSTR connectIn,
            /* [in] */ BSTR userName,
            /* [in] */ BSTR password,
            /* [out] */ BSTR *connectOut) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISocialSession2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISocialSession2 * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISocialSession2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISocialSession2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetPeopleDetails )( 
            ISocialSession2 * This,
            /* [in] */ BSTR personsAddresses,
            /* [retval][out] */ BSTR *personsCollection);
        
        HRESULT ( STDMETHODCALLTYPE *GetActivitiesEx )( 
            ISocialSession2 * This,
            /* [in] */ SAFEARRAY * hashedAddresses,
            /* [in] */ DATE startTime,
            /* [retval][out] */ BSTR *activities);
        
        HRESULT ( STDMETHODCALLTYPE *FollowPersonEx )( 
            ISocialSession2 * This,
            /* [in] */ SAFEARRAY * emailAddresses,
            /* [in] */ BSTR displayName);
        
        HRESULT ( STDMETHODCALLTYPE *LogonCached )( 
            ISocialSession2 * This,
            /* [in] */ BSTR connectIn,
            /* [in] */ BSTR userName,
            /* [in] */ BSTR password,
            /* [out] */ BSTR *connectOut);
        
        END_INTERFACE
    } ISocialSession2Vtbl;

    interface ISocialSession2
    {
        CONST_VTBL struct ISocialSession2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISocialSession2_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISocialSession2_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISocialSession2_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISocialSession2_GetPeopleDetails(This,personsAddresses,personsCollection)	\
    ( (This)->lpVtbl -> GetPeopleDetails(This,personsAddresses,personsCollection) ) 

#define ISocialSession2_GetActivitiesEx(This,hashedAddresses,startTime,activities)	\
    ( (This)->lpVtbl -> GetActivitiesEx(This,hashedAddresses,startTime,activities) ) 

#define ISocialSession2_FollowPersonEx(This,emailAddresses,displayName)	\
    ( (This)->lpVtbl -> FollowPersonEx(This,emailAddresses,displayName) ) 

#define ISocialSession2_LogonCached(This,connectIn,userName,password,connectOut)	\
    ( (This)->lpVtbl -> LogonCached(This,connectIn,userName,password,connectOut) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISocialSession2_INTERFACE_DEFINED__ */

#endif /* __OutlookSocialProvider_LIBRARY_DEFINED__ */

/* interface __MIDL_itf_socialprovider_0001_0068 */
/* [local] */ 

#define DeclareISocialPersonMembers(IPURE) \
	STDMETHOD(GetActivities) (THIS_ DATE startTime, BSTR *Activities) IPURE;\
	STDMETHOD(GetFriendsAndColleagues) (THIS_ BSTR *PersonsCollection) IPURE;\
	STDMETHOD(GetFriendsAndColleaguesIDs) (THIS_ SAFEARRAY **FriendIDs) IPURE;\
	STDMETHOD(GetPicture) (THIS_ SAFEARRAY **ppvarPicture) IPURE;\
	STDMETHOD(GetStatus) (THIS_ BSTR * Status) IPURE;\
	STDMETHOD(GetDetails) (THIS_ BSTR *Details) IPURE;\
 



extern RPC_IF_HANDLE __MIDL_itf_socialprovider_0001_0068_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_socialprovider_0001_0068_v0_0_s_ifspec;

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


