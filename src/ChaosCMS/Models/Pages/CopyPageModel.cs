using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Models.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public class CopyPageModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string NewUrl { get; set; }
    }
}
