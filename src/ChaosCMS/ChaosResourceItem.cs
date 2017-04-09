using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    public class ChaosResourceItem : IFileInfo
    {
        private readonly Stream stream;

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="name"></param>
        public ChaosResourceItem(Stream stream, string name)
        {
            this.stream = stream;
            this.Name = name;
            this.Exists = true;
            this.IsDirectory = false;
            this.LastModified = DateTimeOffset.Now;
            this.Length = this.stream.Length;
        }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Stream CreateReadStream()
        {
            return this.stream;
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
        public DateTimeOffset LastModified { get; }

        /// <summary>
        ///
        /// </summary>
        public bool IsDirectory { get; }
    }
}