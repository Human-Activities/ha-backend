﻿namespace DAL.DataEntities
{
    public class Group
    {
        public Group()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; }
    }
}
