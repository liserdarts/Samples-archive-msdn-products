#pragma once

#define delay_us(t)                     NdisStallExecution(t)
#define delay_ms(t)                     NdisStallExecution((t)*1000)
#define delay(t)                        delay_ms(t)

NTSTATUS 
HwReadMacSieRegister(
    _In_ PNIC Nic,
    _In_ BYTE Request, 
    _In_ USHORT Value,
    _In_ ULONG Length,
    _Out_ PVOID Buffer
    );

NTSTATUS 
HwWriteMacSieRegister(
    _In_ PNIC Nic,
    _In_ BYTE Request, 
    _In_ USHORT Value,
    _In_ ULONG Length,
    _In_ PVOID Buffer ,
    _In_ USHORT Index   
    );

UCHAR
HwPlatformIORead1Byte(
    PNIC Nic,
    ULONG      offset
    );

USHORT
HwPlatformIORead2Byte(
    PNIC Nic,
    ULONG      offset
    );

ULONG
HwPlatformIORead4Byte(
    PNIC Nic,
    ULONG      offset
    );


VOID
HwPlatformIOWrite1Byte(
    _In_ PNIC     Nic,
    ULONG       Offset,
    UCHAR       ParamData
    )  ;

VOID
HwPlatformIOWrite2Byte(
    _In_ PNIC     Nic,
    ULONG       Offset,
    USHORT      ParamData
    ) ;

VOID
HwPlatformIOWrite4Byte(
    _In_ PNIC     Nic,
    ULONG       Offset,
    ULONG       ParamData
    ) ;

//
//  write macros
//  
#define HwPlatformEFIOWrite1Byte(_a,_b,_c)        \
            HwPlatformIOWrite1Byte(_a,_b,_c)
            
#define HwPlatformEFIOWrite2Byte(_a,_b,_c)        \
            HwPlatformIOWrite2Byte(_a,_b,(_c))
            
#define HwPlatformEFIOWrite4Byte(_a,_b,_c)        \
            HwPlatformIOWrite4Byte(_a,_b,(_c))


//
//  read macros
//  
#define HwPlatformEFIORead1Byte(_a,_b)    \
            HwPlatformIORead1Byte(_a,  _b)
#define HwPlatformEFIORead2Byte(_a,_b)    \
            HwPlatformIORead2Byte(_a,  _b) 
#define HwPlatformEFIORead4Byte(_a,_b)        \
            HwPlatformIORead4Byte(_a,  _b)
          
