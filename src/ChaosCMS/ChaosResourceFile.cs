using System.IO;

using Microsoft.Extensions.FileProviders;
using System;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaosResourceFile : IFileInfo
    {
        private readonly Stream manifestResourceStream;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="manifestResourceStream"></param>
        public ChaosResourceFile(string virtualPath, Stream manifestResourceStream)
        {
            this.Exists = true;
            this.Length = manifestResourceStream.Length;
            this.IsDirectory = false;
            this.Name = virtualPath;
            this.LastModified = DateTimeOffset.Now;
            this.manifestResourceStream = manifestResourceStream;
        }
        
        #region Implementation of IFileInfo

        public Stream CreateReadStream()
        {
            return this.manifestResourceStream;
        }

        public bool Exists { get; }

        public long Length { get; }

        public string PhysicalPath { get; }

        public string Name { get; }

        public DateTimeOffset LastModified { get; }

        public bool IsDirectory { get; }

        #endregion
    }
}
