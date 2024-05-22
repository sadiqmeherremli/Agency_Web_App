using Microsoft.AspNetCore.Identity;

namespace Agency_Web_App.Models
{
    public class User : IdentityUser
    {

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
