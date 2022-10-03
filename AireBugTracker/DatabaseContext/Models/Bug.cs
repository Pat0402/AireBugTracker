using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseContext.Models
{
    public class Bug
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        public string Details { get; set; } 
        public bool IsOpen { get; set; }
        public DateTimeOffset OpenedDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
