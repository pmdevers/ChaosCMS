using System;
using System.Text;
using ChaosCMS.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace ChaosCMS
{
    /// <summary>
    /// Represents all the options you cna use to configure the chaos system.
    /// </summary>
    public class ChaosOptions
    {
        /// <summary>
        /// Gets or sets the maximum items per page.
        /// </summary>
        public int MaxItemsPerPage { get; set; } = 128;

        /// <summary>
        /// Gets or set the template directory.
        /// </summary>
        public string TempateDirectory { get; set; } = "Templates";

        /// <summary>
        /// Gets or set the SecurityOptions used.
        /// </summary>
        public ChaosSecurityOptions Security { get; set; } = new ChaosSecurityOptions();

        /// <summary>
        /// Gets or sets the UrlFormatter options used.
        /// </summary>
        public ChaosUrlFormatterOptions UrlFormatter { get; set; } = new ChaosUrlFormatterOptions();
    }
}