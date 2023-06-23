using JPGStockServer.Entities;
using JPGStockServer.Entities.Notification;
using Microsoft.EntityFrameworkCore;

namespace JPGStockServer.Helpers
{
    public class DataContext : DbContext
    {
        //protected readonly IConfiguration Configuration;

        //public DataContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to sql server database
        //    options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        //}

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<NotificationRequest> Notification { get; set; }
    }
}
