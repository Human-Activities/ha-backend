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

        private IRepository<Calendar>? _calendarRepo;
        public IRepository<Calendar> CalendarRepo
        {
            get
            {
                if (_calendarRepo == null)
                {
                    _calendarRepo = new Repository<Calendar>(_humanActivitiesDataContext.Calendars);
                }
                return _calendarRepo;
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

        private IRepository<Cost>? _costRepo;
        public IRepository<Cost> CostRepo
        {
            get
            {
                if (_costRepo == null)
                {
                    _costRepo = new Repository<Cost>(_humanActivitiesDataContext.Costs);
                }
                return _costRepo;
            }
        }

        private IRepository<Event>? _eventRepo;
        public IRepository<Event> EventRepo
        {
            get
            {
                if (_eventRepo == null)
                {
                    _eventRepo = new Repository<Event>(_humanActivitiesDataContext.Events);
                }
                return _eventRepo;
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

        private IRepository<ToDoListTemplate>? _toDoListTemplateRepo;
        public IRepository<ToDoListTemplate> TodoListTemplateRepo
        {
            get
            {
                if (_toDoListTemplateRepo == null)
                {
                    _toDoListTemplateRepo = new Repository<ToDoListTemplate>(_humanActivitiesDataContext.ToDoListTemplates);
                }
                return _toDoListTemplateRepo;
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
