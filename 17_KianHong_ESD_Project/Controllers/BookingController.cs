using _17_KianHong_ESD_Project.Data;
using _17_KianHong_ESD_Project.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _17_KianHong_ESD_Project.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }
       // [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            var bookings = _context.Bookings.ToList();
            return Ok(bookings);
        }
        // GET: api/<BookingController>
        [HttpGet("{bookingId}")]
        public IActionResult GetBookingById(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingID == bookingId);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // POST api/<BookingController>
        [HttpPost]
        public IActionResult CreateBooking([FromBody] BookingInfo newBooking)
        {
            _context.Bookings.Add(newBooking);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBookingById), new { bookingId = newBooking.BookingID }, newBooking);
        }

        [HttpPut("{bookingId}")]
        public IActionResult UpdateBooking(int bookingId, [FromBody] BookingInfo updatedBooking)
        {
            var existingBooking = _context.Bookings.FirstOrDefault(b => b.BookingID == bookingId);

            if (existingBooking == null)
                return NotFound();

            existingBooking.Description = updatedBooking.Description;
            existingBooking.BookingDateFrom = updatedBooking.BookingDateFrom;
            existingBooking.BookingDateTo = updatedBooking.BookingDateTo;
            existingBooking.BookingBy = updatedBooking.BookingBy;
            existingBooking.BookingStatus = updatedBooking.BookingStatus;

            _context.SaveChanges();

            return Ok(existingBooking);
        }
        [HttpGet("byDate")]
        public IActionResult GetBookingsByDate(DateTime bookingDate)
        {
            var bookings = _context.Bookings
                .Where(b => b.BookingDateFrom.Date <= bookingDate.Date && b.BookingDateTo.Date >= bookingDate.Date)
                .ToList();

            return Ok(bookings);
        }
        // DELETE api/<BookingController>/5
        [HttpDelete("{bookingId}")]
        public IActionResult DeleteBooking(int bookingId)
        {
            var bookingToDelete = _context.Bookings.FirstOrDefault(b => b.BookingID == bookingId);

            if (bookingToDelete == null)
                return NotFound();

            _context.Bookings.Remove(bookingToDelete);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
