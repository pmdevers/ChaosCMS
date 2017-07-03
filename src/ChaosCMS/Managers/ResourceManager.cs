using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyModel;

namespace ChaosCMS.Managers
{
    /// <summary>
    ///
    /// </summary>
    public class ResourceManager
    {
        /// <summary>
        ///
        /// </summary>
        public ResourceManager()
        {
            var assemblies = this.GetAssemblies();

            foreach (var assemblyName in assemblies)
            {
                var assembly = Assembly.Load(assemblyName);
                this.GetResources(assembly);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public IList<ChaosResourceItem> Resources { get; set; } = new List<ChaosResourceItem>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public ChaosResourceItem FindByPath(string virtualPath)
        {
            var name = this.GetNameFromPath(virtualPath);
            var fileName = Path.GetFileName(virtualPath);
            return string.IsNullOrEmpty(fileName)
                       ? null
                       : this.Resources.FirstOrDefault(x => x.Name.EndsWith(name, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string GetNameFromPath(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
            {
                return null;
            }

            return virtualPath.Replace("~", string.Empty).Replace("/", ".");
        }

        private IEnumerable<AssemblyName> GetAssemblies()
        {
            var runtimeId = RuntimeEnvironment.GetRuntimeIdentifier();
            var assemblies = DependencyContext.Default.GetRuntimeAssemblyNames(runtimeId);
            return assemblies;
        }

        private void GetResources(Assembly assembly)
        {
            var names = assembly.GetManifestResourceNames();

            foreach (var name in names)
            {
                var extension = Path.GetExtension(name);
                if (ChaosResourceTypes.Contains(extension) || name.ToLowerInvariant().Contains(".views."))
                {
                    var stream = assembly.GetManifestResourceStream(name);
                    this.Resources.Add(new ChaosResourceItem(stream, name));
                }
            }
        }
    }
}