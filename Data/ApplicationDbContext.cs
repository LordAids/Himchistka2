using Himchistka.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        DbSet<Order> Orders { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<Place> Places { get; set; }    }
}
