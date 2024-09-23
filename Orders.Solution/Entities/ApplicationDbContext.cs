using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
        }

        public ApplicationDbContext() { }

        public virtual DbSet<Order> Order { get; set; }

        public virtual DbSet<OrderItem> OrderItem { get; set; }
    }
}
