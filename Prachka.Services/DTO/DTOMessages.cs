using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prachka.Services.DTO
{
    public class DTOMessages
    {
        public List<Guid> ClientIds { get; set; }
        public string Text { get; set; }
        public string Subject { get; set; }
    }
}
