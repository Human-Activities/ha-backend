using DAL.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public interface IHumanActivitiesDataContext
    {
        DbSet<Activity> Activities { get; set; }
        DbSet<Bill> Bills { get; set; }
        DbSet<BillItem> BillItems { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<BillItem> Costs { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<DataEntities.Task> Tasks { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<ToDoList> ToDoList { get; set; }
        DbSet<UserGroups> UserGroups { get; set; }
        DbSet<UserIdentity> UserIdentities { get; set; }
        DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Dispose();
    }
}