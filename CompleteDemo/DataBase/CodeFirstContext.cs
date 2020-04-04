using CompleteDemo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteDemo.DataBase
{
    public class CodeFirstContext : DbContext
    {
        public CodeFirstContext()
        {

        }
        public CodeFirstContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ClassInfo> ClassInfo { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;userid=root;pwd=123456;port=3306;database=wxw;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
