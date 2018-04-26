using Microsoft.EntityFrameworkCore;
using System;
using TomatosAPI.Models;

namespace TomatosAPI.Db
{
    public class TomatoDb : DbContext
    {
        // Reference our tomato table using this
        public DbSet<Tomato> Tomatos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./Tomatos.db");
        }
    }
}
