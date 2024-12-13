namespace ScrewsAPI.Model
{
    public partial class User
    {
        public User(string name, string email, string passwordHash)
        {
            this.Name = name;
            this.Email = email;
            this.PasswordHash = passwordHash;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public int? Role { get; set; }

        public virtual Role? RoleNavigation { get; set; }
    }
    public class UpdateUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
    public class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
