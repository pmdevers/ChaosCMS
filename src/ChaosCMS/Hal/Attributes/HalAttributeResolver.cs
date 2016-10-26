using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ChaosCMS.Extensions;

namespace ChaosCMS.Hal.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class HalAttributeResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IHalModelConfig GetConfig(object model)
        {
            var type = model.GetType();

            // is it worth caching this?
            var classAttributes = type.GetTypeInfo().GetCustomAttributes();

            foreach (var attribute in classAttributes)
            {
                var modelAttribute = attribute as HalModelAttribute;
                if (modelAttribute != null)
                {
                    if (modelAttribute.ForceHal.HasValue || modelAttribute.LinkBase != null)
                    {
                        var config = new HalModelConfig();
                        if (modelAttribute.ForceHal.HasValue)
                        {
                            config.ForceHal = modelAttribute.ForceHal.Value;
                        }
                        if (modelAttribute.LinkBase != null)
                        {
                            config.LinkBase = modelAttribute.LinkBase;
                        }
                        return config;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<Link> GetLinks(object model)
        {
            var type = model.GetType();
            var classAttributes = type.GetTypeInfo().GetCustomAttributes();

            foreach (var attribute in classAttributes)
            {
                var linkAttribute = attribute as HalLinkAttribute;
                if (linkAttribute != null)
                {
                    yield return new Link(linkAttribute.Rel, linkAttribute.Href, linkAttribute.Title, linkAttribute.Method);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, IEnumerable<HalResponse>>> GetEmbeddedCollections(object model, IHalModelConfig config)
        {
            var type = model.GetType();
            var embeddedModelProperties = type.GetTypeInfo().GetProperties().Where(x => x.IsDefined(typeof(HalEmbeddedAttribute)));

            foreach (var propertyInfo in embeddedModelProperties)
            {
                var embeddAttribute = propertyInfo.GetCustomAttribute(typeof(HalEmbeddedAttribute)) as HalEmbeddedAttribute;
                if (embeddAttribute == null) continue;

                var modelValue = propertyInfo.GetValue(model);

                var embeddedItems = modelValue as IEnumerable<object> ?? new List<object> { modelValue };

                var halResponses = embeddedItems.Select(embeddedModel => {
                    var response = new HalResponse(embeddedModel, config);
                    response.AddLinks(this.GetLinks(embeddedModel));
                    response.AddEmbeddedCollections(this.GetEmbeddedCollections(embeddedModel, config));

                    return response;
                });

                yield return new KeyValuePair<string, IEnumerable<HalResponse>>(embeddAttribute.CollectionName, halResponses);
            }
        }
    }
}
