using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public class Database : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        //public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Plant> Plants { get; set; }
        //public DbSet<Humidity> Humidities { get; set; }
        //public DbSet<COTwo> CoTwos { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        public Database()
        {
        }

        public Database(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=tcp:sep4dataserver-azure.database.windows.net,1433;Initial Catalog=sep4-db;Persist Security Info=False;User ID=sep4admin;Password=Password@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}