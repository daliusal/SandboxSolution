using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SandboxSolution.Dtos;
using SandboxSolution.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SandboxSolution.Pages.Login
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public UserDto User { get; set; }

        private readonly IAuthRepo _repository;
        private readonly IConfiguration _configuration;
        public IndexModel(IAuthRepo repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = await _repository.Login(User, _configuration.GetSection("AppSettings:Token").Value);
            if (token != null)
            {
                HttpContext.Session.SetString("Token", token);
                return Redirect("/");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
