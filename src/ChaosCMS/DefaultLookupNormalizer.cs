using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ChaosCMS
{
    /// <inherits />
    public class DefaultLookupNormalizer : ILookupNormalizer
    {
        /// <inherits />
        public string Normalize(string value)
        {
            if (value == null)
            {
                return null;
            }
            return value.Normalize().ToUpperInvariant();
        }
    }
}
