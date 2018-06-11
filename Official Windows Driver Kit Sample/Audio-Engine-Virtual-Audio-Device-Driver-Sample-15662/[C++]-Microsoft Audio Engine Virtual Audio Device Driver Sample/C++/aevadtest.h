//--------------------------------------------------------
// 
// Common header for all test cases
//
//
// Input buffer header - shared by all test cases
//
typedef struct _tag_AEVADTEST_INPUT_HEADER
{
    DWORD version;
    DWORD pinId;
    GUID  testCaseSetId;
    DWORD testCaseId;
    DWORD dwInputBufferSize;    // Number of bytes of input buffer following header 
    DWORD dwReserved[4];        // for feature extension
} AEVADTEST_CASE_INPUT_HEADER;

//
// Output buffer header - shared by all test cases
//
typedef struct _tag_AEVADTEST_CASE_OUTPUT_HEADER
{
    DWORD status;               // Result of KSProperty call 
    ULONGLONG timeStampBegin;
    ULONGLONG timeStampEnd;
    DWORD dwOutputBufferSize;   // Number of bytes of output buffer following inputbuffer
} AEVADTEST_CASE_OUTPUT_HEADER;

#ifdef USERMODE

typedef unsigned char BYTE;
typedef unsigned char UCHAR;
typedef unsigned short WORD;
typedef unsigned short USHORT;
typedef unsigned long ULONG;

#define STATUS_CODE_INIT_VALUE  0xffffffff
#define VERSION_NUMBER          0x00010000
#define INIT_TESTCASE_INPUT_HEADER(inputHeader, TestCaseSetId, TestCaseId)\
        inputHeader.version = VERSION_NUMBER;                             \
        inputHeader.testCaseSetId = TestCaseSetId;                        \
        inputHeader.testCaseId = TestCaseId;                              

#define INIT_TESTCASE_OUTPUT_HEADER(outputHeader)                       \
        outputHeader.status = STATUS_CODE_INIT_VALUE;                   \
        outputHeader.timeStampBegin = 0;                                \
        outputHeader.timeStampEnd = 0;                                   
#endif // end of #ifdef USERMODE

//
// {BC14A101-3537-43b6-9E2E-F0CF32D755E9}
DEFINE_GUID(KSPROPSETID_AEVAD, 
0xbc14a101, 0x3537, 0x43b6, 0x9e, 0x2e, 0xf0, 0xcf, 0x32, 0xd7, 0x55, 0xe9);

typedef enum {
    KSPROPERTY_AEVAD_TESTCASE
} KSPROPERTY_AEVAD;