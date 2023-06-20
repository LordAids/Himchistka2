using Himchistka.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.DTO
{
    public class DTOOrders
    {
        public Guid? Id { get; set; }
        public int Number { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? PlaceId { get; set; }
        public string? ClientName { get; set; }
        public int? Status { get; set; }
        public decimal? Cost { get; set; }   
        public string Comment { get; set; }
        public DateTime? CreationTime { get; set; }
        public List<Guid>? Services { get;set; }
    }
    public class StatusDTO
    {
        public string Text { get; set; }
        public int Value { get;set; }
    }
}
