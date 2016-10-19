using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace ChaosCMS.Razor.Templating.FileSystem
{
	public class FilesystemTemplateManager : ITemplateManager, IDisposable
	{
		private readonly PhysicalFileProvider fileProvider;
        private RazorConfiguration config;

        public FilesystemTemplateManager(IOptions<RazorConfiguration> options)
		{
            this.config = options.Value;
			this.Root = config.Root;
			this.fileProvider = new PhysicalFileProvider(config.Root);
		}

		public string Root { get; }

		public ITemplateSource Resolve(string key)
		{
			IFileInfo fileInfo = GetFileInfo(key);

			FileTemplateSource source = new FileTemplateSource(fileInfo, key);

			return source;
		}

		private IFileInfo GetFileInfo(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException(nameof(key));
			}

			IFileInfo fileInfo = fileProvider.GetFileInfo(key);
			if (!fileInfo.Exists || fileInfo.IsDirectory)
			{
				throw new FileNotFoundException("Can't find a file with a specified key", key);
			}

			return fileInfo;
		}

		public void Dispose()
		{
			if (fileProvider != null)
			{
				fileProvider.Dispose();
			}
		}
	}
}
