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
                var connectionString = builder.Build().GetSection("ConnectionStrings").GetSection("HumanActivitiesDB").Value;
                optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityGuid)
                .ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Activities)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activities_User_UserId");
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.Property(e => e.CalendarGuid)
                .ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Calendars)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Calendars_User_UserId");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryGuid)
                .ValueGeneratedOnAdd();

                entity.HasOne(e => e.Activity)
                    .WithMany(a => a.Categories)
                    .HasForeignKey(e => e.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Activity_ActivityId");
            });

            modelBuilder.Entity<Cost>(entity =>
            {
                entity.Property(e => e.CostGuid)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventGuid)
                .ValueGeneratedOnAdd();

                entity.HasOne(e => e.Calendar)
                    .WithMany(c => c.Events)
                    .HasForeignKey(e => e.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Calendar_CalendarId");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupGuid)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionGuid)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DataEntities.Task>(entity =>
            {
                entity.Property(e => e.TaskGuid)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ToDoListTemplate>(entity =>
            {
                entity.Property(e => e.ToDoListTemplateGuid)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserGuid)
                .ValueGeneratedOnAdd();

                entity.HasOne(e => e.UserRole)
                    .WithMany(u => u.Users)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_user_role_id");
            });

            modelBuilder.Entity<UserIdentity>(entity =>
            {
                entity.Property(e => e.Salt)
                    .IsRequired();
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.Property(e => e.Token)
                    .IsRequired();
            });
        }
    }
}
