using System;
using ChaosCMS.Managers;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    public class ChaosFileProvider : IFileProvider
    {
        private readonly ResourceManager resourceManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="resourceManager"></param>
        public ChaosFileProvider(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subpath"></param>
        /// <returns></returns>
        public IFileInfo GetFileInfo(string subpath)
        {
            return this.resourceManager.FindByPath(subpath.TrimStart('/'));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subpath"></param>
        /// <returns></returns>
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IChangeToken Watch(string filter)
        {
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        public class NotChangedToken : IChangeToken
        {
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

            /// <summary>
            ///
            /// </summary>
            public bool HasChanged { get; } = false;

            /// <summary>
            ///
            /// </summary>
            public bool ActiveChangeCallbacks { get; } = false;

            internal class EmptyDisposable : IDisposable
            {
                public static EmptyDisposable Instance { get; } = new EmptyDisposable();

                private EmptyDisposable()
                {
                }

                public void Dispose()
                {
                }
            }
        }
    }
}