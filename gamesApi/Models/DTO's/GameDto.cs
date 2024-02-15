using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace gamesApi.Models
{
    public class GameDto

    {
        [Key]
        public int GameId { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string GameName { get; set; }

        [NotNull]
        [MaxLength(150)]
        public string GameDescription { get; set; }

        [NotNull]
        public DateTime ReleaseDate { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }


        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }

        public GameDto(int gameId, int publisherId)
        {
            GameId = gameId;
            PublisherId = publisherId;
        }

        public GameDto()
        {
        }
    }
}
