using DAL.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext()
        {}

        public DataContext(DbContextOptions Options) : base(Options)
        {}

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<DataEntities.Task> Tasks { get; set; }
        public virtual DbSet<ToDoListTemplate> ToDoListTemplates{ get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserIdentity> UserIdentities { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }



    }
}
