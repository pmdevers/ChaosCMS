﻿using System;
using System.Text;
using ChaosCMS.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    public class ChaosSecurityOptions
    {
        /// <summary>
        ///
        /// </summary>
        public ChaosSecurityOptions()
        {
            // Password settings
            Identity.Password.RequireDigit = true;
            Identity.Password.RequiredLength = 8;
            Identity.Password.RequireNonAlphanumeric = false;
            Identity.Password.RequireUppercase = true;
            Identity.Password.RequireLowercase = false;

            // Lockout settings
            Identity.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            Identity.Lockout.MaxFailedAccessAttempts = 10;

            // Cookie settings
            Identity.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
            Identity.Cookies.ApplicationCookie.LoginPath = "/login";
            Identity.Cookies.ApplicationCookie.LogoutPath = "/logout";

            // User settings
            Identity.User.RequireUniqueEmail = false;
        }

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

        /// <summary>
        ///
        /// </summary>
        public IdentityOptions Identity { get; set; } = new IdentityOptions();

        /// <summary>
        ///
        /// </summary>
        public TokenOptions Tokens { get; set; } = new TokenOptions();

        /// <summary>
        ///
        /// </summary>
        public CookieOptions Cookies { get; set; } = new CookieOptions();

        /// <summary>
        ///
        /// </summary>
        internal CookieAuthenticationOptions GetCookiesOptions()
        {
            return new CookieAuthenticationOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = this.Cookies.AuthenticationScheme,
                CookieName = this.Cookies.CookieName,
                TicketDataFormat = new ChaosJwtDataFormat(
                                        SecurityAlgorithms.HmacSha256,
                                        this.GetTokenValidationParameters())
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal TokenValidationParameters GetTokenValidationParameters()
        {
            // secretKey contains a secret passphrase only your server knows
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.Tokens.SecretKey));
            return new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = this.Tokens.ValidIsuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = this.Tokens.ValidAudience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };
        }

        /// <summary>
        ///
        /// </summary>
        public class TokenOptions
        {
            /// <summary>
            ///
            /// </summary>
            public string SecretKey { get; set; } = "ChaosKey";

            /// <summary>
            ///
            /// </summary>
            public string ValidIsuer { get; set; } = "ChaosIssuer";

            /// <summary>
            ///
            /// </summary>
            public string ValidAudience { get; set; } = "ChaosAudience";

            /// <summary>
            ///
            /// </summary>
            public string Path { get; set; } = "/token";

            /// <summary>
            ///
            /// </summary>
            public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);

            /// <summary>
            ///
            /// </summary>
            public SigningCredentials SigningCredentials { get; set; }
        }

        /// <summary>
        ///
        /// </summary>
        public class CookieOptions
        {
            /// <summary>
            ///
            /// </summary>
            public string AuthenticationScheme { get; set; } = "Cookie";

            /// <summary>
            ///
            /// </summary>
            public string CookieName { get; set; } = "ChaosAuth";
        }
    }
}