using System;
using System.Collections.Generic;
using System.Text;
using ChaosCMS.Models.Pages;
using LiteDB;

namespace ChaosCMS.LiteDB.Models
{
    public class LiteDBPage : IEntity
    {
        public ObjectId Id { get; set; }
        public string ParentId { get; set; }
        public string Origin { get; set; }
        public string Name { get;  set; }
        public int StatusCode { get; set; }
        public string Url { get;  set; }
        public string PageType { get;  set; }
        public string Template { get;  set; }
        public string Host { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Dictionary<string, string> Children { get; set; } = new Dictionary<string, string>();
        public IList<Content> Content { get; set; } = new List<Content>();
    }
}
