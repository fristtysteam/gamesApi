using gamesApi.Models;

namespace gamesApi.Repositories
{
    public interface IGamesRepository
    {
        public Task<List<GameDto>> GetGames();
        public Task<GameDto> GetGameById(int id);
        public Task<bool> DeleteGameByID(int id);
        public Task<bool> UpdateGameByID(int id, GameDto updatedGame);
        public Task<GameDto> CreateGame(GameDto newGame);




    }
}
