using Domain.Common;
using Domain.Movies;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistance.Identity
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        private readonly List<Rating> Ratings = [];
        private readonly List<Review> Reviews = [];


        public ApplicationUser() : base() { }
        public ApplicationUser(string userName, string email) : base(userName)
        {
            UserName = userName;
            Email = email;
        }
    }
}
