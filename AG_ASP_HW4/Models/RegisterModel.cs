using System.ComponentModel.DataAnnotations;

namespace AP_project.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string? ConfirmPassword { get; set; }
    }
}