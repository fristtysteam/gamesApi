namespace gamesApi.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> DeleteUser(int id);

    }
}
