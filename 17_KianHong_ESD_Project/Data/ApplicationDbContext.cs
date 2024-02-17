using _17_KianHong_ESD_Project.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace _17_KianHong_ESD_Project.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<BookingInfo>? Bookings { get; set; }
    }
}
