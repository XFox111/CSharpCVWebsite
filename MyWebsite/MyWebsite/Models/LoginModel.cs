using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models
{
    public class LoginModel
    {
        [Key]
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
