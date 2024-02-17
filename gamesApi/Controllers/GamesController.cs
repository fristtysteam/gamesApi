﻿using gamesApi.Models;
using gamesApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET
        //AUTHORIZED FOR ADMINS, FUNCTIONALITY IN PROGRAM.CS
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            var games = _gamesRepository.GetGames();

            var result = Ok(games.Result.ToArray());
            return result;
        }

        // GET {id}
        //AUTHORIZED FOR ADMINS, FUNCTIONALITY IN PROGRAM.CS
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            var game = await _gamesRepository.GetGameById(id);
            if (game != null)
            {
                return Ok(game); 
            }
            else
            {
                return NotFound(); 
            }
        }

        // PUT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        //AUTHORIZED FOR ADMINS, FUNCTIONALITY IN PROGRAM.CS
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameById(int id, GameDto updatedGame)
        {

            if (id != updatedGame.GameId)
            {
                return BadRequest("The provided id in the URL does not match the GameId in the payload.");
            }

            var result = await _gamesRepository.UpdateGameByID(id, updatedGame);

            if (result)
            {
                return Ok("The game has been updated");
            }
            else
            {
                return NotFound("Game not found or unable to update.");
            }
        }

        // POST
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //AUTHORIZED FOR ADMINS, FUNCTIONALITY IN PROGRAM.CS
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<GameDto>> CreateGame([FromBody] GameDto newGameDto)
        {
            try
            {
                var createdGameDto = await _gamesRepository.CreateGame(newGameDto);

                if (createdGameDto != null)
                {
                    return CreatedAtAction(nameof(GetGameById), new { id = createdGameDto.GameId }, createdGameDto);
                }
                else
                {
                    return Problem("Failed to create the game.");
                }
            }
            catch (Exception ex)
            {
                return Problem($"An error occurred while creating the game: {ex.Message}");
            }
        }
        // DELETE
        //AUTHORIZED FOR ADMINS, FUNCTIONALITY IN PROGRAM.CS
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var result = await _gamesRepository.DeleteGameByID(id);
            if (result == true)
            {
                return Ok("Game Deleted Successfully");
            }
            else
            {
                return NotFound(); 
            }
        }

        private bool GamesExists(int id)
        {
            return (_context.Games?.Any(e => e.GameId == id)).GetValueOrDefault();
        }
    }
}
