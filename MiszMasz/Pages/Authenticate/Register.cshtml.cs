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
                if (RegisterRequest.Password.Length < 5)
                {
                    ViewData["Errors"] = "Password is too short";
                    return Page();
                }

                var user = await _context.Users.Where(u => u.Name.Equals(RegisterRequest.Username)).FirstOrDefaultAsync();
                if (user == null)
                {
                    var newUser = new User
                    {
                        Email = RegisterRequest.Email,
                        Name = RegisterRequest.Username,
                        Password = RegisterRequest.Password,
                        RoleId = 1,
                    };

                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    return Redirect("Login");
                }
                else
                {
                    ViewData["Errors"] = "User already exists";
                    return Page();
                }
            }            
            return Page();
        }
    }
}
