using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUrlFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string FormatUrl(string url);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DefaultUrlFormatter : IUrlFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        public DefaultUrlFormatter(IOptions<ChaosOptions> optionsAccessor)
        {
            this.Options = optionsAccessor?.Value ?? new ChaosOptions();

        }

        /// <summary>
        /// 
        /// </summary>
        protected internal ChaosOptions Options { get; private set; }

        /// <inherits />
        public string FormatUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            string normalised;
            if (this.Options.UrlFormatter.EarlyTruncate && this.Options.UrlFormatter.MaximumLength > 0 && url.Length > this.Options.UrlFormatter.MaximumLength)
            {
                normalised = url.Substring(0, this.Options.UrlFormatter.MaximumLength).Normalize(NormalizationForm.FormD);
            }
            else
            {
                normalised = url.Normalize(NormalizationForm.FormD);
            }

            var max = this.Options.UrlFormatter.MaximumLength > 0 ? Math.Min(normalised.Length, this.Options.UrlFormatter.MaximumLength) : normalised.Length;
            var sb = new StringBuilder(max);
            for (var i = 0; i < normalised.Length; i++)
            {
                var c = normalised[i];
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (this.Options.UrlFormatter.AllowedUnicodeCategories.Contains(uc) && this.IsAllowed(c))
                {
                    switch (uc)
                    {
                        case UnicodeCategory.UppercaseLetter:
                            if (this.Options.UrlFormatter.ToLower)
                            {
                                c = char.ToLowerInvariant(c);
                            }
                            sb.Append(this.Replace(c));
                            break;

                        case UnicodeCategory.LowercaseLetter:
                            if (this.Options.UrlFormatter.ToUpper)
                            {
                                c = char.ToUpperInvariant(c);
                            }
                            sb.Append(this.Replace(c));
                            break;

                        default:
                            sb.Append(this.Replace(c));
                            break;
                    }
                }
                else if (uc == UnicodeCategory.NonSpacingMark)
                {
                    // don't add a separator
                }
                else
                {
                    if (this.Options.UrlFormatter.Separator != null && !EndsWith(sb, this.Options.UrlFormatter.Separator))
                    {
                        sb.Append(this.Options.UrlFormatter.Separator);
                    }
                }

                if (this.Options.UrlFormatter.MaximumLength > 0 && sb.Length >= this.Options.UrlFormatter.MaximumLength)
                    break;
            }

            var result = sb.ToString();

            if (this.Options.UrlFormatter.MaximumLength > 0 && result.Length > this.Options.UrlFormatter.MaximumLength)
            {
                result = result.Substring(0, this.Options.UrlFormatter.MaximumLength);
            }

            if (!this.Options.UrlFormatter.CanEndWithSeparator && this.Options.UrlFormatter.Separator != null && result.EndsWith(this.Options.UrlFormatter.Separator))
            {
                result = result.Substring(0, result.Length - this.Options.UrlFormatter.Separator.Length);
            }

            return result.Normalize(NormalizationForm.FormC).ToLower();
        }
        
        private bool EndsWith(StringBuilder sb, string text)
        {
            if (sb.Length < text.Length)
                return false;

            for (var i = 0; i < text.Length; i++)
            {
                if (sb[sb.Length - 1 - i] != text[text.Length - 1 - i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the specified character is allowed.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns>true if the character is allowed; false otherwise.</returns>
        public virtual bool IsAllowed(char character)
        {
            foreach (var p in this.Options.UrlFormatter.AllowedRanges)
            {
                if (character >= p.Key && character <= p.Value) return true;
            }
            return false;
        }

        /// <summary>
        /// Replaces the specified character by a given string.
        /// </summary>
        /// <param name="character">The character to replace.</param>
        /// <returns>a string.</returns>
        public virtual string Replace(char character)
        {
            return character.ToString();
        }

    }
}
