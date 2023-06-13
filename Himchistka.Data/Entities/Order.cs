using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Data.Entities
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public decimal Cost { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public Place? Place { get; set; }
        public virtual ICollection<Service> Services{ get; set; } 
    }

    public enum OrderStatus
    {
        New = 1, //Новый заказ
        InProgress = 2, //В работе
        Waiting = 3, //Ожидание заказчика
        Canceled = 4, //Выполнено
    }
}
