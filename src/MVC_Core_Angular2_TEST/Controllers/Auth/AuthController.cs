using ACME.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Controllers.Auth
{

    [Route("")]
    public class AuthController : Controller
    {
        private IProductRepository _repository;

        public AuthController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("api/authenticate")]
        public async Task<IActionResult> Post([FromBody] Product theProduct)
        {
            return Ok();
        }
    }
    //[Route("/api/tokenauth")]
    //public class AuthController : Controller
    //{
    //    public AuthController(SignIn)
    //    {

    //    }

    //    public async Task<ClaimsIdentity> GetIdentity(string email, string password)
    //    {
    //        var result=await _

    //    }
    //}
}
