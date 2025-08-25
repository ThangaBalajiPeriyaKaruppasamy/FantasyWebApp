using FantasyCricketApp.Data;
using FantasyCricketApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FantasyCricketApp.Pages.Players
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Player Player { get; set; }

        public IActionResult OnGet(int id)
        {
            Player = PlayerRepository.GetById(id);
            if (Player == null)
                return RedirectToPage("Index");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            PlayerRepository.Update(Player);
            return RedirectToPage("Index");
        }
    }
}
