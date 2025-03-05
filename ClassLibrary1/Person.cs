using Microsoft.AspNetCore.Identity;

namespace BookShop.Models
{
    public class Person : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
