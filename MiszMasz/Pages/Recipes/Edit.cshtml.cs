﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiszMasz;
using MiszMasz.Entities;

namespace MiszMasz.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly MiszMasz.MiszMaszDbContext _context;

        public EditModel(MiszMasz.MiszMaszDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }
        [BindProperty]
        public IEnumerable<Ingredient> Ingredients { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context
                .Recipes
                .Include(r => r.Author)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);

            Ingredients = Recipe
                .Ingredients
                .Select(i => i.Ingredient)
                .ToList();

            if (Recipe == null)
            {
                return NotFound();
            }

            ViewData["Ingredients"] = new MultiSelectList(_context.Ingredients, "Id", "Name");
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Name");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var RecipeIngredients = Ingredients.Select(i => new RecipeIngredient
            {
                IngredientId = i.Id,
                RecipeId = Recipe.Id
            }).ToList();

            Recipe.Ingredients = RecipeIngredients;

            _context.Attach(Recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(Recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
