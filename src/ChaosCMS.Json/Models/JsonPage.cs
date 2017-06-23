using System;
using System.Collections.Generic;

namespace ChaosCMS.Json.Models
{
    /// <summary>
    ///
    /// </summary>
    public class JsonPage : IEntity
    {
        /// <summary>
        ///
        /// </summary>
        public JsonPage()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        ///
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Hosts { get; set; } = new List<string>();

        /// <summary>
        ///
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Template { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
    }
}