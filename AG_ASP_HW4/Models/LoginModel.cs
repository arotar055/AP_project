using System.ComponentModel.DataAnnotations;

namespace AP_project.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}