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
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public Stream CreateReadStream()
        {
            return this.manifestResourceStream;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Exists { get; }

        /// <summary>
        /// 
        /// </summary>
        public long Length { get; }
        /// <summary>
        /// 
        /// </summary>
        public string PhysicalPath { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset LastModified { get; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDirectory { get; }
    }
}
