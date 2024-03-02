using System.ComponentModel.DataAnnotations;

namespace _17_KianHong_ESD_Project.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is Required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
