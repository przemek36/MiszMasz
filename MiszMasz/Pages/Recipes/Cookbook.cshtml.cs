using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiszMasz.Entities;
using MiszMasz.Pages.Shared;

namespace MiszMasz.Pages.Recipes
{
    public class CookbookModel : AuthorizedPageModel
    {
        private readonly MiszMasz.MiszMaszDbContext _context;

        public CookbookModel(MiszMasz.MiszMaszDbContext context) : base()
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; }

        public async Task OnGetAsync(string Search, string OrderBy)
        {
            var userId = Authorize();
            var cockbook = await _context.Cockbooks
                .Where(c => c.UserId == userId)
                .Include(c => c.Recipe)
                .ThenInclude(r => r.Author)
                .ToListAsync();

            var recipes = cockbook
                .Select(c => c.Recipe)
                .ToList();

            if (!String.IsNullOrEmpty(Search))
                recipes = recipes.Where(r => r.Name.Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();

            switch (OrderBy?.ToLower() ?? "")
            {
                case "likesdesc":
                    recipes = recipes.OrderByDescending(r => r.Likes).ToList();
                    break;
                case "likesasc":
                    recipes = recipes.OrderBy(r => r.Likes).ToList();
                    break;
                case "namesdesc":
                    recipes = recipes.OrderByDescending(r => r.Name).ToList();
                    break;
                case "namesasc":
                    recipes = recipes.OrderBy(r => r.Name).ToList();
                    break;
                case "authordesc":
                    recipes = recipes.OrderByDescending(r => r.Author.Name).ToList();
                    break;
                case "authorasc":
                    recipes = recipes.OrderBy(r => r.Author.Name).ToList();
                    break;
            }

            Recipe = recipes;
        }

    }
}
