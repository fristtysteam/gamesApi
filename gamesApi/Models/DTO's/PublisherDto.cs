using System;
using System.ComponentModel.DataAnnotations;

namespace gamesApi.Models
{
    public class PublisherDto
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PublisherName { get; set; }
       // public List<GameDto> PublisherGames { get; set; }

        public PublisherDto(int publisherId)
        {
            PublisherId = publisherId;
        }
    }
}
