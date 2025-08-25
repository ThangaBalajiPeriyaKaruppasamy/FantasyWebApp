using FantasyCricketApp.Data;
using FantasyCricketApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FantasyCricketApp.Pages.Players
{
    public class DeleteModel : PageModel
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

        public IActionResult OnPost(int id)
        {
            PlayerRepository.Delete(id);
            return RedirectToPage("Index");
        }
    }
}
