using LiteDB;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ChaosCMS.LiteDB.Models
{
    public class LiteDBUser : IEntity
    {
        public LiteDBUser()
        {
            this.Id = ObjectId.NewObjectId();
        }
        public ObjectId Id { get; set; }
        public string Origin { get; set; }

        public int StatusCode { get; set; }
        public string NormalizedName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool HasPassword { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEndDate { get; set; }
        public int AccessFailedCount { get; set; }

        public IList<UserLoginInfo> Logins { get; set; } = new List<UserLoginInfo>();
        public IList<string> Roles { get; set; } = new List<string>();
        public IList<Claim> Claims { get; set; } = new List<Claim>();
        public IList<UserToken> Tokens { get; set; }

    }


    public class UserToken
    {
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
