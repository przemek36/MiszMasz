using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiszMasz.Entities;
using MiszMasz.Models;
using MiszMasz.Pages.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace MiszMasz.Pages.Authenticate
{
    public class RegisterModel : PageModel
    {
        private readonly MiszMaszDbContext _context;
        public RegisterModel(MiszMaszDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public RegisterRequest RegisterRequest { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Name.Equals(RegisterRequest.Username)))
                {
                    ViewData["Errors"] = "Login jest juz zajęty";
                    return Page();
                }
                if(await _context.Users.AnyAsync(u => u.Email.Equals(RegisterRequest.Username)))
                {
                    ViewData["Errors"] = "Email jest juz zajęty";
                    return Page();
                }
                if (RegisterRequest.Password == RegisterRequest.RepeatPassword)
                {
                    ViewData["Errors"] = "Hasła nie są takie same";
                    return Page();
                }
                if (RegisterRequest.Password.Length < 5)
                {
                    ViewData["Errors"] = "Hasło jest za krótkie";
                    return Page();
                }

                var newUser = new User
                {
                    Email = RegisterRequest.Email,
                    Name = RegisterRequest.Username,
                    Password = RegisterRequest.Password,
                    RoleId = 1
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();
                return Redirect("Login");
            }         
            return Page();
        }
    }
}
