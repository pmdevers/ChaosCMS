using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Models.Setup
{
    /// <summary>
    /// 
    /// </summary>
    public class SetupModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Domain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool HomePage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool LoginPage { get; set; }
    }
}
