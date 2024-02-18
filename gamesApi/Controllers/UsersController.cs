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
        private readonly IUserRepository _userRepository;

        public UsersController(GamesContext context, IConfiguration configuration, IUserRepository userRepository)
{
    _context = context;
    _configuration = configuration;
    _userRepository = userRepository;
}
        /// <summary>
        /// Deletes a user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Message declaring wheter a user has been deleted or doesnt exist</returns>
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

            return Ok("User has been deleted");
        }
    }

        
    }

