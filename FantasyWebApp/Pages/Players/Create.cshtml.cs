using FantasyCricketApp.Data;
using FantasyCricketApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FantasyCricketApp.Pages.Players
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Player Player { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            PlayerRepository.Add(Player);
            return RedirectToPage("Index");
        }
    }
}
