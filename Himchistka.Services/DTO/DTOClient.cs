using Himchistka.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.DTO
{
    public class DTOClient
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime birthday { get; set; }
        public string SelectName
        {
            get
            {
                return $"{PhoneNumber} {LastName} {FirstName}";
            }
        }
    }
}
