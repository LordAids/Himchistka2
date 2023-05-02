using Himchistka.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.DTO
{
    public class DTOUpsertOrder
    {
        public Guid? Id { get; set; }
        public Guid? ClientId { get; set; }
        public List<Guid>? Services { get;set; }
    }
}
