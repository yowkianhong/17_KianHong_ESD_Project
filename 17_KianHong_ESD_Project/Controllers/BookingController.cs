using _17_KianHong_ESD_Project.Data;
using _17_KianHong_ESD_Project.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _17_KianHong_ESD_Project.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Bookings);
        }
        [Authorize(Roles = UserRoles.User)]
        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int? id) 
        { 
            var booking = _context.Bookings.FirstOrDefault(e => e.BookingID == id);
            if (booking == null)
                return Problem(detail:"Member with id " + " is not found.", statusCode:404);

            return Ok(booking);
            
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public IActionResult Post(BookingInfo booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return CreatedAtAction("GetAll", new {id = booking.BookingID}, booking);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public IActionResult Put(int? id, BookingInfo booking)
        {
            var entity = _context.Bookings.FirstOrDefault(e => e.BookingID== id);
            if (entity == null)
                return Problem(detail: "Member with id " + id + "is not found .", statusCode: 404);

            entity.Description = booking.Description;
            entity.BookingDateFrom = booking.BookingDateFrom;
            entity.BookingDateTo = booking.BookingDateTo;
            entity.BookingBy = booking.BookingBy;
            entity.BookingStatus = booking.BookingStatus;

            _context.SaveChanges();

            return Ok(entity);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public IActionResult Delete(int? id, BookingInfo booking)
        {
            var entity = _context.Bookings.FirstOrDefault(e => e.BookingID == id);
            if (entity == null)
                return Problem(detail: "Member with id " + id + "is not found .", statusCode: 404);


            _context.Bookings.Remove(entity);
            _context.SaveChanges();

            return Ok(entity);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("byDate")]
        public IActionResult GetBookingsByDate(DateTime bookingDate)
        {
            var bookings = _context.Bookings
                .Where(b => b.BookingDateFrom.Date <= bookingDate.Date && b.BookingDateTo.Date >= bookingDate.Date)
                .ToList();

            return Ok(bookings);
        }
        //testing
    }
}
