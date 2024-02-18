using gamesApi.Models;
using gamesApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GamesContext _context;
        private readonly IGamesRepository _gamesRepository;

        public GamesController(GamesContext context, IGamesRepository gamesRepository)
        {
            _context = context;
            _gamesRepository = gamesRepository;
        }

        /// <summary>
        /// Retrieves a list of games.
        /// </summary>
        /// <remarks>
        /// Authorised for standard users. If removed, JWT key is not required to use, but it's more fun this way.
        /// </remarks>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            var games = _gamesRepository.GetGames();
            return Ok(games.Result.ToArray());
        }

        /// <summary>
        /// Retrieves a game by its ID.
        /// </summary>
        /// /// <remarks>
        /// Same as the GetGames method however this one only displays one Game, Takes in the ID of the game to display
        /// </remarks>
        /// <param name="id">The ID of the game to retrieve.</param>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]
        public async Task<IActionResult> GetGameById(int id)
        {
            var game = await _gamesRepository.GetGameById(id);
            return game != null ? Ok(game) : NotFound();
        }

        /// <summary>
        /// Updates a game by its ID.
        /// </summary>
        /// <param name="id">The ID of the game to update.</param>
        /// <param name="updatedGame">The updated game data.</param>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> UpdateGameById(int id, GameDto updatedGame)
        {
            if (id != updatedGame.GameId)
                return BadRequest("The provided id in the URL does not match the GameId in the payload.");

            var result = await _gamesRepository.UpdateGameByID(id, updatedGame);
            return result ? Ok("The game has been updated") : NotFound("Game not found or unable to update.");
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="newGameDto">The data for the new game.</param>
    
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<GameDto>> CreateGame(GameDto newGameDto)
        {
            try
            {
                // Check if the publisher with the provided PublisherId exists
                var publisherExists = await _context.Publisher.AnyAsync(p => p.PublisherId == newGameDto.PublisherId);
                if (!publisherExists)
                {
                    return BadRequest("The provided PublisherId does not exist.");
                }

                var createdGame = await _gamesRepository.CreateGame(newGameDto);
                return CreatedAtAction(nameof(CreateGame), new { id = createdGame.GameId }, createdGame);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a game by its ID.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var result = await _gamesRepository.DeleteGameByID(id);
            return result ? Ok("Game Deleted Successfully") : NotFound();
        }

        private bool GamesExists(int id)
        {
            return (_context.Games?.Any(e => e.GameId == id)).GetValueOrDefault();
        }
    }
}
