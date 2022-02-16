using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SandboxSolution.Models;

namespace SandboxSolution.Pages.Admin.Games
{
    public class IndexModel : PageModel
    {
        public readonly StoreContext _context;
        public IndexModel(StoreContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
    }
}
