using APIProject6.WebAPI.Context;
using APIProject6.WebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly APIContext _context;
        public ChefsController(APIContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult ChefList()
        {
            var values = _context.Chefs.ToList();
            return Ok(values);
        }

        [HttpPost]

        public IActionResult CreateChef(Chef chef)
        {
            _context.Chefs.Add(chef);
            _context.SaveChanges();
            return Ok("The chef has been added to system. ");
        }

        [HttpDelete]

        public IActionResult DeleteChef(int id)
        {
            var value = _context.Chefs.Find(id);
            if (value == null)
                return NotFound("Chef not found.");

            _context.Chefs.Remove(value);
            _context.SaveChanges();
            return Ok($"Chef '{value}' has been deleted from the system.");
        }

        [HttpGet("GetChef")]

        public IActionResult GetChef(int id)
        {
            return Ok(_context.Chefs.Find(id));
        }

        [HttpPut]

        public IActionResult UpdateChef(Chef id)
        {
            _context.Chefs.Update(id);
            _context.SaveChanges();
            return Ok("The Chef has been updated.");
        }
    }
}
