using ChaosCMS.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Models.Pages
{
    /// <summary>
    /// Content Class
    /// </summary>
    public class Content
    {
        /// <summary>
        /// The name of the content
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Type of the content
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The value of the content
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The underlying Content
        /// </summary>
        public List<Content> Children { get; set; } = new List<Content>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        public TResult GetValue<TResult>() where TResult : RenderOptions
        {
            var result = JsonConvert.DeserializeObject<TResult>(Value);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        public void SetValue<TResult>(TResult value) where TResult : RenderOptions
        {
            this.Value = JsonConvert.SerializeObject(Value);
        }
    }
}
