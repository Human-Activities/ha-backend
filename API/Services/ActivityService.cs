using API.Exceptions;
using API.Models;
using API.Models.Activities;
using API.Models.Categories;
using DAL;
using DAL.DataEntities;
using DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class ActivityService
{
    private readonly IUnitOfWork _uow;

    public ActivityService()
    {
        _uow = DataAccessLayerFactory.CreateUnitOfWork();
    }

    public async Task<CreateActivityResult> CreateActivity(CreateActivityRequest request)
    {
        if (request.Name.IsNullOrEmpty())
            throw new OperationException(StatusCodes.Status400BadRequest, "Activity name can't be empty");

        User? user = null;
        Group? group = null;

        if (Guid.TryParse(request.UserGuid, out Guid userGuid))
            user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.UserGuid == userGuid);

        if (user == null)
            throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. UserGuid is incorrect");

        if (Guid.TryParse(request.GroupGuid, out Guid groupGuid))
            group = await _uow.GroupRepo.SingleOrDefaultAsync(u => u.GroupGuid == groupGuid);

        if (!request.GroupGuid.IsNullOrEmpty() && group == null)
            throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. GroupGuid is incorrect");

        var activity = new Activity
        {
            UserId = user.Id,
            GroupId = group?.Id,
            Name = request.Name,
            Description = request.Description,
            IsPublic = request.IsPublic,
            CategoryId = request.Category.Id
        };

        try
        {
            await _uow.ActivityRepo.AddAsync(activity);
            await _uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
        }

        return (CreateActivityResult)activity.ToGetActivitiesResult();
    }

    public async Task<GetActivityResult> GetActivity(string activityGuid)
    {
        var activity = await _uow.ActivityRepo.SingleOrDefaultAsync(a => a.ActivityGuid == Guid.Parse(activityGuid));

        if (activity == null)
            throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no activity like this");

        return activity.ToGetActivitiesResult();
    }

    public async Task<IEnumerable<GetActivityResult>> GetActivities(string userGuidAsString, string? groupGuidAsString = null)
    {
        User? user = null;
        Group? group = null;
        IEnumerable<Activity> activities;

        if (groupGuidAsString.IsNullOrEmpty())
        {
            if (userGuidAsString.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. UserGuid and GroupGuid cannot be both empty");

            if (Guid.TryParse(userGuidAsString, out Guid userGuid))
                user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.UserGuid == userGuid);

            if (user == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. UserGuid is incorrect");
            else
                activities = await _uow.ActivityRepo.WhereAsync(a => a.User.UserGuid == userGuid);
        }
        else
        {
            if (Guid.TryParse(groupGuidAsString, out Guid groupGuid))
                group = await _uow.GroupRepo.SingleOrDefaultAsync(u => u.GroupGuid == groupGuid);

            if (!userGuidAsString.IsNullOrEmpty() && group == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. GroupGuid is incorrect");
            else
                activities = await _uow.ActivityRepo.WhereAsync(a => a.Group.GroupGuid == groupGuid);
        }

        return activities.Select(a => a.ToGetActivitiesResult());
    }

    public async Task<EditActivityResult> EditActivity(EditActivityRequest request)
    {
        if (request.Name.IsNullOrEmpty())
            throw new OperationException(StatusCodes.Status400BadRequest, "Activity name can't be empty");

        if (!Guid.TryParse(request.ActivityGuid, out Guid activityGuid))
            throw new OperationException(StatusCodes.Status400BadRequest, "ActivityGuid is incorrect");

        var activity = await _uow.ActivityRepo.SingleOrDefaultAsync(a => a.ActivityGuid == activityGuid);

        if (activity == null)
            throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no activity like this");

        activity.Name = request.Name;
        activity.Description = request.Description;
        activity.IsPublic = request.IsPublic;
        activity.CategoryId = request.Category.Id;

        try
        {
            _uow.ActivityRepo.Update(activity);
            await _uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
        }

        return (EditActivityResult)activity.ToGetActivitiesResult();
    }

    public async System.Threading.Tasks.Task DeleteActivity(string activityGuid)
    {
        var activity = await _uow.ActivityRepo.SingleOrDefaultAsync(a => a.ActivityGuid == Guid.Parse(activityGuid));

        if (activity == null)
            throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no activity like this");

        try
        {
            _uow.ActivityRepo.Remove(activity);
            await _uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

public static class ActivityServiceExtensions
{
    public static GetActivityResult ToGetActivitiesResult(this Activity activity)
    {
        return new GetActivityResult
        {
            ActivityGuid = activity.ActivityGuid.ToString(),
            UserGuid = activity.User.UserGuid.ToString(),
            GroupGuid = activity.Group?.GroupGuid.ToString(),
            Name = activity.Name,
            Description = activity.Description,
            IsPublic = activity.IsPublic,
            Category = new ActivityCategory
            {
                Id = activity.Category.Id,
                Name = activity.Category.Name,
                RankPoints = activity.Category.Value ?? 0
            },
            Author = new Author
            {
                AuthorGuid = activity.User.UserGuid.ToString(),
                Name = activity.User.Login
            }
        };
    }
}