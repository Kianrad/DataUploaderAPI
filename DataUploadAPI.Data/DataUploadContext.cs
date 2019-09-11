using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using DataUploadAPI.Data.Configurations;
using DataUploadAPI.Data.Entities;

namespace DataUploadAPI.Data
{
    public class DataUploadContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public static long InstanceCount;

        public DataUploadContext(DbContextOptions options) : base(options) 
            => Interlocked.Increment(ref InstanceCount);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ProductConfiguration(modelBuilder.Entity<Product>());
            
            new CategoryConfiguration(modelBuilder.Entity<Category>());
        }
    }
}