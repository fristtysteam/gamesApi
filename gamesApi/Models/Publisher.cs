using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gamesApi.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PublisherName { get; set; }

        //CODE TO SHOW GAMES PER PUBLISHER
       /* public ICollection<Game> PublisherGames { get; set; }

        public Publisher()
        {
            PublisherGames = new List<Game>();
        }*/
    }
}
