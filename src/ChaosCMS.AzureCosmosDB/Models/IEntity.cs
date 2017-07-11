using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.AzureCosmosDB.Models
{
    public interface IEntity
    {
        string Id { get; set; }
        string Origin { get; set; }
    }
}
