using gamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GamesContext _context;

        public UserRepository(GamesContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return false;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    
    }
}
