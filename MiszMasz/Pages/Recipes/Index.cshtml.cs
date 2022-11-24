using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiszMasz.Entities;

namespace MiszMasz.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly MiszMasz.MiszMaszDbContext _context;

        public IndexModel(MiszMasz.MiszMaszDbContext context) : base()
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; }

        public async Task OnGetAsync(string Search, string OrderBy)
        {
             var recipes = await _context.Recipes
                .Include(r => r.Author).ToListAsync();

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
                case "namedesc":
                    recipes = recipes.OrderByDescending(r => r.Name).ToList();
                    break;
                case "nameasc":
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
