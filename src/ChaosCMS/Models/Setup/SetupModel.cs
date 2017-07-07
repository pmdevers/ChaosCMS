using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Models.Setup
{
    public class SetupModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Domain { get; set; }
        public string Description { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool HomePage { get; set; }
        public bool LoginPage { get; set; }
    }
}
