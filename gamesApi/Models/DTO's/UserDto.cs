using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace gamesApi.Models.DTO_s
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [NotNull]
        [MaxLength(20)]
        public string Role { get; set; }
    }
}
