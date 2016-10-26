using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChaosCMS.Extensions;
using ChaosCMS.Hal.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChaosCMS.Hal
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(JsonHalModelConverter))]
    public class HalResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public const string LinksKey = "_links";
        /// <summary>
        /// 
        /// </summary>
        public const string EmbeddedKey = "_embedded";

        private readonly IHalModelConfig config;

        private readonly List<Link> links = new List<Link>();
        private readonly Dictionary<string, object> embedded = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public HalResponse(IHalModelConfig config)
        {
            this.config = config ?? new HalModelConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="config"></param>
        public HalResponse(object model, IHalModelConfig config = null)
            : this(config)
        {
            if (!(model is JObject) && (model is IEnumerable))
            {
                throw new ArgumentException("The HAL model should not be Enumerable. You should use an embedded collection instead", nameof(model));
            }
            this.Model = model;
        }

        /// <summary>
        /// 
        /// </summary>
        public object Model { get; }

        /// <summary>
        /// 
        /// </summary>
        public IHalModelConfig Config => config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rel"></param>
        /// <returns></returns>
        public bool HasLink(string rel)
        {
            return links.Any(l => l.Rel == rel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="links"></param>
        /// <returns></returns>
        public HalResponse AddLinks(IEnumerable<Link> links)
        {
            this.links.AddRange(links);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public HalResponse AddEmbeddedResource(string name, HalResponse resource)
        {
            embedded.Add(name, resource);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public HalResponse AddEmbeddedCollection(string name, IEnumerable<HalResponse> objects)
        {
            embedded.Add(name, objects);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="attachEmbedded"></param>
        /// <returns></returns>
        public JObject ToPlainResponse(JsonSerializer serializer, bool attachEmbedded = true)
        {
            var output = GetBaseJObject(serializer);

            if (this.embedded.Any())
            {
                var embeddedOutput = EmbeddedToJObject((m) => m.ToPlainResponse(serializer));
                output.Merge(embeddedOutput);
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public JObject ToJObject(JsonSerializer serializer)
        {
            var output = GetBaseJObject(serializer);

            if (this.links.Any())
            {
                var linksOutput = new JObject();

                var dtoProps = this.Model?.ToDictionary() ?? new Dictionary<string, object>();
                var resolvedLinks = GetResolvedLinks(this.links, dtoProps, this.config.LinkBase);

                foreach (var link in resolvedLinks)
                {
                    if (link.Value is IEnumerable)
                    {
                        var linksOuput = JArray.FromObject(link.Value);
                        linksOutput.Add(link.Key, linksOuput);
                    }
                    else
                    {
                        var linkOuput = JObject.FromObject(link.Value);
                        linksOutput.Add(link.Key, linkOuput);
                    }
                }

                output.Add(LinksKey, linksOutput);
            }

            if (this.embedded.Any())
            {
                var embeddedOutput = EmbeddedToJObject((m) => m.ToJObject(serializer));
                output.Add(EmbeddedKey, embeddedOutput);
            }

            return output;
        }

        private JObject EmbeddedToJObject(Func<HalResponse, JObject> converter)
        {
            var embeddedOutput = new JObject();
            foreach (var embedPair in this.embedded)
            {

                if (embedPair.Value is IEnumerable<HalResponse>)
                {
                    embeddedOutput.Add(embedPair.Key, JArray.FromObject(((IEnumerable<HalResponse>)embedPair.Value).Select(m => converter(m))));
                }
                else if (embedPair.Value is HalResponse)
                {
                    embeddedOutput.Add(embedPair.Key, JObject.FromObject(converter((HalResponse)embedPair.Value)));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return embeddedOutput;
        }

        private JObject GetBaseJObject(JsonSerializer serializer)
        {
            var output = this.Model != null ? 
                JObject.FromObject(this.Model, serializer) : 
                new JObject();
            return output;
        }

        private static Dictionary<string, object> GetResolvedLinks(IEnumerable<Link> links, IDictionary<string, object> properties, string linkBase)
        {
            var subsituted = links;

            if (properties.Any())
            {
                subsituted = links.Select(l => l.CreateLink(properties)).ToList();
            }

            var resolved = subsituted;

            if (!string.IsNullOrWhiteSpace(linkBase))
            {
                resolved = subsituted.Select(l => l.RebaseLink(linkBase)).ToList();
            }

            var grouped = resolved.GroupBy(r => r.Rel);

            var singles = grouped.Where(g => g.Count() <= 1 && g.All(l => !l.IsRelArray)).ToDictionary(k => k.Key, v => v.SingleOrDefault() as object);
            var lists = grouped.Where(g => g.Count() > 1 || g.Any(l => l.IsRelArray)).ToDictionary(k => k.Key, v => v.AsEnumerable() as object);

            var allLinks = singles.Concat(lists).ToDictionary(k => k.Key, v => v.Value);

            return allLinks;
        }
    }
}
