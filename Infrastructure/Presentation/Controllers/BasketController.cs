using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasket(string id)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateBasket(BasketDto basketDto)
        {
            var basket = await serviceManager.BasketService.AddOrUpdateBasketAsync(basketDto);
            return Created("Basket Has Created Or Updated Successfully", basket);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
