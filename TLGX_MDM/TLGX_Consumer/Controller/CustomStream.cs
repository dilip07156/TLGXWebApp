using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TLGX_Consumer.Controller
{
    public class CustomStream : Stream
    {
        private readonly FileStream _file;
        private readonly long _length;
        private long _bytesRead;

        public class ProgressChangedEventArgs : EventArgs
        {
            public long BytesRead;
            public long Length;

            public ProgressChangedEventArgs(long bytesRead, long length)
            {
                BytesRead = bytesRead;
                Length = length;
            }
        }

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public CustomStream(FileStream fileStream)
        {
            _file = fileStream;
            _length = _file.Length;
            _bytesRead = 0;
            if (ProgressChanged != null)
            {
                ProgressChanged(this,
                    new ProgressChangedEventArgs(_bytesRead, _length));
            }
        }

        public double GetProgress()
        {
            return ((double)_bytesRead) / _file.Length;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush() { }

        public override long Length
        {
            get
            {
                return _length;
            }
        }

        public override long Position
        {
            get { return _bytesRead; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int result = _file.Read(buffer, offset, count);
            _bytesRead += result;
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressChangedEventArgs(_bytesRead, _length));
            }
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}