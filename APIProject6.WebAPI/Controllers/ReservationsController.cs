using APIProject6.WebAPI.Context;
using APIProject6.WebAPI.Dtos.ReservationDtos;
using APIProject6.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace APIProject6.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly APIContext _context;
        private readonly IMapper _mapper;

        public ReservationsController(APIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ReservationList()
        {
            var values = _context.Reservations.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateReservation(CreateReservationDto createReservationDto)
        {
            var value = _mapper.Map<Reservation>(createReservationDto);
            _context.Reservations.Add(value);
            _context.SaveChanges();
            return Ok("The Reservation has been added.");
        }

        [HttpDelete]

        public IActionResult DeleteReservation(int id)
        {
            var value = _context.Reservations.Find(id);
            _context.Reservations.Remove(value);
            _context.SaveChanges();
            return Ok("Reservation has been deleted.");
        }

        [HttpGet("GetReservation")]
        public IActionResult GetReservation(int id)
        {
            var value = _context.Reservations.Find(id);

            return Ok(value);
        }

        [HttpPut]

        public IActionResult UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            var value = _mapper.Map<Reservation>(updateReservationDto);
            _context.Reservations.Update(value);
            _context.SaveChanges();
            return Ok("The Reservation has been updated.");
        }

        [HttpGet("GetTotalReservationCount")]

        public IActionResult GetTotalReservationCount()
        {
            var totalCount = _context.Reservations.Count();
            return Ok(totalCount);
        }

        [HttpGet("GetTotalCustomerCount")]
        public IActionResult GetTotalCustomerCount()
        {
            var totalCount = _context.Reservations.Sum(x=>x.CountOfPeople);
            return Ok(totalCount);
        }

        [HttpGet("GetPendingReservation")]
        public IActionResult GetPendingReservation()
        {
            var totalCount = _context.Reservations.Where(x=>x.ReservationStatus== "Waiting for Approval").Count();
            return Ok(totalCount);
        }
        [HttpGet("GetApprovedReservation")]
        public IActionResult GetApprovedReservation()
        {
            var totalCount = _context.Reservations.Where(x => x.ReservationStatus == "Approved").Count();
            return Ok(totalCount);
        }

        [HttpGet("GetReservationChart")]
        public async Task<IActionResult> GetReservationChart(int monthCount = 4)
        {
            if (monthCount <= 0)
            {
                monthCount = 4;
            }

            var latestReservationDate = await _context.Reservations
    .MaxAsync(x => (DateTime?)x.ReservationDate) ?? DateTime.Today;

            var startMonth = new DateTime(latestReservationDate.Year, latestReservationDate.Month, 1)
                .AddMonths(-(monthCount - 1));

            var endMonth = startMonth.AddMonths(monthCount);

            var reservations = await _context.Reservations
                .AsNoTracking()
                .Where(x => x.ReservationDate >= startMonth && x.ReservationDate < endMonth)
                .Select(x => new
                {
                    x.ReservationDate,
                    x.ReservationStatus,
                    x.CountOfPeople,
                    x.Email
                })
                .ToListAsync();

            static bool IsStatus(string? value, string target)
            {
                return string.Equals(
                    value?.Trim(),
                    target,
                    StringComparison.OrdinalIgnoreCase
                );
            }

            var months = Enumerable.Range(0, monthCount)
                .Select(i => startMonth.AddMonths(i))
                .ToList();

            var groupedReservations = reservations
                .GroupBy(x => new DateTime(x.ReservationDate.Year, x.ReservationDate.Month, 1))
                .ToDictionary(g => g.Key, g => g.ToList());

            var result = new ResultReservationChartDto
            {
                Labels = months
                    .Select(x => x.ToString("MMM yyyy", CultureInfo.InvariantCulture))
                    .ToList(),

                Approved = months
                    .Select(month =>
                        groupedReservations.TryGetValue(month, out var items)
                            ? items.Count(x => IsStatus(x.ReservationStatus, "Approved"))
                            : 0
                    )
                    .ToList(),

                WaitingForApproval = months
                    .Select(month =>
                        groupedReservations.TryGetValue(month, out var items)
                            ? items.Count(x => IsStatus(x.ReservationStatus, "Waiting for Approval"))
                            : 0
                    )
                    .ToList(),

                Cancelled = months
                    .Select(month =>
                        groupedReservations.TryGetValue(month, out var items)
                            ? items.Count(x => IsStatus(x.ReservationStatus, "Cancelled"))
                            : 0
                    )
                    .ToList(),

                Completed = months
                    .Select(month =>
                        groupedReservations.TryGetValue(month, out var items)
                            ? items.Count(x => IsStatus(x.ReservationStatus, "Completed"))
                            : 0
                    )
                    .ToList(),

                NoShow = months
                    .Select(month =>
                        groupedReservations.TryGetValue(month, out var items)
                            ? items.Count(x => IsStatus(x.ReservationStatus, "No Show"))
                            : 0
                    )
                    .ToList(),

                TotalReservations = reservations.Count,

                TotalGuests = reservations.Sum(x => x.CountOfPeople),

                ApprovedReservations = reservations
                    .Count(x => IsStatus(x.ReservationStatus, "Approved")),

                WaitingReservations = reservations
                    .Count(x => IsStatus(x.ReservationStatus, "Waiting for Approval")),

                CancelledReservations = reservations
                    .Count(x => IsStatus(x.ReservationStatus, "Cancelled")),

                CompletedReservations = reservations
                    .Count(x => IsStatus(x.ReservationStatus, "Completed")),

                NoShowReservations = reservations
                    .Count(x => IsStatus(x.ReservationStatus, "No Show")),

                NewCustomers = reservations
                    .Where(x => !string.IsNullOrWhiteSpace(x.Email))
                    .Select(x => x.Email.Trim().ToLower())
                    .Distinct()
                    .Count()
            };

            return Ok(result);
        }


    }
}
