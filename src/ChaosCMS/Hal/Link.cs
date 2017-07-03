using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ChaosCMS.Extensions;
using Newtonsoft.Json;

namespace ChaosCMS.Hal
{
    /// <summary>
    ///
    /// </summary>
    public class Link
    {
        /// <summary>
        ///
        /// </summary>
        public const string RelForSelf = "self";

        private static readonly Regex IsTemplatedRegex = new Regex(@"{.+}", RegexOptions.Compiled);

        private readonly bool _replaceParameters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="href"></param>
        /// <param name="title"></param>
        /// <param name="method"></param>
        /// <param name="replaceParameters"></param>
        /// <param name="isRelArray"></param>
        public Link(string rel, string href, string title = null, string method = null, bool replaceParameters = true, bool isRelArray = false)
        {
            this.Rel = rel;
            this.Href = href;
            this.Title = title;
            this.Method = method;
            this._replaceParameters = replaceParameters;
            this.IsRelArray = isRelArray;
        }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public string Rel { get; private set; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public bool IsRelArray { get; private set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; private set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("templated", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Templated => !string.IsNullOrEmpty(Href) && IsTemplatedRegex.IsMatch(Href) ? (bool?)true : null;

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        public string Method { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("deprecation", NullValueHandling = NullValueHandling.Ignore)]
        public string Deprecation { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("profile", NullValueHandling = NullValueHandling.Ignore)]
        public string Profile { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("hreflang", NullValueHandling = NullValueHandling.Ignore)]
        public string HrefLang { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal Link CreateLink(IDictionary<string, object> parameters)
        {
            var clone = Clone();

            if (!_replaceParameters || parameters == null)
            {
                return clone;
            }

            if (!string.IsNullOrWhiteSpace(clone.Href))
            {
                clone.Href = clone.Href.SubstituteParams(parameters);
            }

            if (!string.IsNullOrWhiteSpace(clone.Title))
            {
                clone.Title = clone.Title.SubstituteParams(parameters);
            }

            return clone;
        }

        internal Link RebaseLink(string baseUriString)
        {
            var clone = Clone();

            if (!string.IsNullOrWhiteSpace(baseUriString))
            {
                var hrefUri = GetHrefUri(clone.Href);
                if (!hrefUri.IsAbsoluteUri)
                {
                    var baseUri = new Uri(baseUriString, UriKind.RelativeOrAbsolute);

                    if (baseUri.IsAbsoluteUri)
                    {
                        var rebasedUri = new Uri(baseUri, hrefUri);
                        clone.Href = rebasedUri.ToString();
                    }
                    else
                    {
                        // very simplistic but shoult work
                        clone.Href = String.Format("{0}/{1}", baseUriString.TrimEnd('/'), clone.Href.TrimStart('/'));
                    }
                }
            }

            return clone;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Link Clone()
        {
            return (Link)MemberwiseClone();
        }

        private static Uri GetHrefUri(string href)
        {
            return new Uri(href, UriKind.RelativeOrAbsolute);
        }
    }
}