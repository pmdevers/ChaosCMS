using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChaosCMS.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaosJwtDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string algorithm;
        private readonly TokenValidationParameters validationParameters;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="validationParameters"></param>
        public ChaosJwtDataFormat(string algorithm, TokenValidationParameters validationParameters)
        {
            this.algorithm = algorithm;
            this.validationParameters = validationParameters;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protectedText"></param>
        /// <returns></returns>
        public AuthenticationTicket Unprotect(string protectedText)
            => Unprotect(protectedText, null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protectedText"></param>
        /// <param name="purpose"></param>
        /// <returns></returns>
        public AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = null;
            SecurityToken validToken = null;

            try
            {
                principal = handler.ValidateToken(protectedText, this.validationParameters, out validToken);

                var validJwt = validToken as JwtSecurityToken;

                if (validJwt == null)
                {
                    throw new ArgumentException("Invalid JWT");
                }

                if (!validJwt.Header.Alg.Equals(algorithm, StringComparison.Ordinal))
                {
                    throw new ArgumentException($"Algorithm must be '{algorithm}'");
                }

                // Additional custom validation of JWT claims here (if any)
            }
            catch (SecurityTokenValidationException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }

            // Validation passed. Return a valid AuthenticationTicket:
            return new AuthenticationTicket(principal, new AuthenticationProperties(), "Cookie");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // This ISecureDataFormat implementation is decode-only
        public string Protect(AuthenticationTicket data)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="purpose"></param>
        /// <returns></returns>
        public string Protect(AuthenticationTicket data, string purpose)
        {
            throw new NotImplementedException();
        }
    }
}
