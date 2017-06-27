using System;

namespace ChaosCMS.Json
{
    /// <summary>
    ///
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        ///
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ExternalId { get; set; }
    }
}