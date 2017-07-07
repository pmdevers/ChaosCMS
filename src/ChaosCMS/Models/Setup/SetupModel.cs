using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Models.Setup
{
    public class SetupModel
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public bool HomePage { get; set; }
        public bool LoginPage { get; set; }
    }
}
