using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.AzureCosmosDB.Models
{
    public interface IEntity
    {
        [JsonProperty(PropertyName = "id")]
        string Id { get; set; }
        string Origin { get; set; }
    }
}
