using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChaosCMS.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenProviderMiddleware<TUser>
        where TUser : class
    {
        private readonly RequestDelegate next;
        private readonly ChaosOptions options;
        private readonly UserManager<TUser> userManager;
        private readonly SignInManager<TUser> signInManager;
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public TokenProviderMiddleware(
            RequestDelegate next,
            UserManager<TUser> userManager,
            SignInManager<TUser> signInManager,
            IOptions<ChaosOptions> options)
        {
            this.next = next;
            this.options = options.Value;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(this.options.Security.Tokens.Path, StringComparison.Ordinal))
            {
                return this.next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
               || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, Math.Round((DateTime.UtcNow - UnixEpoch).TotalSeconds).ToString(), ClaimValueTypes.Integer64)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: this.options.Security.Tokens.ValidIsuer,
                audience: this.options.Security.Tokens.ValidAudience,
                claims: claims,
                notBefore: now,
                expires: now.Add(this.options.Security.Tokens.Expiration),
                signingCredentials: this.options.Security.Tokens.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)this.options.Security.Tokens.Expiration.TotalSeconds
            };

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var result = await this.signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            // DON'T do this in production, obviously!
            if (result.Succeeded)
            {
                return new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { });
            }

            // Credentials are invalid, or account doesn't exist
            return null;
        }
    }
}
