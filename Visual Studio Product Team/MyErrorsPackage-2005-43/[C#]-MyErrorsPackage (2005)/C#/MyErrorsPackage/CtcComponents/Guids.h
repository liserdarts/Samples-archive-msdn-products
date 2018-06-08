// Guids.h
//

// do not use #pragma once - used by ctc compiler
#ifndef __GUIDS_H_
#define __GUIDS_H_

#ifndef _CTC_GUIDS_


// guidPersistanceSlot ID for our Tool Window
// {7383db3b-ab1f-440b-a9a7-f97020227f83}
DEFINE_GUID(GUID_guidPersistanceSlot, 
0x7383DB3B, 0xAB1F, 0x440B, 0xA9, 0xA7, 0xF9, 0x70, 0x20, 0x22, 0x7F, 0x83);

#define guidMyErrorsPackagePkg   CLSID_MyErrorsPackagePackage

// Command set guid for our commands (used with IOleCommandTarget)
// {c9456b17-f6bc-4229-8f67-878e6e35b6e0}
DEFINE_GUID(guidMyErrorsPackageCmdSet, 
0xC9456B17, 0xF6BC, 0x4229, 0x8F, 0x67, 0x87, 0x8E, 0x6E, 0x35, 0xB6, 0xE0);

#else  // _CTC_GUIDS

#define guidMyErrorsPackagePkg      { 0x968CEB5D, 0x9859, 0x4D47, { 0xB6, 0xB0, 0xD5, 0x7E, 0xA8, 0xC1, 0x84, 0xF2 } }
#define guidMyErrorsPackageCmdSet	  { 0xC9456B17, 0xF6BC, 0x4229, { 0x8F, 0x67, 0x87, 0x8E, 0x6E, 0x35, 0xB6, 0xE0 } }


#endif // _CTC_GUIDS_

#endif // __GUIDS_H_
