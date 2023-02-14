using DAL.DataContext;
using DAL.DataEntities;
using DAL.Repository;
using Task = DAL.DataEntities.Task;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHumanActivitiesDataContext _humanActivitiesDataContext;

        private IRepository<Activity>? _activityRepo;
        public IRepository<Activity> ActivityRepo
        {
            get
            {
                if (_activityRepo == null)
                {
                    _activityRepo = new Repository<Activity>(_humanActivitiesDataContext.Activities);
                }
                return _activityRepo;
            }
        }

        private IRepository<Bill>? _billRepo;
        public IRepository<Bill> BillRepo
        {
            get
            {
                if (_billRepo == null)
                {
                    _billRepo = new Repository<Bill>(_humanActivitiesDataContext.Bills);
                }
                return _billRepo;
            }
        }

        private IRepository<BillItem>? _billItemRepo;
        public IRepository<BillItem> BillItemRepo
        {
            get
            {
                if (_billItemRepo == null)
                {
                    _billItemRepo = new Repository<BillItem>(_humanActivitiesDataContext.BillItems);
                }
                return _billItemRepo;
            }
        }

        private IRepository<Category>? _categoryRepo;
        public IRepository<Category> CategoryRepo
        {
            get
            {
                if (_categoryRepo == null)
                {
                    _categoryRepo = new Repository<Category>(_humanActivitiesDataContext.Categories);
                }
                return _categoryRepo;
            }
        }

        private IRepository<Group>? _groupRepo;
        public IRepository<Group> GroupRepo
        {
            get
            {
                if (_groupRepo == null)
                {
                    _groupRepo = new Repository<Group>(_humanActivitiesDataContext.Groups);
                }
                return _groupRepo;
            }
        }

        private IRepository<Section>? _sectionRepo;
        public IRepository<Section> SectionRepo
        {
            get
            {
                if (_sectionRepo == null)
                {
                    _sectionRepo = new Repository<Section>(_humanActivitiesDataContext.Sections);
                }
                return _sectionRepo;
            }
        }

        private IRepository<Task>? _taskRepo;
        public IRepository<Task> TaskRepo
        {
            get
            {
                if (_taskRepo == null)
                {
                    _taskRepo = new Repository<Task>(_humanActivitiesDataContext.Tasks);
                }
                return _taskRepo;
            }
        }

        private IRepository<ToDoList>? _toDoListRepo;
        public IRepository<ToDoList> TodoListRepo
        {
            get
            {
                if (_toDoListRepo == null)
                {
                    _toDoListRepo = new Repository<ToDoList>(_humanActivitiesDataContext.ToDoList);
                }
                return _toDoListRepo;
            }
        }

        private IRepository<User>? _userRepo;
        public IRepository<User> UserRepo
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new Repository<User>(_humanActivitiesDataContext.Users);
                }
                return _userRepo;
            }
        }

        private IRepository<UserGroups>? _userGroupRepo;
        public IRepository<UserGroups> UserGroupRepo
        {
            get
            {
                if (_userGroupRepo == null)
                {
                    _userGroupRepo = new Repository<UserGroups>(_humanActivitiesDataContext.UserGroups);
                }
                return _userGroupRepo;
            }
        }

        private IRepository<UserIdentity>? _userIdentityRepo;
        public IRepository<UserIdentity> UserIdentityRepo
        {
            get
            {
                if (_userIdentityRepo == null)
                {
                    _userIdentityRepo = new Repository<UserIdentity>(_humanActivitiesDataContext.UserIdentities);
                }
                return _userIdentityRepo;
            }
        }

        private IRepository<UserRefreshToken>? _userRefreshTokenRepo;
        public IRepository<UserRefreshToken> UserRefreshTokenRepo
        {
            get
            {
                if (_userRefreshTokenRepo == null)
                {
                    _userRefreshTokenRepo = new Repository<UserRefreshToken>(_humanActivitiesDataContext.UserRefreshTokens);
                }
                return _userRefreshTokenRepo;
            }
        }

        private IRepository<UserRole>? _userRoleRepo;
        public IRepository<UserRole> UserRoleRepo
        {
            get
            {
                if (_userRoleRepo == null)
                {
                    _userRoleRepo = new Repository<UserRole>(_humanActivitiesDataContext.UserRoles);
                }
                return _userRoleRepo;
            }
        }

        public UnitOfWork(IHumanActivitiesDataContext humanActivitiesDataContext)
        {
            _humanActivitiesDataContext = humanActivitiesDataContext;
        }

        public async Task<int> CompleteAsync()
        {
            return await _humanActivitiesDataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _humanActivitiesDataContext.Dispose();
        }
    }
}
