using Microsoft.AspNetCore.Identity;
using TheCyberSecurityQuiz.Data.Models;

namespace TheCyberSecurityQuiz.UI.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<UserResult> UserResults { get; set; } // many-to-many med result
    }

}
