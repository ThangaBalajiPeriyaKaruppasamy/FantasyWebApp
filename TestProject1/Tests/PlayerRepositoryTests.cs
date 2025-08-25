using FantasyCricketApp.Data;
using FantasyCricketApp.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace FantasyCricketApp.Tests
{
    [TestFixture]
    public class PlayerRepositoryTests
    {
        // Runs before each test to restore initial two players
        [SetUp]
        public void ResetRepository()
        {
            var playersField = typeof(PlayerRepository)
                .GetField("Players", BindingFlags.NonPublic | BindingFlags.Static);

            var list = (List<Player>)playersField.GetValue(null);
            list.Clear();

            // Re-seed initial players
            list.Add(new Player
            {
                Id = 1,
                Name = "Virat Kohli",
                Team = "RCB",
                Role = "Batsman",
                Price = 7.0M
            });
            list.Add(new Player
            {
                Id = 2,
                Name = "Jasprit Bumrah",
                Team = "MI",
                Role = "Bowler",
                Price = 8.5M
            });
        }

        [Test]
        public void GetAll_ReturnsInitialTwoPlayers()
        {
            var all = PlayerRepository.GetAll();
            Assert.AreEqual(2, all.Count);
            Assert.AreEqual("Virat Kohli", all[0].Name);
            Assert.AreEqual("Jasprit Bumrah", all[1].Name);
        }

        [Test]
        public void GetById_ExistingId_ReturnsPlayer()
        {
            var player = PlayerRepository.GetById(2);
            Assert.IsNotNull(player);
            Assert.AreEqual("Jasprit Bumrah", player.Name);
        }

        [Test]
        public void Add_NewPlayer_AssignsNewIdAndAdds()
        {
            var newPlayer = new Player
            {
                Name = "Rohit Sharma",
                Team = "MI",
                Role = "Batsman",
                Price = 9.0M
            };

            PlayerRepository.Add(newPlayer);

            Assert.Greater(newPlayer.Id, 2);
            var fetched = PlayerRepository.GetById(newPlayer.Id);
            Assert.IsNotNull(fetched);
            Assert.AreEqual("Rohit Sharma", fetched.Name);
        }

        [Test]
        public void Update_ExistingPlayer_ModifiesFields()
        {
            var original = PlayerRepository.GetById(1);
            original.Price = 10.0M;
            PlayerRepository.Update(original);

            var updated = PlayerRepository.GetById(1);
            Assert.AreEqual(10.0M, updated.Price);
        }

        [Test]
        public void Delete_ExistingId_RemovesPlayer()
        {
            PlayerRepository.Delete(1);
            var afterDelete = PlayerRepository.GetAll();
            Assert.AreEqual(1, afterDelete.Count);
            Assert.IsNull(PlayerRepository.GetById(1));
        }
    }
}
