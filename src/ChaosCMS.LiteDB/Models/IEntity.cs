using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.LiteDB.Models
{
    public interface IEntity
    {
        ObjectId Id { get; set; }
        string ExternalId { get; set; }
    }
}
