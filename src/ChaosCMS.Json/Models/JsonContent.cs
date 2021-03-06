﻿using ChaosCMS.Models.Pages;
using System;

namespace ChaosCMS.Json.Models
{
    /// <summary>
    ///
    /// </summary>
    public class JsonContent : IEntity
    {
        /// <summary>
        ///
        /// </summary>
        public JsonContent()
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
        public string ExternalId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Guid PageId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Value { get; set; }
    }
}