using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ACME.Models
{
   // public class ProductContext : DbContext
   public class ProductContext : IdentityDbContext<ProductUser>
    {
        private IConfigurationRoot _config;

        public ProductContext(IConfigurationRoot config,DbContextOptions options):base(options)
        {
            _config = config;
        }

        public DbSet<Product>Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //Inject IConfigurationRoot on constructor in order to pass dynamically the connection string to config.json instead of hardcoding it.
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:AcmeContextConnection"]);
        }
    }
}
