using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiszMasz.Helpers;
using MiszMasz.Models;
using MiszMasz.Pages.Shared;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiszMasz.Pages.Authenticate
{
    public class LoginModel : PageModel
    {
        private readonly MiszMaszDbContext _context;

        public LoginModel(MiszMaszDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; }

        
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.Where(u => u.Name.Equals(LoginRequest.Username) && u.Password.Equals(LoginRequest.Password)).FirstOrDefaultAsync();
                if (user != null)
                {
                    HttpContext.Session.Set("UserID", Encoding.ASCII.GetBytes(user.Id.ToString()));
                    HttpContext.Session.Set("Username", Encoding.ASCII.GetBytes(user.Name));
                    return Redirect("/Index");
                }
                else
                {
                    ViewData["Errors"] = "Zly login lub haslo";
                    return Page();
                }
            }
            return Page();
        }
    }
}
