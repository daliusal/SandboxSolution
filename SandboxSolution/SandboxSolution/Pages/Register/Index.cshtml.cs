using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SandboxSolution.Dtos;
using SandboxSolution.Models;

namespace SandboxSolution.Pages.Register
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public UserRegistrationDto User { get; set; }
        private readonly IAuthRepo _repository;
        public IndexModel(IAuthRepo repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _repository.Register(User);
                return Redirect("/login");
            }
            else
            {
                return Page();
            }
        }
    }
}
