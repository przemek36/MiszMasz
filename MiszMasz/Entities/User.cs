using System.Collections.Generic;

namespace MiszMasz.Entities
{
    public class User
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
