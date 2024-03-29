﻿using Himchistka.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } 
        public ICollection<Place> Places { get; set;}
    }
}
