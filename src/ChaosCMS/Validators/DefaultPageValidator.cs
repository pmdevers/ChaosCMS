using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChaosCMS.Managers;

namespace ChaosCMS.Validators
{
    /// <inheritdoc />
    public class DefaultPageValidator<TPage> : IPageValidator<TPage> where TPage : class
    {
        private readonly ChaosErrorDescriber errorDescriber;

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorDescriber"></param>
        public DefaultPageValidator(ChaosErrorDescriber errorDescriber)
        {
            this.errorDescriber = errorDescriber;
        }

        /// <inheritdoc />
        public async Task<ChaosResult> ValidateAsync(PageManager<TPage> manager, TPage page)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            var errors = new List<ChaosError>();

            await ValidatePageName(manager, page, errors);
            await ValidatePageType(manager, page, errors);
            await ValidatePageUrl(manager, page, errors);
            await ValidatePageTemplate(manager, page, errors);
            await ValidatePageStatusCode(manager, page, errors);

            if (errors.Count > 0)
            {
                return ChaosResult.Failed(errors.ToArray());
            }
            return ChaosResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private async Task ValidatePageType(PageManager<TPage> manager, TPage page, List<ChaosError> errors)
        {
            var pageType = await manager.GetPageTypeAsync(page);
            if (string.IsNullOrWhiteSpace(pageType))
            {
                errors.Add(errorDescriber.PageTypeIsInvalid(pageType));
            }
        }

        private async Task ValidatePageStatusCode(PageManager<TPage> manager, TPage page, List<ChaosError> errors)
        {
            var status = await manager.GetStatusCodeAsync(page);
            var found = await manager.FindByStatusCodeAsync(status);
            if(status != 200 && found != null)
            {
                errors.Add(errorDescriber.PageStatusCodeIsInvalid(status));
            }
        }

        private async Task ValidatePageUrl(PageManager<TPage> manager, TPage page, List<ChaosError> errors)
        {
            var url = await manager.GetUrlAsync(page);
            if (string.IsNullOrWhiteSpace(url))
            {
                errors.Add(errorDescriber.PageUrlIsInvalid(url));
            }
        }

        private async Task ValidatePageTemplate(PageManager<TPage> manager, TPage page, List<ChaosError> errors)
        {
            var template = await manager.GetTemplateAsync(page);
            if (string.IsNullOrWhiteSpace(template))
            {
                errors.Add(errorDescriber.PageTemplateIsInvalid(template));
            }
        }

        private async Task ValidatePageName(PageManager<TPage> manager, TPage page, List<ChaosError> errors)
        {
            var name = await manager.GetNameAsync(page);
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(errorDescriber.PageNameIsInvalid(name));
            }
        }
    }
}