using Microsoft.EntityFrameworkCore;
using SIBKMNET_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBKMNET_Mvc.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<Province> Provinces { get; set; }
        
        public DbSet<Region> Regions { get; set; }
    }
}
