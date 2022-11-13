using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiszMasz;
using MiszMasz.Entities;

namespace MiszMasz.Pages.test
{
    public class IndexModel : PageModel
    {
        private readonly MiszMasz.MiszMaszDbContext _context;

        public IndexModel(MiszMasz.MiszMaszDbContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; }

        public async Task OnGetAsync()
        {
            Recipe = await _context.Recipes
                .Include(r => r.Author).ToListAsync();
        }
    }
}
