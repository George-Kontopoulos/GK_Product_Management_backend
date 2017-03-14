using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Models
{
    public class ProductRepository : IProductRepository
    {
       // private ILogger _logger;
        private ProductContext _context;
        public ProductRepository(ProductContext context)
        {
            _context = context;
           // _logger = logger;
        }

        public void AddProduct(Product product)
        {
            _context.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Remove(product);
        }

        public bool DeleteAllProducts()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM Products");
            return true;
        }

        public void UpdateProduct(Product product)
        {
            // _context.Update(product);
            _context.Update(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
         //   _logger.LogInformation("Retrieving All Products from database.");
            return _context.Products.ToList();
        }

        public Product GetByDateAvailable(DateTime dateavailable)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(string productname)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(product => product.id == id);
        }

        public Product GetProductByRating(float productrating)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
          return (await _context.SaveChangesAsync())>0;
        }
    }
}
