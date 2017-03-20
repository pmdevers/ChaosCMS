using Microsoft.AspNetCore.Builder;
using System;

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
    }
    /// <summary>
    /// 
    /// </summary>
    public class ChaosSecurityOptions : IdentityOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string RedirectAfterLoginPath { get; set; } = "/";
        /// <summary>
        /// 
        /// </summary>
        public string RedirectAfterLogoutPath { get; set; } = "/";

        /// <summary>
        /// 
        /// </summary>
        public string RegistrationPath { get; set; } = "/register";
    }
}