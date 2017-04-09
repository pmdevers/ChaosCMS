using System;

namespace ChaosCMS.Json.Models
{
    /// <summary>
    ///
    /// </summary>
    public class JsonRole : IEntity
    {
        /// <summary>
        ///
        /// </summary>
        public JsonRole()
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
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string NormalizedName { get; set; }
    }
}