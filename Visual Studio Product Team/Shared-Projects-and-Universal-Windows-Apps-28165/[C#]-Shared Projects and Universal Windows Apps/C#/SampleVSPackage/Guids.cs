// Guids.cs
// MUST match guids.h
using System;

namespace Company.SampleVSPackage
{
    static class GuidList
    {
        public const string guidSampleVSPackagePkgString = "a42d0857-f33c-49dd-b86a-154a7b40ccda";
        public const string guidSampleVSPackageCmdSetString = "1d59512f-57ce-4445-8a0d-0be0137bf8aa";

        public static readonly Guid guidSampleVSPackageCmdSet = new Guid(guidSampleVSPackageCmdSetString);
    };
}