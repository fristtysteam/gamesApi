using gamesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace gamesApi.Repositories
{

    public class GamesRepository : IGamesRepository
    {

        private readonly GamesContext _context;


        public GamesRepository(GamesContext context)
        {
            _context = context;
        }

        public async Task<List<GameDto>> GetGames()
        {
            if (_context.Games == null)
            {
                return null;
            }

            var games = await _context.Games.Select(t =>
                new GameDto(t.GameId, t.PublisherId)
                {
                    GameDescription = t.GameDescription,
                    GameName = t.GameName,
                    Genre = t.Genre,
                    ReleaseDate = t.ReleaseDate,
                    //ImageUrl = t.ImageUrl
                }
            ).ToListAsync();

            if (games != null)
            {
                return games;
            }

            return null;
        }


        public async Task<GameDto> GetGameById(int id)
        {
            var game = await _context.Games
                .Where(g => g.GameId == id)
                .Select(t => new GameDto(t.GameId, t.PublisherId)
                {
                    GameDescription = t.GameDescription,
                    GameName = t.GameName,
                    Genre = t.Genre,
                    ReleaseDate = t.ReleaseDate
                })
                .FirstOrDefaultAsync();

            return game;
        }

        public async Task<bool> DeleteGameByID(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return false;
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateGameByID(int id, GameDto updatedGame)
        {
            if (id != updatedGame.GameId)
            {
                return false;
            }

          
                var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.GameId == id);

                if (existingGame == null)
                {
                    return false;
                }

                existingGame.GameName = updatedGame.GameName;
                existingGame.GameDescription = updatedGame.GameDescription;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.Genre = updatedGame.Genre;
                existingGame.PublisherId = updatedGame.PublisherId;

                await _context.SaveChangesAsync();

                return true;
            }
           
            
        
        public async Task<GameDto> CreateGame(GameDto newGame)
        {
            try
            {
                var game = new Game
                {
                    GameName = newGame.GameName,
                    GameDescription = newGame.GameDescription,
                    Genre = newGame.Genre,
                    ReleaseDate = newGame.ReleaseDate,
                };

                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                return new GameDto(game.GameId, game.PublisherId)
                {
                    GameName = game.GameName,
                    GameDescription = game.GameDescription,    
                    Genre = game.Genre,
                    ReleaseDate = game.ReleaseDate,
                    

                };
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}

