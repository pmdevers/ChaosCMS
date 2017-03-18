using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaosResourceItem : IFileInfo
    {
        private readonly Stream stream;

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

        #region Implementation of IFileInfo

        public Stream CreateReadStream()
        {
            return this.stream;
        }

        public bool Exists { get; }

        public long Length { get; }

        public string PhysicalPath { get; }

        public DateTimeOffset LastModified { get; }

        public bool IsDirectory { get; }

        #endregion
    }
}
