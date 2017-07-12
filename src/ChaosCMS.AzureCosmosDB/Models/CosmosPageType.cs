using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.AzureCosmosDB.Models
{
    public class CosmosPageType : IEntity
    {
        public string Id { get; set; }
        public string Host { get; set; }
        public string Origin { get; set; }
        public string Name { get;  set; }
        
    }
}
