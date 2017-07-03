using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosCMS.Managers;

namespace ChaosCMS.Validators
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPageType"></typeparam>
    public class DefaultPageTypeValidator<TPageType> : IPageTypeValidator<TPageType>
        where TPageType : class
    {
        private readonly ChaosErrorDescriber errorDescriber;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorDescriber"></param>
        public DefaultPageTypeValidator(ChaosErrorDescriber errorDescriber)
        {
            this.errorDescriber = errorDescriber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public async Task<ChaosResult> ValidateAsync(PageTypeManager<TPageType> manager, TPageType pageType)
        {
            if(manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            var errors = new List<ChaosError>();

            await this.ValidateNameAsync(manager, pageType, errors);

            if(errors.Count > 0)
            {
                return ChaosResult.Failed(errors.ToArray());
            }

            return ChaosResult.Success;
        }

        private async Task ValidateNameAsync(PageTypeManager<TPageType> manager, TPageType pageType, List<ChaosError> errors)
        {
            var name = await manager.GetNameAsync(pageType);
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(errorDescriber.PageTypeNameIsInvalid(name));
            }
        }
    }
}
