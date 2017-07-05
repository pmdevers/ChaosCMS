using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.LiteDB.Models
{
    public class LiteDBUser : IEntity
    {
        public ObjectId Id { get; set; }
        public string Origin { get; set; }

        public int StatusCode { get; set; }
        public string NormalizedName { get; internal set; }
        public string UserName { get; internal set; }
    }
}
