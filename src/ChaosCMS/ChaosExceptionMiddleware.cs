using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS
{
    /// <summary>
    /// The Chaos exception middleware
    /// </summary>
    public class ChaosExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initilizes a niew instance of the <see cref="ChaosExceptionMiddleware"/>
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        public ChaosExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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
                await httpContext.Response.WriteAsync(ex.Message);
            }
        }
    }
}
