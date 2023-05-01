﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Data.Entities
{
    /// <summary>
    /// Услуги
    /// </summary>
    public class Service
    {
        [Key]
        public Guid ServiceId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<OrderServices> OrderServices { get; set; }

    }
}
