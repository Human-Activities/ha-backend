﻿using API.Exceptions;
using API.Models.Activities;
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

        var activity = new Activity
        {
            Name = request.Name,
            Description = request.Description,
            IsPrivate = request.IsPrivate
        };

        await _uow.ActivityRepo.AddAsync(activity);
        await _uow.CompleteAsync();

        return new CreateActivityResult("Activity has been created succesfully!");
    }

    public async Task<GetActivityResult> GetActivity (int activityId)
    {
        var activity = await _uow.ActivityRepo.FindAsync(activityId);

        return new GetActivityResult
        {
            Name = activity.Name,
            Description = activity.Description,
            IsPrivate = activity.IsPrivate,
            IsTemplate = activity.IsTemplate,
            Categories = activity.Categories
        };
    }

    public async Task<IEnumerable<GetActivitiesResult>> GetActivities(GetActivitiesRequest request, string userId)
    {
        var activities =  await _uow.ActivityRepo.WhereAsync(a => a.IsPrivate == request.IsPrivate && a.User.UserGuid == Guid.Parse(userId));

        return activities.Select(a => a.ToGetActivitiesResult());
    }

    public async Task<EditActivityResult> EditActivity(EditActivityRequest request)
    {
        if (request.Name.IsNullOrEmpty())
            throw new OperationException(StatusCodes.Status400BadRequest, "Activity name can't be empty");

        var activity = await _uow.ActivityRepo.FindAsync(request.Id);

        if (activity == null)
            throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no activity like this");

        activity.Name = request.Name;
        activity.Description = request.Description;
        activity.IsPrivate = request.IsPrivate;
        activity.IsTemplate = request.IsTemplate;

        _uow.ActivityRepo.Update(activity);
        await _uow.CompleteAsync();

        return new EditActivityResult("Activity has been edited successfully!");
    }

    public async Task<DeleteActivityResult> DeleteActivity (int activityId)
    {
        var activity = await _uow.ActivityRepo.FindAsync(activityId);

        if (activity == null)
            throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no activity like this");

        _uow.ActivityRepo.Remove(activity);
        await _uow.CompleteAsync();

        return new DeleteActivityResult("Activity has been deleted successfully!");
    }
}

public static class ActivityServiceExtensions
{
    public static GetActivitiesResult ToGetActivitiesResult(this Activity activity)
    {
        return new GetActivitiesResult
        {
            Id = activity.Id,
            Name = activity.Name,
            Description = activity.Description,
            IsPrivate = activity.IsPrivate,
            IsTemplate = activity.IsTemplate,
            Categories = activity.Categories
        };
    }
}



