﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseContext.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; } 
        public bool IsOpen { get; set; }
        public DateTimeOffset OpenedDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
