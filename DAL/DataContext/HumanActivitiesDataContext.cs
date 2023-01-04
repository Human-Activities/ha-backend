using DAL.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.DataContext
{
    public class HumanActivitiesDataContext : DbContext, IHumanActivitiesDataContext
    {
        public HumanActivitiesDataContext()
        { }

        public HumanActivitiesDataContext(DbContextOptions Options) : base(Options)
        { }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<DataEntities.Task> Tasks { get; set; }
        public virtual DbSet<ToDoListTemplate> ToDoListTemplates { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserIdentity> UserIdentities { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                string connectionString = builder.Build().GetSection("ConnectionStrings").GetSection("HumanActivitiesDB").Value;
                optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasOne(e => e.UserRole)
                    .WithMany(u => u.Users)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRoleId");
            });

            modelBuilder.Entity<UserIdentity>(entity =>
            {
                entity.ToTable("UserIdentity");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Salt)
                    .IsRequired();
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.ToTable("UserRefreshToken");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Token)
                    .IsRequired();
            });
        }
    }
}
