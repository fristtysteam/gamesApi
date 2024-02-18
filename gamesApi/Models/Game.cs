using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace gamesApi.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string GameName { get; set; }

        [Required]
        [NotNull]
        [MaxLength(150)]
        public string GameDescription { get; set; }

        [Required]
        [NotNull]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Genre { get; set; }

      //  public string? ImageUrl { get; set; }
        //dotnet add package Microsoft.EntityFrameworkCore.Design
        //dotnet tool install --global dotnet-ef
        //dotnet ef migrations add AddImageUrlToGame
        //dotnet ef database update


        [ForeignKey(nameof(Publisher))]        
        public int PublisherId { get; set; }

        [JsonIgnore]
        public Publisher Publisher { get; set; }


        public Game()
        {
        }

        public Game(int gameId, string gameName, string gameDescription, DateTime releaseDate, string genre)
        {
            GameId = gameId;
            GameName = gameName;
            GameDescription = gameDescription;
            ReleaseDate = releaseDate;
            Genre = genre;
        }
    }
}

