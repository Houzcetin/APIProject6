using APIProject6.WebAPI.Context;
using APIProject6.WebAPI.Dtos.NotificationDtos;
using APIProject6.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly APIContext _context;

        public NotificationsController(IMapper mapper, APIContext contex)
        {
            _mapper = mapper;
            _context = contex;
        }

        [HttpGet]

        public IActionResult NotificationList()
        {
            var values = _context.Notifications.ToList();
            return Ok(_mapper.Map<List<ResultNotificationDto>>(values));
        }

        [HttpPost]

        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var value = _mapper.Map<Notification>(createNotificationDto);
            _context.Notifications.Add(value);
            _context.SaveChanges();
            return Ok("It has been added");
        }

        [HttpDelete]

        public IActionResult DeleteNotification(int id)
        {

            var value = _context.Notifications.Find(id);
            _context.Notifications.Remove(value);
            _context.SaveChanges();
            return Ok("Deletion successfull.");
        }

        [HttpGet("GetNotification")]

        public IActionResult GetNotification(int id)
        {

            var value = _context.Notifications.Find(id);
            return Ok(_mapper.Map<GetNotificationByIdDto>(value));


        }

        [HttpPut]

        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            var value = _mapper.Map<Notification>(updateNotificationDto);
            _context.Notifications.Update(value);
            _context.SaveChanges();
            return Ok("It has been updated");
        }
    }
}
