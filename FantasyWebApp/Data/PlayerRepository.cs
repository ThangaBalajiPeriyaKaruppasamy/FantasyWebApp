using FantasyCricketApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace FantasyCricketApp.Data
{
    public static class PlayerRepository
    {
        private static readonly List<Player> Players = new()
        {
            new Player { Id = 1, Name = "Virat Kohli", Team = "RCB", Role = "Batsman", Price = 7.0M },
            new Player { Id = 2, Name = "Jasprit Bumrah", Team = "MI", Role = "Bowler", Price = 8.5M }
        };

        public static List<Player> GetAll() =>
            Players.OrderBy(p => p.Id).ToList();

        public static Player GetById(int id) =>
            Players.FirstOrDefault(p => p.Id == id);

        public static void Add(Player player)
        {
            var nextId = Players.Any() ? Players.Max(p => p.Id) + 1 : 1;
            player.Id = nextId;
            Players.Add(player);
        }

        public static void Update(Player player)
        {
            var existing = GetById(player.Id);
            if (existing == null) return;
            existing.Name = player.Name;
            existing.Team = player.Team;
            existing.Role = player.Role;
            existing.Price = player.Price;
        }

        public static void Delete(int id)
        {
            var player = GetById(id);
            if (player != null)
                Players.Remove(player);
        }
    }
}
