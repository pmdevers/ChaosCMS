using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChaosCMS.Managers;

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace ChaosCMS
{
    public class ChaosFileProvider : IFileProvider
    {
        private readonly ResourceManager resourceManager;

        public ChaosFileProvider(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        #region Implementation of IFileProvider

        public IFileInfo GetFileInfo(string subpath)
        {
            return this.resourceManager.FindByPath(subpath.TrimStart('/'));
        }

        #endregion

        #region Implementation of IFileProvider

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IFileProvider

        public IChangeToken Watch(string filter)
        {
            return null;
        }

        #endregion

        public class NotChangedToken : IChangeToken
        {
            #region Implementation of IChangeToken

            /// <summary>
            /// 
            /// </summary>
            /// <param name="callback"></param>
            /// <param name="state"></param>
            /// <returns></returns>
            public IDisposable RegisterChangeCallback(Action<object> callback, object state)
            {
                return EmptyDisposable.Instance;
            }

            #endregion

            #region Implementation of IChangeToken

            /// <summary>
            /// 
            /// </summary>
            public bool HasChanged { get; } = false;

            /// <summary>
            /// 
            /// </summary>
            public bool ActiveChangeCallbacks { get; } = false;

            #endregion

            internal class EmptyDisposable : IDisposable
            {
                public static EmptyDisposable Instance { get; } = new EmptyDisposable();
                private EmptyDisposable() { }
                public void Dispose() { }
            }
        }
    }

}
