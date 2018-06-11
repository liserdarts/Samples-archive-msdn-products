// ***************************************************************
// <copyright file="DotNetStream.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// Implements the .NET Stream abstract class interface over a COM IStream.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Antivirus
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Implements the .NET Stream abstract class interface over a COM IStream.
    /// </summary>
    class DotNetStream : Stream
    {
        /// <summary>
        /// The underlying COM stream.
        /// </summary>
        private ComInterop.IStream stream;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stream">COM IStream interface.</param>
        public DotNetStream(ComInterop.IStream stream)
        {
            this.stream = stream;
        }

        /// <summary>
        /// True.
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// False.
        /// </summary>
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <summary>
        /// False.
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// No-op.
        /// </summary>
        public override void Flush()
        {
            return;
        }

        /// <summary>
        /// The length of the stream.
        /// </summary>
        public override long Length
        {
            get
            {
                ComInterop.tagSTATSTG stat;
                stream.Stat(out stat, 0);
                return (long)stat.cbSize.QuadPart;
            }
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public override long Position
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        /// <summary>
        /// Reads from the stream.
        /// </summary>
        /// <param name="buffer">The buffer for data from the stream.</param>
        /// <param name="offset">Must be zero.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The 'offset' usually indicates the position in the 
        /// buffer to which data should be copied, but it is not supported in
        /// this implementation of Stream.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (offset != 0)
            {
                throw new NotImplementedException("DotNetStream.Read does not support nonzero offset");
            }
            unsafe
            {
                fixed (byte* fixedBuffer = buffer)
                {
                    uint bytesRead = 0;
                    this.stream.RemoteRead(out fixedBuffer[0], (uint)count, out bytesRead);
                    return (int)bytesRead;
                }
            }
        }

        /// <summary>
        /// Seek to a given position in the stream.
        /// </summary>
        /// <param name="offset">The number of bytes to move the file pointer.</param>
        /// <param name="origin">The origin to move the file pointer from.</param>
        /// <returns>The current file pointer position.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            ComInterop._ULARGE_INTEGER result;
            ComInterop._LARGE_INTEGER largeInt;
            largeInt.QuadPart = offset;
            stream.RemoteSeek(largeInt, (uint)origin, out result);
            return (long)result.QuadPart;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">Not implemented.</param>
        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="buffer">Not implemented.</param>
        /// <param name="offset">Not implemented.</param>
        /// <param name="count">Not implemented.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
