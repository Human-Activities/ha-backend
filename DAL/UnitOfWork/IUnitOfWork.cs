using DAL.DataEntities;
using DAL.Repository;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Activity> ActivityRepo { get; }
        IRepository<Calendar> CalendarRepo { get; }
        IRepository<Category> CategoryRepo { get; }
        IRepository<Cost> CostRepo { get; }
        IRepository<Event> EventRepo { get; }
        IRepository<Group> GroupRepo { get; }
        IRepository<Section> SectionRepo { get; }
        IRepository<DataEntities.Task> TaskRepo { get; }
        IRepository<ToDoListTemplate> TodoListTemplateRepo { get; }
        IRepository<UserCosts> UserCostRepo { get; }
        IRepository<UserGroups> UserGroupRepo { get; }
        IRepository<UserIdentity> UserIdentityRepo { get; }
        IRepository<UserRefreshToken> UserRefreshTokenRepo { get; }
        IRepository<User> UserRepo { get; }
        IRepository<UserRole> UserRoleRepo { get; }

        Task<int> CompleteAsync();
        void Dispose();
    }
}