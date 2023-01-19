using API.Exceptions;
using API.Models.ToDoLists;
using API.Models.Sections;
using API.Models.Tasks;
using DAL;
using DAL.DataEntities;
using DAL.UnitOfWork;
using Task = DAL.DataEntities.Task;
using Microsoft.IdentityModel.Tokens;
using DAL.CommonVariables;

namespace API.Services
{
    public class ToDoListsService
    {
        private readonly IUnitOfWork _uow;

        public ToDoListsService()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        public async Task<CreateToDoListResult> CreateToDoList(CreateToDoListRequest request, int userId)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList name can't be empty");

            var toDoList = new ToDoListTemplate
            {
                Name = request.Name,
                Description = request.Description,
                IsFavourite = request.IsFavourite,
                ToDoListType = request.ToDoListType,
                UserId = userId,
                Sections = request.Sections?.Select(s => new Section
                {
                    Name = s.Name,
                    Tasks = s.Tasks?.Select(t => new Task
                    {
                        Name = t.Name,
                        IsDone = t.IsDone,
                        Priority = t.Priority,
                        Notes = t.Notes
                    }).ToList()
                }).ToList()
            };

            await _uow.TodoListTemplateRepo.AddAsync(toDoList);
            await _uow.CompleteAsync();

            return new CreateToDoListResult("ToDoList has been created succesfully!");
        }

        public async Task<GetToDoListResult> GetToDoList(string toDoListGuidAsString)
        {
            if (!Guid.TryParse(toDoListGuidAsString, out Guid toDoListGuid))
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");

            var toDoList = await _uow.TodoListTemplateRepo.SingleOrDefaultAsync(t => t.ToDoListTemplateGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            return new GetToDoListResult
            {
                ToDoListGuid = toDoList.ToDoListTemplateGuid.ToString(),
                Name = toDoList.Name,
                Description = toDoList.Description,
                IsFavourite = toDoList.IsFavourite,
                ToDoListType = toDoList.ToDoListType,
                Sections = toDoList.Sections?.Select(s => s.ToGetSectionResult())
            };
        }

        public async Task<IEnumerable<GetToDoListResult>> GetToDoLists(int userId, string groupGuid)
        {
            var toDoLists = new List<GetToDoListResult>();

            if (groupGuid.IsNullOrEmpty())
            {
                toDoLists = (await _uow.TodoListTemplateRepo.WhereAsync(td => td.UserId == userId)).Select(td => td.ToGetToDoListResult()).ToList();
            }
            else
            {
                if (Guid.TryParse(groupGuid, out Guid toDoListGuidParsed))
                    toDoLists = (await _uow.TodoListTemplateRepo.WhereAsync(td => td.ToDoListTemplateGuid == toDoListGuidParsed)).Select(td => td.ToGetToDoListResult()).ToList();
                else
                    throw new OperationException(StatusCodes.Status400BadRequest, "Group guid is incorrect");
            }

            return toDoLists;
        }

        public async Task<IEnumerable<GetToDoListResult>> GetToDoListTemplates(int userId, string groupGuid)
        {
            var toDoLists = new List<GetToDoListResult>();

            if (groupGuid.IsNullOrEmpty())
            {
                toDoLists = (await _uow.TodoListTemplateRepo
                    .WhereAsync(td => td.UserId == userId 
                    && (td.ToDoListType == ToDoListType.Template || td.ToDoListType == ToDoListType.Base)))
                    .Select(td => td.ToGetToDoListResult()).ToList();
            }
            else
            {
                if (Guid.TryParse(groupGuid, out Guid toDoListGuidParsed))
                    toDoLists = (await _uow.TodoListTemplateRepo.WhereAsync(td => td.ToDoListTemplateGuid == toDoListGuidParsed)).Select(td => td.ToGetToDoListResult()).ToList();
                else
                    throw new OperationException(StatusCodes.Status400BadRequest, "Group guid is incorrect");
            }

            return toDoLists;
        }

        public async Task<EditToDoListResult> EditToDoList(EditToDoListRequest request)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList name can't be empty");

            if(!Guid.TryParse(request.ToDoListGuid, out Guid toDoListGuid))
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");
            }

