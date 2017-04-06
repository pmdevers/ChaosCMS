using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaosTypesPart : ApplicationPart, IApplicationPartTypeProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        public ChaosTypesPart(params Type[] types)
        {
            Types = types.Select(t => t.GetTypeInfo());
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Name => string.Join(", ", Types.Select(t => t.FullName));

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<TypeInfo> Types { get; }
    }
}