// ***************************************************************
// <copyright file="Service.cpp" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// Entry point for the service executable
// </summary>
// ***************************************************************

#include "stdafx.h"
#include "resource.h"
#include "Service.h"

// This implementation of AtlServiceModule allows the COM server to be called
// by the network service account, which E12 runs under.
class CServiceModule : public CAtlServiceModuleT< CServiceModule, IDS_SERVICENAME >
{
private:
    typedef CAtlServiceModuleT< CServiceModule, IDS_SERVICENAME > BaseClass;

public :
	DECLARE_LIBID(LIBID_AntivirusLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_Service, "{270156EC-3866-454b-A6E5-903A4C6ABEA3}")

	HRESULT InitializeSecurity() throw()
	{
		return CoInitializeSecurity(NULL, -1, NULL, NULL, RPC_C_AUTHN_LEVEL_PKT, RPC_C_IMP_LEVEL_IMPERSONATE, NULL, EOAC_NONE, NULL);
	}

	// Set launch permissions for this service
    HRESULT RegisterAppId(bool bService = false)
    {
        // Call base class to do its usual stuff
        HRESULT hr = BaseClass::RegisterAppId(bService);
        if (FAILED(hr))
        {
            return hr;
        }

        // Open registry key and set launch permission
        CRegKey keyAppID;
        LONG lRet;
        if ( (lRet = keyAppID.Open(HKEY_CLASSES_ROOT, _T("AppID"), KEY_WRITE)) == ERROR_SUCCESS)
        {
            CRegKey keyThisAppID;
            lRet = keyThisAppID.Open(keyAppID, GetAppIdT());
            if (lRet != ERROR_SUCCESS)
                return AtlHresultFromWin32(lRet);

            hr = SetLaunchPermission(keyThisAppID);
        }
        else
        {
            hr = HRESULT_FROM_WIN32(lRet);
        }
        return hr;
    }

private:

	// Set launch permission
    static HRESULT SetLaunchPermission( CRegKey appIDKey )
    {
        HRESULT                 hr = NOERROR;
        LONG                    lRes;
        SECURITY_DESCRIPTOR     sdNew = {0};
        PSECURITY_DESCRIPTOR    psdCur = NULL;
        PACL                    pAclNew;
        DWORD                   cbsdNew = 0;
        DWORD                   ec = 0;
        BYTE                    aMemory[256];
        TOKEN_USER              *pTokenUser = (TOKEN_USER *) &aMemory;
        HANDLE                  hToken = NULL;
        DWORD                   lIgnore;
        DWORD                   lSidLen;
        SID                     *pSid = NULL;
        SID                     *pGroup = NULL;
        SID                     *pOwner = NULL;

        // Initialize a new ACL and a new security descriptor 
        // Set the security descriptor to this new empty ACL...
        // (Empty ACL means no permissions for anyone)..
        pAclNew = (PACL)new BYTE[sizeof(ACL)];
        if ( !InitializeAcl(pAclNew, sizeof(ACL), ACL_REVISION ))
        {
            ec = GetLastError();
			hr = HRESULT_FROM_WIN32(ec);
			goto Exit;
        }
            
        if ( !InitializeSecurityDescriptor(&sdNew, SECURITY_DESCRIPTOR_REVISION) )
        {
            ec = GetLastError();
			hr = HRESULT_FROM_WIN32(ec);
			goto Exit;
        }

        // Get the pSid of the current process.. We need to use this to set the 
        // owner and group information of the Security Descriptor.
        if ( !OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, &hToken) )
        {
            ec = GetLastError();
			hr = HRESULT_FROM_WIN32(ec);
			goto Exit;
        }

        // Lookup SID of process token.
        if ( !GetTokenInformation(hToken, TokenUser, pTokenUser,
                                     sizeof(aMemory), &lIgnore ) )
        {
            ec = GetLastError();
			hr = HRESULT_FROM_WIN32(ec);
			goto Exit;
        }

        // Allocate memory to hold the SID.
        lSidLen = GetLengthSid( pTokenUser->User.Sid );
        pSid = (SID *) malloc(lSidLen);
		if (!pSid)
		{
			hr = HRESULT_FROM_WIN32(E_OUTOFMEMORY);
			goto Exit;
		}

