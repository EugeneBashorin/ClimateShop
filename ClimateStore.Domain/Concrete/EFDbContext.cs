using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimateStore.Domain.Entities;
using System.Data.Entity;

namespace ClimateStore.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
