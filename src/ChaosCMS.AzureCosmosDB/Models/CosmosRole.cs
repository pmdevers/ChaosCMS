using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.AzureCosmosDB.Models
{
    public class CosmosRole : IEntity
    {
        public string Id { get; set; }
        public string Origin { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; internal set; }
    }
}