        memcpy(pSid, pTokenUser->User.Sid, lSidLen);
        
        CloseHandle( hToken );
        hToken = NULL;	

        // Allocate memory for the Owner and Group Sids
        pGroup = (SID *) malloc(lSidLen);
		if (!pGroup)
		{
			hr = HRESULT_FROM_WIN32(E_OUTOFMEMORY);
			goto Exit;
		}
        
        pOwner = (SID *) malloc(lSidLen);
		if (!pOwner)
		{
			hr = HRESULT_FROM_WIN32(E_OUTOFMEMORY);
			goto Exit;
		}
        
        // Copy the pSid of the current process into the two SID structures 
        // to be used as Owner and Group of the Security Descriptor
        memcpy(pOwner, pSid, lSidLen);
        memcpy(pGroup, pSid, lSidLen);
        
        // Set the Group and Owner fields in the Security Descriptor
        if ( !SetSecurityDescriptorGroup( &sdNew, pGroup, FALSE) )
        {
            ec = GetLastError();
			hr = HRESULT_FROM_WIN32(ec);
			goto Exit;
        }

        if ( !SetSecurityDescriptorOwner( &sdNew, pOwner, FALSE) )
        {
            ec = GetLastError();
			hr = HRESULT_FROM_WIN32(ec);
			goto Exit;
        }

        // allow NETWORK SERVICE, SYSTEM, INTERACTIVE 
        AddAccessAllowedACEToACL (&pAclNew, COM_RIGHTS_EXECUTE, _T("SYSTEM"));
        AddAccessAllowedACEToACL (&pAclNew, COM_RIGHTS_EXECUTE, _T("INTERACTIVE"));
        AddAccessAllowedACEToACL (&pAclNew, COM_RIGHTS_EXECUTE, _T("NT AUTHORITY\\NETWORKSERVICE"));
        AddAccessAllowedACEToACL (&pAclNew, COM_RIGHTS_EXECUTE, _T("Administrators"));

        // Set the discretionary ACL on the security descriptor
        if ( !SetSecurityDescriptorDacl(&sdNew, TRUE, pAclNew, FALSE) )
        {
            ec = GetLastError();
			hr = HRESULT_FROM_WIN32(ec);
			goto Exit;
        }

        // This call HAS to fail the first time giving the size of the SD...
        if ( !MakeSelfRelativeSD(&sdNew, psdCur, &cbsdNew ) )
        {
            ec = GetLastError();
            if ( ec != ERROR_INSUFFICIENT_BUFFER )
			{
				hr = HRESULT_FROM_WIN32(ec);
				goto Exit;
			}
        }
        
        // Allocate the SD which will be in self relative form
        psdCur = (PSECURITY_DESCRIPTOR) malloc(cbsdNew);
		if (!psdCur)
		{
			hr = HRESULT_FROM_WIN32(E_OUTOFMEMORY);
			goto Exit;
		}
        
        // Store the Security Descriptor with no permissions for anyone in the new SD
        // which is in self relative format...
        if ( !MakeSelfRelativeSD(&sdNew, psdCur, &cbsdNew) )
        {
            ec = GetLastError();
			hr = HRESULT_FROM_WIN32(ec);
			goto Exit;
        }

        // Set the regkey which refuses LaunchPermission to everyone...
        lRes = appIDKey.SetValue(_T("LaunchPermission"), REG_BINARY, (BYTE *) psdCur, cbsdNew);
        if ( lRes != ERROR_SUCCESS )
		{
			hr = HRESULT_FROM_WIN32(lRes);
			goto Exit;
		}

    Exit:

        if (hToken != NULL )
        {
            CloseHandle(hToken);
        }

