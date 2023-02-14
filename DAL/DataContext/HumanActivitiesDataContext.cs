using DAL.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
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
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillItem> BillItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<BillItem> Costs { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<DataEntities.Task> Tasks { get; set; }
        public virtual DbSet<ToDoList> ToDoList { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroups> UserGroups { get; set; }
        public virtual DbSet<UserIdentity> UserIdentities { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Section> Sections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var connectionString = builder.Build().GetSection("ConnectionStrings").GetSection("HumanActivitiesDB").Value;
                optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityGuid)
                .HasValueGenerator<GuidValueGenerator>();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Activities)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activities_User_UserId");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.BillGuid)
                .HasValueGenerator<GuidValueGenerator>();

                entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<BillItem>(entity =>
            {
                entity.Property(e => e.BillItemGuid)
                .HasValueGenerator<GuidValueGenerator>();

                entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupGuid)
                .HasValueGenerator<GuidValueGenerator>();
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionGuid)
                .HasValueGenerator<GuidValueGenerator>();
            });

            modelBuilder.Entity<DataEntities.Task>(entity =>
            {
                entity.Property(e => e.TaskGuid)
                .HasValueGenerator<GuidValueGenerator>();
            });

            modelBuilder.Entity<ToDoList>(entity =>
            {
                entity.Property(e => e.ToDoListGuid)
                .HasValueGenerator<GuidValueGenerator>();

                entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserGuid)
                .HasValueGenerator<GuidValueGenerator>();

                entity.HasOne(e => e.UserRole)
                    .WithMany(u => u.Users)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_user_role_id");
            });

            modelBuilder.Entity<UserIdentity>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Salt)
                    .IsRequired();
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Token)
                    .IsRequired();
            });
        }
    }
}
