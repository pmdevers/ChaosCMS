using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace ChaosCMS.LiteDB.Models
{
    public class LiteDBPage : IEntity
    {
        public ObjectId Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get;  set; }
        public int StatusCode { get; set; }
        public string Url { get;  set; }
        public string PageType { get;  set; }
        public string Template { get;  set; }
        public List<string> Hosts { get; set; } = new List<string>();
    }
}
