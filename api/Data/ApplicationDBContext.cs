using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }


        public DbSet<Models.Player> Players { get; set; }
        public DbSet<Models.Score> Scores { get; set; }
        public DbSet<Models.Comment> Comments { get; set; }
    }
}