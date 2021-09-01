using Microsoft.EntityFrameworkCore;
using Rennbahn3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rennbahn3
{
    class RennbahnContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rennbahn3");
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Saison> Saisons { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
