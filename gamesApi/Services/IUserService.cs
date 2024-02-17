using gamesApi.Models;

namespace gamesApi.Services
{
    public interface IUserService 
    {
        public User Get(UserLogin userLogin);

    }
}
