using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using gamesApi.Models;
using gamesApi.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using gamesApi.Repositories;

namespace gamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly GamesContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UsersController(GamesContext context, IConfiguration configuration, IUserService userService, IUserRepository userRepository)
{
    _context = context;
    _configuration = configuration;
    _userService = userService;
    _userRepository = userRepository;
}

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        //Only admins should be able to delete users.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

        
    }

