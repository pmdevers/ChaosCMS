using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ChaosCMS.Json.Models
{
    /// <summary>
    ///
    /// </summary>
    public class JsonUser : IEntity
    {
        /// <summary>
        ///
        /// </summary>
        public JsonUser()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        ///
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string NormalizedUserName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string NormalizedEmail { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool LockOutEnabled { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTimeOffset? LockOutEndDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int FailedAttempts { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IList<UserLoginInfo> Logins { get; set; } = new List<UserLoginInfo>();

        /// <summary>
        ///
        /// </summary>
        public IList<Claim> Claims { get; set; } = new List<Claim>();

        /// <summary>
        ///
        /// </summary>
        public IList<string> Roles { get; set; } = new List<string>();
    }
}