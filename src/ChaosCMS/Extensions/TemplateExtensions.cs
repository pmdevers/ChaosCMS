using System.Collections.Generic;
using Tavis.UriTemplates;

namespace ChaosCMS.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class TemplateExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="uriTemplateString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string SubstituteParams(this string uriTemplateString, IDictionary<string, object> parameters)
        {
            var uriTemplate = new UriTemplate(uriTemplateString);

            foreach (var parameter in parameters)
            {
                var name = parameter.Key;
                var value = parameter.Value;

                var substituionValue = value?.ToString();
                uriTemplate.SetParameter(name, substituionValue);
            }

            return uriTemplate.Resolve();
        }
    }
}