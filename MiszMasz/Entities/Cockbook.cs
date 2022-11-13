namespace MiszMasz.Entities
{
    public class Cockbook
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } 
    }
}
