using Microsoft.EntityFrameworkCore;
using OrderMS.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMS.Infra
{
    public class OrderDbContext: DbContext
    {
        public DbSet<OrderModel> OrderData { get; set; }

        public OrderDbContext()
        {
        }

        public OrderDbContext(DbContextOptions
<OrderDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MAHBUBAHMED; initial catalog=ordermsdb;integrated security=true;");
        }
    }
}
