namespace MiszMasz.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public string Text { get; set; }
    }
}