            var toDoList = await _uow.TodoListTemplateRepo.SingleOrDefaultAsync(g => g.ToDoListTemplateGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            toDoList.Name = request.Name;
            toDoList.Description = request.Description;
            toDoList.IsFavourite = request.IsFavourite;
            toDoList.ToDoListType = request.ToDoListType;

            if (request.Sections != null)
            {
                foreach (var updatedSection in request.Sections)
                {
                    if (!Guid.TryParse(updatedSection.SectionGuid, out Guid sectionGuid))
                    {
                        throw new OperationException(StatusCodes.Status400BadRequest, "Section guid is incorrect");
                    }

                    var section = await _uow.SectionRepo.SingleOrDefaultAsync(s => s.SectionGuid == sectionGuid);

                    if (section == null)
                        throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no section like this");

                    section.Name = updatedSection.Name;

                    if (updatedSection.Tasks != null)
                    {
                        foreach (var updatedTask in updatedSection.Tasks)
                        {
                            if (!Guid.TryParse(updatedTask.TaskGuid, out Guid taskGuid))
                            {
                                throw new OperationException(StatusCodes.Status400BadRequest, "Task guid is incorrect");
                            }

                            var task = await _uow.TaskRepo.SingleOrDefaultAsync(t => t.TaskGuid == taskGuid);

                            if (task == null)
                                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no task like this");

                            task.Name = updatedTask.Name;
                            task.Priority = updatedTask.Priority;
                            task.IsDone = updatedTask.IsDone;
                            task.Notes = updatedTask.Notes;
                        }
                    }
                }
            }

            _uow.TodoListTemplateRepo.Update(toDoList);
            await _uow.CompleteAsync();

            return new EditToDoListResult("ToDoList has been edited successfully!");
        }

        public async Task<SetFavouriteResult> SetFavourite(SetFavouriteRequest request)
        {
            if (!Guid.TryParse(request.ToDoListGuid, out Guid toDoListGuid))
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");
            }

            var toDoList = await _uow.TodoListTemplateRepo.SingleOrDefaultAsync(g => g.ToDoListTemplateGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            toDoList.IsFavourite = request.IsFavourite;

            await _uow.CompleteAsync();

            return new SetFavouriteResult("ToDoList has been updated successfully!");
        }

        public async Task<SetTemplateResult> SetTemplate(SetTemplateRequest request)
        {
            if (!Guid.TryParse(request.ToDoListGuid, out Guid toDoListGuid))
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");
            }

            var toDoList = await _uow.TodoListTemplateRepo.SingleOrDefaultAsync(g => g.ToDoListTemplateGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            toDoList.ToDoListType = request.ToDoListType;

            await _uow.CompleteAsync();

            return new SetTemplateResult("ToDoList has been updated successfully!");
        }

        public async Task<DeleteToDoListResult> DeleteToDoList(string toDoListGuidAsString)
        {
            if (!Guid.TryParse(toDoListGuidAsString, out Guid toDoListGuid))
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");
            }

            var toDoList = await _uow.TodoListTemplateRepo.SingleOrDefaultAsync(t => t.ToDoListTemplateGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            _uow.TodoListTemplateRepo.Remove(toDoList);
            await _uow.CompleteAsync();

            return new DeleteToDoListResult("ToDoList has been deleted successfully!");
        }
    }
}

public static class ToDoListsServiceExtensions
{
    public static GetToDoListResult ToGetToDoListResult(this ToDoListTemplate toDoList)
    {
        return new GetToDoListResult
        {
            ToDoListGuid = toDoList.ToDoListTemplateGuid.ToString(),
            ToDoListType = toDoList.ToDoListType,
            Description = toDoList.Description,
            IsFavourite = toDoList.IsFavourite,
            Name = toDoList.Name,
            Sections = toDoList.Sections?.Select(s => s.ToGetSectionResult())
        };
    }

    public static GetSectionResult ToGetSectionResult(this Section section)
    {
        return new GetSectionResult
        {
            SectionGuid = section.SectionGuid.ToString(),
            Name = section.Name,
            Tasks = section.Tasks?.Select(t => t.ToGetSectionResult())
        };
    }

    private static GetTaskResult ToGetSectionResult(this Task task)
    {
        return new GetTaskResult
        {
            TaskGuid = task.TaskGuid.ToString(),
            IsDone = task.IsDone,
            Name = task.Name,
            Notes = task.Notes,
            Priority = task.Priority
        };
    }
}
