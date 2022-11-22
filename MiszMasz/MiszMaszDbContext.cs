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
        public DbSet<Cockbook> Cockbooks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Recipe>()
                .HasOne(r => r.Author)
                .WithMany(a => a.Recipes)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.Recipe)
                .WithMany(r => r.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cockbook>()
                .HasOne(c => c.Recipe)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cockbook>()
                .HasOne(c => c.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cockbook>()
                .HasKey(c => new { c.RecipeId, c.UserId });

            builder.Entity<Like>()
                .HasKey(c => new { c.RecipeId, c.UserId });

            builder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Like>()
                .HasOne(l => l.Recipe)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

       
    }
}
