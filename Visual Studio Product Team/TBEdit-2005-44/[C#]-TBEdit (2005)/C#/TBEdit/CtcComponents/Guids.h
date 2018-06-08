// Guids.h
//

// do not use #pragma once - used by ctc compiler
#ifndef __GUIDS_H_
#define __GUIDS_H_

#ifndef _CTC_GUIDS_


#define guidTBEditPkg   CLSID_TBEditPackage

// Command set guid for our commands (used with IOleCommandTarget)
// {ae1509fc-74e6-481b-bc78-24ac64017e51}
DEFINE_GUID(guidTBEditCmdSet, 
0xAE1509FC, 0x74E6, 0x481B, 0xBC, 0x78, 0x24, 0xAC, 0x64, 0x1, 0x7E, 0x51);

#else  // _CTC_GUIDS

#define guidTBEditPkg      { 0x3555BBE4, 0xAC8F, 0x4B43, { 0x96, 0x48, 0x47, 0x5D, 0xEC, 0xF6, 0x74, 0x8 } }
#define guidTBEditCmdSet	  { 0xAE1509FC, 0x74E6, 0x481B, { 0xBC, 0x78, 0x24, 0xAC, 0x64, 0x1, 0x7E, 0x51 } }


#endif // _CTC_GUIDS_

#endif // __GUIDS_H_
