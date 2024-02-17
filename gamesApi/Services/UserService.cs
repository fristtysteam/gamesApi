using gamesApi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace gamesApi.Services
{
    public class UserService : IUserService
    {
        private readonly GamesContext _context;
        public UserService(GamesContext context)
        {
            _context = context;
        }

        public User Get(UserLogin userLogin)
        {
            User user = _context.User.FirstOrDefault(o => o.Username.Equals
            (userLogin.Username, StringComparison.OrdinalIgnoreCase) &&
            o.Password.Equals(userLogin.Password));
            return user;
        }
        
    }
}
