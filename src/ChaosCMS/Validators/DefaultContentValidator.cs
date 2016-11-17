using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosCMS.Managers;

namespace ChaosCMS.Validators
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class DefaultContentValidator<TContent> : IContentValidator<TContent> where TContent : class
    {
        private readonly ChaosErrorDescriber errorDescriber;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorDescriber"></param>
        public DefaultContentValidator(ChaosErrorDescriber errorDescriber)
        {
            this.errorDescriber = errorDescriber;
        }

        /// <inheritdoc />
        public async Task<ChaosResult> ValidateAsync(ContentManager<TContent> manager, TContent content)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var errors = new List<ChaosError>();

            await ValidateNameAsync(manager, content, errors);
            await ValidateTypeAsync(manager, content, errors);
            await ValidateValueAsync(manager, content, errors);

            if (errors.Count > 0)
            {
                return ChaosResult.Failed(errors.ToArray());
            }

            return ChaosResult.Success;
        }

        private async Task ValidateValueAsync(ContentManager<TContent> manager, TContent content, List<ChaosError> errors)
        {
            var value = await manager.GetValueAsync(content);
            if (string.IsNullOrEmpty(value))
            {
                errors.Add(errorDescriber.ContentNameIsInvalid(value));
            }
        }

        private async Task ValidateTypeAsync(ContentManager<TContent> manager, TContent content, List<ChaosError> errors)
        {
            var type = await manager.GetTypeAsync(content);
            if (string.IsNullOrEmpty(type))
            {
                errors.Add(errorDescriber.ContentNameIsInvalid(type));
            }
        }

        private async Task ValidateNameAsync(ContentManager<TContent> manager, TContent content, List<ChaosError> errors)
        {
            var name = await manager.GetNameAsync(content);
            if (string.IsNullOrEmpty(name))
            {
                errors.Add(errorDescriber.ContentNameIsInvalid(name));
            }

            var item = await manager.FindByNameAsync(name);
            if (item != null)
            {
                errors.Add(this.errorDescriber.ContentNameIsNotUnique(name));
            }
        }
    }
}
