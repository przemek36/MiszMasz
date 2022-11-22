using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiszMasz;
using MiszMasz.Entities;
using MiszMasz.Helpers;
using MiszMasz.Pages.Shared;

namespace MiszMasz.Pages.Recipes
{
    public class CreateModel : AuthorizedPageModel
    {
        private readonly MiszMasz.MiszMaszDbContext _context;

        public CreateModel(MiszMasz.MiszMaszDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Authorize();
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var authorId = Authorize();
            Recipe.AuthorId = authorId;
            Recipe.Likes = 0;
            _context.Recipes.Add(Recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./MyRecipes");
        }
    }
}
