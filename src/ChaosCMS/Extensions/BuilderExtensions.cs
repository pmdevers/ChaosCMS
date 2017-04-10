using System;
using ChaosCMS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            app.UseIdentity();
            app.UseMvc();
            
            return app;
        }
    }
}