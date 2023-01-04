using DAL.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public interface IHumanActivitiesDataContext
    {
        DbSet<Activity> Activities { get; set; }
        DbSet<Calendar> Calendars { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Cost> Costs { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<DataEntities.Task> Tasks { get; set; }
        DbSet<ToDoListTemplate> ToDoListTemplates { get; set; }
        DbSet<UserIdentity> UserIdentities { get; set; }
        DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Dispose();
    }
}