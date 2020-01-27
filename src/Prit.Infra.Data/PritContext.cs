using Microsoft.EntityFrameworkCore;
using Prit.Domain.Aggregates;
using Prit.Infra.Data.EntityMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prit.Infra.Data
{
    public class PritContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public PritContext(DbContextOptions<PritContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
        }
    }
}
