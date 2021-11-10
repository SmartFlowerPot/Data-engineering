﻿using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public class Database : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=tcp:sep4dataserver-azure.database.windows.net,1433;Initial Catalog=sep4-db;Persist Security Info=False;User ID=sep4admin;Password=Password@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}