        // MakeSelfRelativeSD will create new mem, and copy the contents of these, so release these 4
        if (pSid != NULL)
        {
           free(pSid);
        }
        if (pGroup != NULL)
        {
           free(pGroup);
        }
        if (pOwner != NULL)
        {
           free(pOwner);
        }
        if (psdCur != NULL)
        {
           free(psdCur);
        }
        return hr;	
    }

	// Add the given principal to the given ACL
    static DWORD AddAccessAllowedACEToACL (
        PACL *ppAcl,
        DWORD permissionMask,
        LPTSTR strPrincipal)
    {
        ACL_SIZE_INFORMATION  aclSizeInfo;
        int                   nAclSize;
        DWORD                 dwRetVal;
        PSID                  pPrincipalSID;
        PACL                  pOldAcl, pNewACL;

        pOldAcl = *ppAcl;

        dwRetVal = GetPrincipalSID (strPrincipal, &pPrincipalSID);
        if (dwRetVal != ERROR_SUCCESS)
            return dwRetVal;

        GetAclInformation (pOldAcl, (LPVOID) &aclSizeInfo, (DWORD) sizeof (ACL_SIZE_INFORMATION), AclSizeInformation);
        nAclSize = aclSizeInfo.AclBytesInUse +
                  sizeof (ACL) + sizeof (ACCESS_ALLOWED_ACE) +
                  GetLengthSid (pPrincipalSID) - sizeof (DWORD);

        pNewACL = (PACL) new BYTE [nAclSize];
        if (!InitializeAcl (pNewACL, nAclSize, ACL_REVISION))
        {
            free (pPrincipalSID);
            return GetLastError();
        }

        dwRetVal = CopyACL(pOldAcl, pNewACL);
        if (dwRetVal != ERROR_SUCCESS)
        {
            free (pPrincipalSID);
            return dwRetVal;
        }

        if (!AddAccessAllowedAce (pNewACL, ACL_REVISION2, permissionMask, pPrincipalSID))
        {
            free (pPrincipalSID);
            return GetLastError();
        }

        *ppAcl = pNewACL;

        free (pPrincipalSID);
        return ERROR_SUCCESS;
    }

	// Get the SID for the given principal
    static DWORD GetPrincipalSID (
        LPTSTR strPrincipal,
        PSID *pSid)
    {
        DWORD        sidSize;
        TCHAR        refDomain [256];
        DWORD        refDomainSize;
        DWORD        dwRetVal;
        SID_NAME_USE snu;

        sidSize = 0;
        refDomainSize = 255;

        LookupAccountName (NULL,
                           strPrincipal,
                           *pSid,
                           &sidSize,
                           refDomain,
                           &refDomainSize,
                           &snu);

        dwRetVal = GetLastError();
        if (dwRetVal != ERROR_INSUFFICIENT_BUFFER)
            return dwRetVal;

        *pSid = (PSID) malloc (sidSize);
        refDomainSize = 255;

        if (!LookupAccountName (NULL,
                                strPrincipal,
                                *pSid,
                                &sidSize,
                                refDomain,
                                &refDomainSize,
                                &snu))
        {
            return GetLastError();
        }

        return ERROR_SUCCESS;
    }

	// Copy ACL
    static DWORD CopyACL (
        PACL pOldAcl,
        PACL pNewAcl)
    {
        ACL_SIZE_INFORMATION  aclSizeInfo;
        LPVOID                pAce;
        ACE_HEADER            *pAceHeader;
        ULONG                 i;

        GetAclInformation (pOldAcl, (LPVOID) &aclSizeInfo, (DWORD) sizeof (aclSizeInfo), AclSizeInformation);

        // Copy all of the ACEs to the new ACL
        for (i = 0; i < aclSizeInfo.AceCount; i++)
        {
            // Get the ACE and header info
            if (!GetAce (pOldAcl, i, &pAce))
                return GetLastError();

            pAceHeader = (ACE_HEADER *) pAce;

            // Add the ACE to the new list
            if (!AddAce (pNewAcl, ACL_REVISION, 0xffffffff, pAce, pAceHeader->AceSize))
                return GetLastError();
        }
        return ERROR_SUCCESS;
    }
};

CServiceModule _AtlModule;

// Windows entry point
extern "C" int WINAPI _tWinMain(
    HINSTANCE /*hInstance*/, 
    HINSTANCE /*hPrevInstance*/, 
    LPTSTR  /*lpCmdLine*/, 
    int nShowCmd)
{
    return _AtlModule.WinMain(nShowCmd);
}
