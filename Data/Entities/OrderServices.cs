using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Data.Entities
{
    public class OrderServices
    {
        [Key]
        public Guid OrderServiceId { get; set; }
        public Guid OrderId { get; set;}
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
