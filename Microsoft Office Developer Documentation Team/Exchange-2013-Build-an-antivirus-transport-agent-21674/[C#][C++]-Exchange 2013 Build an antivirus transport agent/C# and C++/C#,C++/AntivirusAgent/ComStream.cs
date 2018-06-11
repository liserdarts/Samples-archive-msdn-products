// ***************************************************************
// <copyright file="ComStream.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// Implements the COM IStream interface over a .NET stream.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Antivirus
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    /// <summary>
    /// Implements the COM IStream interface over a .NET stream.
    /// </summary>
    class ComStream : ComInterop.IStream
    {
        /// <summary>
        /// The underlying .NET stream.
        /// </summary>
        private Stream stream;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stream">The .NET stream.</param>
        public ComStream(Stream stream)
        {
            this.stream = stream;
        }

        #region IStream Members

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="ppstm">Not implemented.</param>
        public void Clone(out ComInterop.IStream ppstm)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="grfCommitFlags">Not implemented.</param>
        public void Commit(uint grfCommitFlags)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="libOffset">Not implemented.</param>
        /// <param name="cb">Not implemented.</param>
        /// <param name="dwLockType">Not implemented.</param>
        public void LockRegion(ComInterop._ULARGE_INTEGER libOffset, ComInterop._ULARGE_INTEGER cb, uint dwLockType)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="pstm">Not implemented.</param>
        /// <param name="cb">Not implemented.</param>
        /// <param name="pcbRead">Not implemented.</param>
        /// <param name="pcbWritten">Not implemented.</param>
        public void RemoteCopyTo(ComInterop.IStream pstm, ComInterop._ULARGE_INTEGER cb, out ComInterop._ULARGE_INTEGER pcbRead, out ComInterop._ULARGE_INTEGER pcbWritten)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Reads from the stream.
        /// </summary>
        /// <param name="pv">The buffer to fill with data from the stream.</param>
        /// <param name="cb">The number of bytes to read.</param>
        /// <param name="pcbRead">The number of bytes actually read.</param>
        public void RemoteRead(out byte pv, uint cb, out uint pcbRead)
        {
            unsafe
            {
                int bytesToRead = (int)Math.Min((uint)cb, (uint)int.MaxValue);
                byte[] buffer = new byte[bytesToRead];
                int bytesRead = this.stream.Read(buffer, 0, bytesToRead);
                fixed (byte* bufferPointer = &pv)
                {
                    Marshal.Copy(buffer, 0, (IntPtr)bufferPointer, bytesRead);
                }
                fixed (uint* resultPointer = &pcbRead)
                {
                    *resultPointer = (uint)bytesRead;
                }
            }
        }

        /// <summary>
        /// Seeks within the stream.
        /// </summary>
        /// <param name="dlibMove">The number of bytes to move.</param>
        /// <param name="dwOrigin">Seek origin.</param>
        /// <param name="plibNewPosition">The position after seeking.</param>
        public void RemoteSeek(ComInterop._LARGE_INTEGER dlibMove, uint dwOrigin, out ComInterop._ULARGE_INTEGER plibNewPosition)
        {
            switch (dwOrigin)
            {
                case 0:
                    this.stream.Seek(dlibMove.QuadPart, SeekOrigin.Begin);
                    break;

                case 1:
                    this.stream.Seek(dlibMove.QuadPart, SeekOrigin.Current);
                    break;

                case 2:
                    this.stream.Seek(dlibMove.QuadPart, SeekOrigin.End);
                    break;
            }

            plibNewPosition.QuadPart = (ulong)this.stream.Position;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="pv">Not implemented.</param>
        /// <param name="cb">Not implemented.</param>
        /// <param name="pcbWritten">Not implemented.</param>
        public void RemoteWrite(ref byte pv, uint cb, out uint pcbWritten)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void Revert()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="libNewSize">Not implemented.</param>
        public void SetSize(ComInterop._ULARGE_INTEGER libNewSize)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Partially implemented - returns only the size of the stream.
        /// </summary>
        /// <param name="pstatstg">A pointer to stream state information.</param>
        /// <param name="grfStatFlag">Not implemented.</param>
        public void Stat(out ComInterop.tagSTATSTG pstatstg, uint grfStatFlag)
        {
            ComInterop.tagSTATSTG result = new ComInterop.tagSTATSTG();
            result.cbSize.QuadPart = (ulong)this.stream.Length;
            pstatstg = result;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="libOffset">Not implemented.</param>
        /// <param name="cb">Not implemented.</param>
        /// <param name="dwLockType">Not implemented.</param>
        public void UnlockRegion(ComInterop._ULARGE_INTEGER libOffset, ComInterop._ULARGE_INTEGER cb, uint dwLockType)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        #endregion
    }

#if UseSystemComTypes
    class StreamWrapper : System.Runtime.InteropServices.ComTypes.IStream
    {
        private Stream stream;

        public StreamWrapper(Stream stream)
        {
            this.stream = stream;
        }

    #region IStream Members


        public void Commit(int grfCommitFlags)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            int read = this.stream.Read(pv, 0, cb);
            unsafe
            {
                int* ptr = (int*)pcbRead;
                *ptr = read;
            }
        }

        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            switch (dwOrigin)
            {
                case 0:
                    this.stream.Seek(dlibMove, SeekOrigin.Begin);
                    break;

                case 1:
                    this.stream.Seek(dlibMove, SeekOrigin.Current);
                    break;

                case 2:
                    this.stream.Seek(dlibMove, SeekOrigin.End);
                    break;
            }

            unsafe
            {
                long* ptr = (long*) plibNewPosition;
                *ptr = this.stream.Position;
            }
        }

        public void SetSize(long libNewSize)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
        {
            System.Runtime.InteropServices.ComTypes.STATSTG result = new System.Runtime.InteropServices.ComTypes.STATSTG();
            pstatstg = result;
        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            this.stream.Write(pv, 0, cb);
            unsafe
            {
                int* ptr = (int*)pcbWritten;
                *ptr = cb;
            }
        }

        public void Clone(out IStream ppstm)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public void Revert()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

    #endregion
    }
#endif
}
