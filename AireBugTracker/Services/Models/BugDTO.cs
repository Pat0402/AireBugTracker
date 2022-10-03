using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class BugDTO
    {
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        public string Details { get; set; }
        public bool IsOpen { get; set; }
    }
}
