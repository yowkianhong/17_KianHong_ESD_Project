using System.ComponentModel.DataAnnotations;

namespace _17_KianHong_ESD_Project.Model
{
    public class BookingInfo
    {
        [Key]
        public int BookingID { get; set; }
        public string Description { get; set; }
        public DateTime BookingDateFrom { get; set; }
        public DateTime BookingDateTo { get; set; }
        public string BookingBy { get; set; }
        public string BookingStatus {  get; set; } 
    }
}
