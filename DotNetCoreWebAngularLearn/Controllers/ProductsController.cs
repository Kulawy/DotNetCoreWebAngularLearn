using DotNetCoreWebAngularLearn.Data;
using DotNetCoreWebAngularLearn.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebAngularLearn.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]   // wspomaga dokumentację, okresla nasz controller jako ApiController
    [Produces("application/json")]  // wspomaga dokumentację, wskazuje na to co produkuje metoda / jakie informacje dostanie przegladarka
    public class ProductsController : ControllerBase
    {
        private readonly IMyRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMyRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]  // wspomaga dokumentację, wskazuje na to co produkuje metoda / jakie informacje dostanie przegladarka
        [ProducesResponseType(400)] // wspomaga dokumentację, wskazuje na to co produkuje metoda / jakie informacje dostanie przegladarka
        public ActionResult<IEnumerable<Product>> Get()
        {

            try
            {
                //return _repository.GetAllProducts();
                return Ok(_repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                //return null;
                return BadRequest("Failed to get products");
            }

        }

        //nie zwracamy IEnumerable<Product> bo tak nie ma być i to jest źle bo servisy nie dają rady tylko dajemy IActionResult
        //ale z tym wciąż coś nie tak (chyba chodzi o to żeby zwracało konkretną listę produktów) więc najlepiej jak damy ActionResult<IEnumerable<Product>>,
        //a nasa klasa nie będzie rozszerzać Controller tylko ControllerBase i musimy dodać [ApiController]

    }
}
