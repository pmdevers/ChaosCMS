using System;

namespace ChaosCMS
{
    /// <summary>
    /// Service to enable localization for application facing chaos errors.
    /// </summary>
    /// <remarks>
    /// These errors are generally used as display messages to end users.
    /// </remarks>
    public class ChaosErrorDescriber
    {
        /// <summary>
        /// Returns the default <see cref="ChaosError"/>.
        /// </summary>
        /// <returns>The default <see cref="ChaosError"/>.</returns>
        public virtual ChaosError DefaultError()
        {
            return new ChaosError
            {
                Code = nameof(DefaultError),
                Description = Resources.DefaultError
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual ChaosError NegativePage()
        {
            return new ChaosError
            {
                Code = nameof(NegativePage),
                Description = Resources.NegativePage
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ChaosError PageNameIsInvalid(string name)
        {
            return new ChaosError
            {
                Code = nameof(PageNameIsInvalid),
                Description = Resources.FormatPageNameIsInvalid(name)
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public ChaosError PageTypeIsInvalid(string pageType)
        {
            return new ChaosError
            {
                Code = nameof(PageTypeIsInvalid),
                Description = Resources.FormatPageTypeIsInvalid(pageType)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ChaosError PageTypeNameIsInvalid(string name)
        {
            return new ChaosError
            {
                Code = nameof(PageTypeNameIsInvalid),
                Description = Resources.FormatNameIsInvalid(name)
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ChaosError PageUrlIsInvalid(string url)
        {
            return new ChaosError
            {
                Code = nameof(PageUrlIsInvalid),
                Description = Resources.FormatPageUrlIsInvalid(url)
            };
        }

        internal ChaosError PageStatusCodeIsInvalid(int status)
        {
            return new ChaosError
            {
                Code = nameof(PageStatusCodeIsInvalid),
                Description = Resources.FormatPageStatusIsInvalid(status)
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public ChaosError PageTemplateIsInvalid(string template)
        {
            return new ChaosError
            {
                Code = nameof(PageTemplateIsInvalid),
                Description = Resources.FormatPageTemplateIsInvalid(template)
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ChaosError ContentNameIsInvalid(string name)
        {
            return new ChaosError
            {
                Code = nameof(ContentNameIsInvalid),
                Description = Resources.FormatContentNameIsInvalid(name)
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ChaosError ContentTypeIsInvalid(string type)
        {
            return new ChaosError
            {
                Code = nameof(ContentTypeIsInvalid),
                Description = Resources.FormatContentTypeIsInvalid(type)
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ChaosError ContentValueIsInvalid(string value)
        {
            return new ChaosError
            {
                Code = nameof(ContentValueIsInvalid),
                Description = Resources.FormatContentValueIsInvalid(value)
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ChaosError ContentNameIsNotUnique(string name)
        {
            return new ChaosError
            {
                Code = nameof(ContentNameIsNotUnique),
                Description = Resources.FormatContentNameIsNotUnique(name)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public ChaosError PageIdNotFound(string pageId)
        {
            return new ChaosError
            {
                Code = nameof(PageIdNotFound),
                Description = Resources.FormatPageNotFound(pageId)
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ChaosError ConcurrencyFailure()
        {
            return new ChaosError
            {
                Code = nameof(ConcurrencyFailure),
                Description = Resources.ConcurrencyFailure
            };
        }
    }
}