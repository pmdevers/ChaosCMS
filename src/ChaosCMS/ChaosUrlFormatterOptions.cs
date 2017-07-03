using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS
{
    /// <summary>
    /// Url Formater Options
    /// </summary>
    public class ChaosUrlFormatterOptions
    {
        private bool toLower;
        private bool toUpper;
        
        /// <summary>
        /// The max lenght allowed in a url;
        /// </summary>
        public int MaximumLength { get; set; } = 80;
        /// <summary>
        /// Gets or sets a value indicating whether the string is truncated before normalization.
        /// </summary>
        public bool EarlyTruncate { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether the string can end with a separator string.
        /// </summary>
        public bool CanEndWithSeparator { get; set; } = false;
        /// <summary>
        /// Gets or sets the separator.
        /// </summary>
        public string Separator { get;  set; } = "-";
        /// <summary>
        /// Gets or sets a value indicating whether to uppercase the resulting string.
        /// </summary>
        public virtual bool ToUpper
        {
            get
            {
                return this.toUpper;
            }
            set
            {
                this.toUpper = value;
                if (this.toUpper)
                {
                    this.toLower = false;
                }
            }
        }
        /// <summary>
        ///  Gets or sets a value indicating whether to lowercase the resulting string.
        /// </summary>
        public virtual bool ToLower
        {
            get
            {
                return this.toLower;
            }
            set
            {
                this.toLower = value;
                if (this.toLower)
                {
                    this.toUpper = false;
                }
            }
        }
        /// <summary>
        /// Gets the allowed unicode categories list.
        /// </summary>
        public List<UnicodeCategory> AllowedUnicodeCategories { get; set; } = new List<UnicodeCategory>() { UnicodeCategory.UppercaseLetter, UnicodeCategory.LowercaseLetter, UnicodeCategory.DecimalDigitNumber };
        /// <summary>
        /// Gets the allowed ranges list.
        /// </summary>
        public virtual IList<KeyValuePair<short, short>> AllowedRanges { get; private set; } = new List<KeyValuePair<short, short>>
        {
            new KeyValuePair<short, short>((short)'a', (short)'z'),
            new KeyValuePair<short, short>((short)'A', (short)'Z'),
            new KeyValuePair<short, short>((short)'0', (short)'9')
        };
    }
}
