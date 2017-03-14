using ACME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProductByName(string productname);

        Product GetProductById(int id);

        Product GetProductByRating(float productrating);

        Product GetByDateAvailable(DateTime dateavailable);

        void AddProduct(Product product);

        void DeleteProduct(Product product);

        bool DeleteAllProducts();

        void UpdateProduct(Product product);

        Task<bool> SaveChangesAsync();
    }
}
