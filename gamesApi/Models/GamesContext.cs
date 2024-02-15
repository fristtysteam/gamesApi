using Microsoft.EntityFrameworkCore;

namespace gamesApi.Models
{
    public class GamesContext : DbContext
    {
        public string DbPath { get; }

        public GamesContext(DbContextOptions<GamesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Game> Games { get; set; } = null;
        public DbSet<Publisher> Publisher { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Game>()
                .HasData(
            new Game
                {
                    GameId = 1,
                    GameName = "Counter-Strike",
                    GameDescription = "Like previous games in the series, Counter-Strike 2 is a multiplayer tactical first-person shooter, where two teams compete to complete different objectives, depending on the game mode selected. Players are split into two teams, the Counter-Terrorists and the Terrorists.",
                    ReleaseDate = new DateTime(2023, 09, 27),
                    Genre = "Shooter",
                    PublisherId = 1

                },
                new Game
                {
                    GameId = 2,
                    GameName = "Assassin's Creed: Valhalla",
                    GameDescription = "Assassin's Creed Valhalla is an action role-playing video game developed by Ubisoft Montreal and published by Ubisoft. It is the twelfth major installment and the twenty-second release in the Assassin's Creed series.",
                    ReleaseDate = new DateTime(2020, 11, 10),
                    Genre = "Action-adventure",
                    PublisherId = 2
                },
                new Game
                {
                    GameId = 3,
                    GameName = "Cyberpunk 2077",
                    GameDescription = "Cyberpunk 2077 is an action role-playing video game developed and published by CD Projekt. The story takes place in Night City, an open world set in the Cyberpunk universe.",
                    ReleaseDate = new DateTime(2020, 12, 10),
                    Genre = "Action RPG",
                    PublisherId = 3
                },
                new Game
                {
                    GameId = 4,
                    GameName = "Half-Life 3",
                    GameDescription = "Half-Life 3 is a highly anticipated first-person shooter video game developed and published by Valve. It continues the story of protagonist Gordon Freeman and his struggle against the Combine.",
                    ReleaseDate = new DateTime(2024, 06, 15),
                    Genre = "First-person shooter",
                    PublisherId = 1

                },
                new Game
                {
                    GameId = 5,
                    GameName = "Portal 3",
                    GameDescription = "Portal 3 is a puzzle-platform game developed and published by Valve. Players control Chell, who must navigate through various test chambers using the Aperture Science Handheld Portal Device.",
                    ReleaseDate = new DateTime(2025, 03, 20),
                    Genre = "Puzzle-platform",
                    PublisherId = 1
                },
                new Game
                {
                    GameId = 6,
                    GameName = "Left 4 Dead 3",
                    GameDescription = "Left 4 Dead 3 is a cooperative first-person shooter video game developed and published by Valve. Players must survive against hordes of zombies while completing objectives.",
                    ReleaseDate = new DateTime(2026, 09, 10),
                    Genre = "Cooperative shooter",
                    PublisherId = 1

                },
                new Game
                {
                    GameId = 7,
                    GameName = "Assassin's Creed: Odyssey",
                    GameDescription = "Assassin's Creed Odyssey is an action role-playing video game developed by Ubisoft Quebec and published by Ubisoft. It is the eleventh major installment and the twenty-first release in the Assassin's Creed series.",
                    ReleaseDate = new DateTime(2018, 10, 05),
                    Genre = "Action RPG",
                    PublisherId = 2
                },
                new Game
                {
                    GameId = 8,
                    GameName = "Far Cry 6",
                    GameDescription = "Far Cry 6 is a first-person shooter game developed by Ubisoft Toronto and published by Ubisoft. The game is set on the fictional Caribbean island of Yara, inspired by Cuba.",
                    ReleaseDate = new DateTime(2021, 10, 07),
                    Genre = "First-person shooter",
                    PublisherId = 2
                },
                new Game
                {
                    GameId = 9,
                    GameName = "Watch Dogs: Legion",
                    GameDescription = "Watch Dogs: Legion is an action-adventure game developed by Ubisoft Toronto and published by Ubisoft. Players control DedSec, a hacker group fighting against the surveillance state in a near-future London.",
                    ReleaseDate = new DateTime(2020, 10, 29),
                    Genre = "Action-adventure",
                    PublisherId = 2
                },
                new Game
                {
                    GameId = 10,
                    GameName = "Assassin's Creed: Origins",
                    GameDescription = "Assassin's Creed Origins is an action role-playing video game developed by Ubisoft Montreal and published by Ubisoft. It is the tenth major installment in the Assassin's Creed series.",
                    ReleaseDate = new DateTime(2017, 10, 27),
                    Genre = "Action-adventure",
                    PublisherId = 2
                },
                new Game
                {
                    GameId = 11,
                    GameName = "The Witcher 3: Wild Hunt",
                    GameDescription = "The Witcher 3: Wild Hunt is an action role-playing game developed and published by CD Projekt Red. It is the third installment in the Witcher series, and players control Geralt of Rivia, a monster hunter known as a Witcher, who is searching for his missing adopted daughter.",
                    ReleaseDate = new DateTime(2015, 05, 19),
                    Genre = "Action RPG",
                    PublisherId = 3
                }
            );

            

            modelBuilder.Entity<Publisher>()
                .HasData(
                    new Publisher
                    {
                        PublisherId = 1,
                        PublisherName = "Valve"
                    },
                     new Publisher
                     {
                         PublisherId = 2,
                         PublisherName = "Ubisoft"
                     },
                      new Publisher
                      {
                          PublisherId = 3,
                          PublisherName = "CD Projekt RED"
                      }
                );
           

            base.OnModelCreating(modelBuilder);
        }
    }
}
