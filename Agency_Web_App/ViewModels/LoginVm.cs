using System.ComponentModel.DataAnnotations;

namespace Agency_Web_App.ViewModels
{
    public class LoginVm
    {

        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
