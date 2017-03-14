using ACME.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Controllers.Api
{
    [EnableCors("localhost")]
    [Route("/api/products")]
    public class ProductsController :Controller
    {

        private IProductRepository _repository;
      //  private ILogger _logger;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
            //_logger = logger;
        }
      //  [Authorize]
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllProducts();
                
               return Ok(Mapper.Map<IEnumerable<Product>>(results));
            }
            catch (Exception ex)
            {
             //   _logger.LogError($"Failed to get All Products:{ex}");
                return BadRequest("Error occured!");
            }
        }

        [HttpGet("getproduct/{id}")]
        public IActionResult GetProductbyId(int id)
        {
            try
            {
               System.Diagnostics.Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
               // Task.Delay(10000);
               var result= _repository.GetProductById(id);
                return Ok(Mapper.Map<Product>(result));
                //return Ok("product added");
                //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured retrieving product by id.");
            }
        }

       // [Authorize]
        [HttpPost("addproduct")]
        [EnableCors("localhost")]
        public async Task<IActionResult> Post([FromBody] Product theProduct)
        {
            //if (ModelState.IsValid)
            //{
                //Save to the database

               // var newProduct = Mapper.Map<Product>(theProduct);
                _repository.AddProduct(theProduct);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/products/{theProduct.name}", theProduct);
                }
           // }
            return BadRequest("Failed to save changes to the database!");
            //return BadRequest(ModelState);
        }

        //[HttpOptions]
        //[HttpDelete("deleteproduct/products")]
        //public async Task<IActionResult> Delete([FromHeader] Product deletedproduct)
        //{
        //    _repository.DeleteProduct(deletedproduct);

        //    if (await _repository.SaveChangesAsync())
        //    {
        //        return Created($"api/products/{deletedproduct.name}", deletedproduct);
        //    }

        //    return BadRequest("Failed to save changes to the database!");

        //}

        [HttpDelete("deleteproduct/{id}")]
        public async Task<IActionResult> Delete([FromHeader]Product deletedproduct)
        {
            Product deleteProduct = _repository.GetProductById(deletedproduct.id);

            _repository.DeleteProduct(deleteProduct);

            if (await _repository.SaveChangesAsync())
            {   
                
                return Created($"api/products/{deletedproduct.name}", deletedproduct);
               
            }

            return BadRequest("Failed to save changes to the database!");
        }

        [HttpDelete("deleteallproducts")]
        public async Task<IActionResult> DeleteAllProducts()
        {
            try
            {
                if (_repository.DeleteAllProducts())
                {
                    System.Diagnostics.Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    await _repository.SaveChangesAsync();
                    await Task.Delay(100);
                    System.Diagnostics.Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    return Ok("Product List was succesfully deleted!");
                }
                else { return BadRequest("Something went wrong..."); }
            }
            catch (Exception ex)
            {
                //   _logger.LogError($"Failed to get All Products:{ex}");
                return BadRequest("Error occured!Product List was not deleted!");
            }
        }

        [HttpPut("updateproduct")]
        [EnableCors("localhost")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product theProduct)
        {
            _repository.UpdateProduct(theProduct);
            if (await _repository.SaveChangesAsync())
            {
                return Ok("The product has been updated successfully!");
            }
            return BadRequest("Failed to update the product...");
        }

    }
}
