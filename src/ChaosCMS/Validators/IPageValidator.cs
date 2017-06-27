using System.Threading.Tasks;
using ChaosCMS.Managers;

namespace ChaosCMS.Validators
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    public interface IPageValidator<TPage, TContent>
        where TPage : class
        where TContent : class
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<ChaosResult> ValidateAsync(PageManager<TPage, TContent> manager, TPage page);
    }
}