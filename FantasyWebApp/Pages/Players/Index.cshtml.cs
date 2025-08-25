using FantasyCricketApp.Data;
using FantasyCricketApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FantasyCricketApp.Pages.Players
{
    public class IndexModel : PageModel
    {
        public List<Player> Players { get; set; }

        public void OnGet()
        {
            Players = PlayerRepository.GetAll();
        }
    }
}
