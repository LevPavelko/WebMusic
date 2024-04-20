
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace WebMusic.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Не корректно веден Email адрес")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Password)]
        
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}
