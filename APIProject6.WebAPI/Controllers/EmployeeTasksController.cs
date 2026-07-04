using APIProject6.WebAPI.Context;
using APIProject6.WebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly APIContext _context;
        public EmployeeTasksController(APIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult EmployeeTaskList()
        {
            var values = _context.EmployeeTasks.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateEmployeeTask(EmployeeTask EmployeeTask)
        {
            _context.EmployeeTasks.Add(EmployeeTask);
            _context.SaveChanges();
            return Ok("The Employee Task has been added.");
        }

        [HttpDelete]

        public IActionResult DeleteEmployeeTask(int id)
        {
            var value = _context.EmployeeTasks.Find(id);
            _context.EmployeeTasks.Remove(value);
            _context.SaveChanges();
            return Ok("Employee Task has been deleted.");
        }

        [HttpGet("GetEmployeeTask")]
        public IActionResult GetEmployeeTask(int id)
        {
            var value = _context.EmployeeTasks.Find(id);

            return Ok(value);
        }

        [HttpPut]

        public IActionResult UpdateEmployeeTask(EmployeeTask EmployeeTask)
        {
            _context.EmployeeTasks.Update(EmployeeTask);
            _context.SaveChanges();
            return Ok("The Employee Task has been updated.");
        }
    }
}
