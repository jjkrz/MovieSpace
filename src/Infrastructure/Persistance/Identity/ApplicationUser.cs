using Domain.Common;
using Domain.Movies;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistance.Identity
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        private readonly List<Rating> Ratings = [];

        public ApplicationUser() : base() { }
        public ApplicationUser(string userName, string email) : base(userName)
        {
            UserName = userName;
            Email = email;
        }
    }
}
