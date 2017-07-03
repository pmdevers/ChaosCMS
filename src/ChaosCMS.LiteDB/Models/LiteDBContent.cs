using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.LiteDB.Models
{
    public class LiteDBContent<TChild> :IEntity
        where TChild : LiteDBContent<TChild>
    {
        public ObjectId Id { get; set; }
        public string ExternalId { get; set; }
        public string Value { get; internal set; }
        public List<TChild> Children { get; internal set; }
        public string Name { get; internal set; }
    }
}
