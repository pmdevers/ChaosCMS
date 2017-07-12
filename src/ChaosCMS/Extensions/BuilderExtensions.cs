using System;
using ChaosCMS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            var hosting = app.ApplicationServices.GetService<IHostingEnvironment>();

            var marker = app.ApplicationServices.GetService<ChaosMarkerService>();
            if (marker == null)
            {
                throw new InvalidOperationException(Resources.MustCallAddChaos);
            }

            var options = app.ApplicationServices.GetRequiredService<IOptions<ChaosOptions>>().Value;
            var builder = app.ApplicationServices.GetService<ChaosBuilder>();
            var exceptionMiddleWare = typeof(ChaosExceptionMiddleware);

            if (hosting.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/error");
                app.UseStatusCodePagesWithReExecute("/{0}");
            }

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
            //app.UseMiddleware(exceptionMiddleWare);

            app.UseSwagger();

            if (hosting.IsDevelopment())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseIdentity();
            app.UseMvc();
            
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello, World!");
            });

            return app;
        }
    }
}