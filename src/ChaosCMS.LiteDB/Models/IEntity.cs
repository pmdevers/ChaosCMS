using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.LiteDB.Models
{
    public interface IEntity
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        ObjectId Id { get; set; }
        string Origin { get; set; }
    }
}
