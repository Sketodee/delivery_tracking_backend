using Microsoft.EntityFrameworkCore;

namespace DeliveryTracking.Data
{
    public class DataContext : DbContext 
    {
        public DbSet<Test> Tests { get; set; }  
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }  

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Test>().HasData(
                new Test()
                {
                    Id = 1,
                    Name = "Shuttle Discovery",
                },
                 new Test()
                 {
                     Id = 2,
                     Name = "Kilimanjaro",
                 },
                  new Test()
                  {
                      Id = 3,
                      Name = "The Coliseum",
                  }
            );

        }
    }
}
