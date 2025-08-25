using FantasyCricketApp.Data;
using FantasyCricketApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FantasyCricketApp.Pages.Players
{
    public class DetailsModel : PageModel
    {
        public Player Player { get; set; }

        public IActionResult OnGet(int id)
        {
            Player = PlayerRepository.GetById(id);
            if (Player == null)
                return RedirectToPage("Index");
            return Page();
        }
    }
}
