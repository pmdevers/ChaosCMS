using ChaosCMS.Razor.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public class RazorConfiguration
    {
        public string Root { get; set; } = "templates";
        public ISet<string> Namespaces { get; set; } = new HashSet<string>();
        public PreRenderActionList PreRenderCallbacks { get; set; } = new PreRenderActionList();
    }
}
