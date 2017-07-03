using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Json.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonPageType : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonPageType()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExternalId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}
