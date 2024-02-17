using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using gamesApi.Services;
using gamesApi.Models;
namespace gamesApi.Services;
public static class AuthenticationService
{
    public static IResult Login(UserLogin user, IUserService service, IConfiguration configuration)
    {
        if (!string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Password))
        {
            var loggedInUser = service.Get(user);
            if (loggedInUser is null) return Results.NotFound("User not found");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedInUser.Username),
                new Claim(ClaimTypes.Email, loggedInUser.Email),
                new Claim(ClaimTypes.Role, loggedInUser.Role)
            };

            var token = new JwtSecurityToken
            (
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Results.Ok(tokenString);
        }
        return Results.BadRequest("Invalid user credentials");
    }
}
