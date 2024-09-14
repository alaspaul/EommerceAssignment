using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class TradeDbContext : DbContext
    {
        public TradeDbContext(DbContextOptions<TradeDbContext> options) : base(options)
        {
        }

        public DbSet<BuyOrder> BuyOrders { get; set; }

        public DbSet<SellOrder> SellOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");


            string buyOrdersString = System.IO.File.ReadAllText("BuyOrders.json");
            List<BuyOrder> buyOrders = System.Text.Json.JsonSerializer.Deserialize<List<BuyOrder>>(buyOrdersString);

            foreach (BuyOrder buyOrder in buyOrders)
                modelBuilder.Entity<BuyOrder>().HasData(buyOrder);


            string sellOrdersString = System.IO.File.ReadAllText("SellOrders.json");
            List<SellOrder> sellOrders = System.Text.Json.JsonSerializer.Deserialize<List<SellOrder>>(sellOrdersString);

            foreach (SellOrder sellOrder in sellOrders)
                modelBuilder.Entity<SellOrder>().HasData(sellOrder);

        }

    }
}
