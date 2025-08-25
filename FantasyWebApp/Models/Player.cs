using System.ComponentModel.DataAnnotations;

namespace FantasyCricketApp.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Team { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; } // Batsman, Bowler, etc.

        [Range(0, 100)]
        public decimal Price { get; set; } // in millions
    }
}
