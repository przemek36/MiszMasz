using System.Collections.Generic;

namespace MiszMasz.Entities
{
    public class Recipe
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
    }
}
