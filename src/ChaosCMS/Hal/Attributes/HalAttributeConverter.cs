using System;
using System.Linq;
using System.Reflection;
using ChaosCMS.Extensions;

namespace ChaosCMS.Hal.Attributes
{
    /// <summary>
    ///
    /// </summary>
    public class HalAttributeConverter : IHalConverter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CanConvert(Type type)
        {
            if (type == null || type == typeof(HalResponse))
            {
                return false;
            }

            // Is it worth caching this check?
            return type.GetTypeInfo().GetCustomAttributes().Any(x => x is HalModelAttribute);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HalResponse Convert(object model)
        {
            if (!this.CanConvert(model?.GetType()))
            {
                throw new InvalidOperationException();
            }

            var resolver = new HalAttributeResolver();

            var halConfig = resolver.GetConfig(model);

            var response = new HalResponse(model, halConfig);
            response.AddLinks(resolver.GetLinks(model));
            response.AddEmbeddedCollections(resolver.GetEmbeddedCollections(model, halConfig));

            return response;
        }
    }
}