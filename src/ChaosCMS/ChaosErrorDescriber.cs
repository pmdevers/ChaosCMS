using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS
{
    /// <summary>
    /// Service to enable localization for application facing chaos errors.
    /// </summary>
    /// <remarks>
    /// These errors are generally used as display messages to end users.
    /// </remarks>
    public class ChaosErrorDescriber
    {
        /// <summary>
        /// Returns the default <see cref="ChaosError"/>.
        /// </summary>
        /// <returns>The default <see cref="ChaosError"/>.</returns>
        public virtual ChaosError DefaultError()
        {
            return new ChaosError
            {
                Code = nameof(DefaultError),
                Description = Resources.DefaultError
            };
        }
    }
}
