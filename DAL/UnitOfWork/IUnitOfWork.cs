using DAL.DataEntities;
using DAL.Repository;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Activity> ActivityRepo { get; }
        IRepository<Bill> BillRepo { get; }
        IRepository<BillItem> BillItemRepo { get; }
        IRepository<Category> CategoryRepo { get; }
        IRepository<Group> GroupRepo { get; }
        IRepository<Section> SectionRepo { get; }
        IRepository<DataEntities.Task> TaskRepo { get; }
        IRepository<ToDoList> TodoListRepo { get; }
        IRepository<UserGroups> UserGroupRepo { get; }
        IRepository<UserIdentity> UserIdentityRepo { get; }
        IRepository<UserRefreshToken> UserRefreshTokenRepo { get; }
        IRepository<User> UserRepo { get; }
        IRepository<UserRole> UserRoleRepo { get; }

        Task<int> CompleteAsync();
        void Dispose();
    }
}