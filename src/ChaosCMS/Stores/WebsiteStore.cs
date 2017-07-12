using ChaosCMS.Models.Website;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebsiteStore : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ChaosResult> CreateAsync(Site site, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosResult> UpdateAsync(Site site, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosResult> DeleteAsync(Site site, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Site> FindByIdAsync(string id, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetIdAsync(Site site, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Site> FindByHostAsync(HostString host, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Site> FindByRequestAsync(HttpRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="normalizedName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Site> FindByNameAsync(string normalizedName, CancellationToken cancellationToken);
    }
}
