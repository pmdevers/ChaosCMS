using System.Collections.Generic;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// Default Element options
    /// </summary>
    public class RenderOptions
    {
        /// <summary>
        ///
        /// </summary>
        public List<string> CssClass { get; set; } = new List<string>();

        /// <summary>
        ///
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
    }
}