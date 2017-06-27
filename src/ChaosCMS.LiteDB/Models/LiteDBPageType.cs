using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace ChaosCMS.LiteDB.Models
{
    public class LiteDBPageType : IEntity
    {
        public ObjectId Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; internal set; }
    }
}
