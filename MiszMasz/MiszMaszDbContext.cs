using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiszMasz.Entities;

namespace MiszMasz
{
    public class MiszMaszDbContext : DbContext
    {
        public MiszMaszDbContext(DbContextOptions<MiszMaszDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Cockbook> Cockbooks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Recipe>()
                .HasOne(r => r.Author)
                .WithMany(a => a.Recipes)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RecipeIngredient>()
                .HasOne(i => i.Recipe)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RecipeIngredient>()
                .HasOne(i => i.Ingredient)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RecipeIngredient>()
                .HasKey(i => new { i.RecipeId, i.IngredientId });

            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(c => c.Recipe)
                .WithMany(r => r.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Cockbook>()
                .HasOne(c => c.Recipe)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Cockbook>()
                .HasOne(c => c.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Cockbook>()
                .HasKey(c => new { c.RecipeId, c.UserId });

            base.OnModelCreating(builder);
        }

       
    }
}
