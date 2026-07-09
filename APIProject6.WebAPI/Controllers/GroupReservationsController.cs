using APIProject6.WebAPI.Context;
using APIProject6.WebAPI.Dtos.GroupReservationDtos;
using APIProject6.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupReservationsController : ControllerBase
    {
        private readonly APIContext _context;
        private readonly IMapper _mapper;
        public GroupReservationsController(APIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GroupReservationList()
        {
            var values = _context.GroupReservations.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateGroupReservation(CreateGroupReservationDto createGroupReservationDto)
        {
            var value = _mapper.Map<GroupReservation>(createGroupReservationDto);
            _context.GroupReservations.Add(value);
            _context.SaveChanges();
            return Ok("The GroupReservation has been added.");
        }

        [HttpDelete]

        public IActionResult DeleteGroupReservation(int id)
        {
            var value = _context.GroupReservations.Find(id);
            _context.GroupReservations.Remove(value);
            _context.SaveChanges();
            return Ok("GroupReservation has been deleted.");
        }

        [HttpGet("GetGroupReservation")]
        public IActionResult GetGroupReservation(int id)
        {
            var value = _context.GroupReservations.Find(id);

            return Ok(value);
        }

        [HttpPut]

        public IActionResult UpdateGroupReservation(UpdateGroupReservationDto updateGroupReservationDto)
        {
            var value = _context.GroupReservations.Find(updateGroupReservationDto.GroupReservationId);
            if (value == null)
            {
                return NotFound("GroupReservation not found.");
            }

            // Copy only the editable fields so Email / PersonCount are preserved
            value.ReservationOwner = updateGroupReservationDto.ReservationOwner;
            value.GroupTitle = updateGroupReservationDto.GroupTitle;
            value.ReservationDate = updateGroupReservationDto.ReservationDate;
            value.LastProcessDate = updateGroupReservationDto.LastProcessDate;
            value.Priority = updateGroupReservationDto.Priority;
            value.Details = updateGroupReservationDto.Details;
            value.ReservationStatus = updateGroupReservationDto.ReservationStatus;

            _context.SaveChanges();
            return Ok("The GroupReservation has been updated.");
        }
    }
}
