using Himchistka.Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Data.Entities
{
    /// <summary>
    /// Точка химчистки
    /// </summary>
    public class Place
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ApplicationUser> Employee { get; set; }
    }
}
