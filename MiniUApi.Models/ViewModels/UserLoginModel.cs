using System.ComponentModel.DataAnnotations;

namespace MiniUApi.Models.ViewModels
{
    public class UserLoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}