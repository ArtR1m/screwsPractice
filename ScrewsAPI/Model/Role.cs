﻿namespace ScrewsAPI.Model
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
