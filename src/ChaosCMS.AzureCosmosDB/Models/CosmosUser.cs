using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ChaosCMS.AzureCosmosDB.Models
{
    public class CosmosUser : IEntity
    {
        public string Id { get; set; }
        public string Origin { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUsername { get;  set; }
        public int AccessFailedCount { get;  set; }
        public string Email { get;  set; }
        public bool EmailConfirmed { get;  set; }
        public bool LockoutEnabled { get;  set; }
        public DateTimeOffset? LockoutEndDate { get;  set; }
        public string PasswordHash { get;  set; }
        public string PhoneNumber { get;  set; }
        public bool PhoneNumberConfirmed { get;  set; }
        public string SecurityStamp { get;  set; }
        public bool TwoFactorEnabled { get;  set; }
        public string Username { get;  set; }

        public IList<Claim> Claims { get; set; } = new List<Claim>();
        public IList<UserLoginInfo> Logins { get; set; } = new List<UserLoginInfo>();
        public IList<string> Roles { get; set; } = new List<string>();

        public Dictionary<string, string> Tokens = new Dictionary<string, string>();
    }
}
