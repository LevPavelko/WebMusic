using System.ComponentModel.DataAnnotations;

namespace WebMusic.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
