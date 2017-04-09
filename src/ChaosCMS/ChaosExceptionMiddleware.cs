using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ChaosCMS
{
    /// <summary>
    /// The Chaos exception middleware
    /// </summary>
    public class ChaosExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ChaosExceptionMiddleware> logger;

        /// <summary>
        /// Initilizes a niew instance of the <see cref="ChaosExceptionMiddleware"/>
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        /// <param name="logger"></param>
        public ChaosExceptionMiddleware(RequestDelegate next, ILogger<ChaosExceptionMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ChaosHttpExeption ex)
            {
                await httpContext.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError("-1", ex);
            }
        }
    }
}