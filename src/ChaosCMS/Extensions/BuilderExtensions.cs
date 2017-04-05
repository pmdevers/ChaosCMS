using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using ChaosCMS;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ChaosCMS.Security;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Chaos extensions for <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Enables Chaos for the current application.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> instance this method extends.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance this method extends.</returns>
        public static IApplicationBuilder UseChaos(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var marker = app.ApplicationServices.GetService<ChaosMarkerService>();
            if (marker == null)
            {
                throw new InvalidOperationException(Resources.MustCallAddChaos);
            }

            var options = app.ApplicationServices.GetRequiredService<IOptions<ChaosOptions>>().Value;
            var builder = app.ApplicationServices.GetService<ChaosBuilder>();
            var exceptionMiddleWare = typeof(ChaosExceptionMiddleware);
            //var tokenProvider = typeof(TokenProviderMiddleware<>).MakeGenericType(builder.IdentityBuilder.UserType);

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = options.Security.GetTokenValidationParameters()
            });

            app.UseCookieAuthentication(options.Security.GetCookiesOptions());

            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build();
            });
            app.UseStaticFiles();
            app.UseMiddleware(exceptionMiddleWare);
            //app.UseMiddleware(tokenProvider);
            app.UseIdentity();
            app.UseMvc();

            //app.UseMiddleware(middleware);
            
            return app;
        }
    }
}