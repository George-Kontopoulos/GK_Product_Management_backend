//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ACME.Models
//{
//    public class ProductContextSeedData
//    {
//        private ProductContext _context;
//        private UserManager<ProductUser> _userManager;
//        // private UserManager<WorldUser> _userManager;

//        public ProductContextSeedData(ProductContext context,UserManager <ProductUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }
//        public async Task EnsureSeedData()
//        {
//            if (await _userManager.FindByEmailAsync("gk@acme.com") == null)
//            {
//                var user = new ProductUser()
//                {
//                    UserName = "GK",
//                    Email = "gk@acme.com",
//                };
//                await _userManager.CreateAsync(user, "gk$7");
//            }

//            if (!_context.Products.Any())
//            {
//                //Set sample data
//                var hammer = new Product()
//                {
//                    //Id = 5,
//                    name="Hammer",
//                    code= "tbx-0048",
//                    available=new DateTime(2016,5,21),
//                    price=8.90M,
//                    rating=4.8f
//                };
             
//                _context.Products.Add(hammer);
               
//                var saw = new Product()
//                {
//                    //Id = 8,
//                    name = "saw",
//                    code = "tbx-0022",
//                    available = new DateTime(2016, 5, 15),
//                    price = 11.55M,
//                    rating = 3.7f
//                };

//                _context.Products.Add(saw);

//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}
