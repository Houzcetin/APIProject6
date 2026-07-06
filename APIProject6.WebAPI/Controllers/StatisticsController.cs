using APIProject6.WebAPI.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly APIContext _context;
        public StatisticsController(APIContext context)
        {
            _context = context;
        }
        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            var value =_context.Products.Count();
            return Ok(value);
        }
        [HttpGet("ReservationCount")]
        public IActionResult ReservationCount()
        {
            var value = _context.Reservations.Count();  
            return Ok(value);
        }
        [HttpGet("ChefCount")]
        public IActionResult ChefCount()
        {
            var value = _context.Chefs.Count();
            return Ok(value);
        }
        [HttpGet("TotalGuestCount")]
        public IActionResult TotalGuestCount()
        {
            var value = _context.Reservations.Sum(x => x.CountOfPeople);
            return Ok(value);
        }
    }
}
