using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prit.Application.Interfaces;
using Prit.Portal.Models.Products;

namespace Prit.Portal.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductAppService _productAppService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductAppService productAppService, ILogger<ProductController> logger)
        {
            _productAppService = productAppService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productAppService.GetAllAsync();

            return View(products);
        }


        [HttpPost]
        public async Task<ActionResult> Upsert([FromBody]CreateOrUpdateProductViewModel product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(product);

                await _productAppService.AddOrUpdateAsync(product.AsProduct());
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery]int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(id); 

                await _productAppService.RemoveAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}