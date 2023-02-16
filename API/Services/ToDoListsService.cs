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

        public async Task<GetToDoListResult> CreateToDoList(CreateToDoListRequest request, int userId)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList name can't be empty");

            var toDoList = new ToDoList
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

            try
            {
                await _uow.TodoListRepo.AddAsync(toDoList);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return toDoList.ToGetToDoListResult();
        }

        public async Task<GetToDoListResult> GetToDoList(string toDoListGuidAsString)
        {
            if (!Guid.TryParse(toDoListGuidAsString, out Guid toDoListGuid))
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");

            var toDoList = await _uow.TodoListRepo.SingleOrDefaultAsync(t => t.ToDoListGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            return new GetToDoListResult
            {
                ToDoListGuid = toDoList.ToDoListGuid.ToString(),
                Name = toDoList.Name,
                Description = toDoList.Description,
                IsFavourite = toDoList.IsFavourite,
                ToDoListType = toDoList.ToDoListType,
                Sections = toDoList.Sections?.Select(s => s.ToGetSectionResult())
            };
        }

        public async Task<IEnumerable<GetToDoListResult>> GetToDoLists(int userId, string? groupGuid)
        {
            var toDoLists = new List<GetToDoListResult>();

            if (groupGuid.IsNullOrEmpty())
            {
                toDoLists = (await _uow.TodoListRepo.WhereAsync(td => td.UserId == userId)).Select(td => td.ToGetToDoListResult()).ToList();
            }
            else
            {
                if (Guid.TryParse(groupGuid, out Guid toDoListGuidParsed))
                    toDoLists = (await _uow.TodoListRepo.WhereAsync(td => td.ToDoListGuid == toDoListGuidParsed)).Select(td => td.ToGetToDoListResult()).ToList();
                else
                    throw new OperationException(StatusCodes.Status400BadRequest, "Group guid is incorrect");
            }

            return toDoLists;
        }

        public async Task<IEnumerable<GetToDoListResult>> GetToDoListTemplates(int userId, string? groupGuid)
        {
            var toDoLists = new List<GetToDoListResult>();

            if (groupGuid.IsNullOrEmpty())
            {
                toDoLists = (await _uow.TodoListRepo
                    .WhereAsync(td => td.ToDoListType == ToDoListType.Base
                    || (td.ToDoListType == ToDoListType.Template && td.UserId == userId)))
                    .Select(td => td.ToGetToDoListResult()).ToList();
            }
            else
            {
                if (Guid.TryParse(groupGuid, out Guid toDoListGuidParsed))
                    toDoLists = (await _uow.TodoListRepo.WhereAsync(td => td.ToDoListGuid == toDoListGuidParsed)).Select(td => td.ToGetToDoListResult()).ToList();
                else
                    throw new OperationException(StatusCodes.Status400BadRequest, "Group guid is incorrect");
            }

            return toDoLists;
        }

        public async Task<GetToDoListResult> EditToDoList(EditToDoListRequest request)
        {
            if (request.Name.IsNullOrEmpty())
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList name can't be empty");

            if (!Guid.TryParse(request.ToDoListGuid, out Guid toDoListGuid))
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");
            }

            var toDoList = await _uow.TodoListRepo.SingleOrDefaultAsync(g => g.ToDoListGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            toDoList.Name = request.Name;
            toDoList.Description = request.Description;
            toDoList.IsFavourite = request.IsFavourite;
            toDoList.ToDoListType = request.ToDoListType;

            if (request.Sections != null)
            {
                if (toDoList.Sections != null && toDoList.Sections.Any())
                {
                    foreach (var section in toDoList.Sections)
                    {
                        var updatedSection = request.Sections.SingleOrDefault(s => s.SectionGuid == section.SectionGuid.ToString());

                        if (updatedSection == null)
                            toDoList.Sections.Remove(section);
                        else
                        {
                            section.Name = section.Name;

                            if (updatedSection.Tasks != null && updatedSection.Tasks.Any())
                            {
                                if (section.Tasks != null)
                                {
                                    foreach (var task in section.Tasks)
                                    {
                                        var updatedTask = updatedSection.Tasks.SingleOrDefault(t => t.TaskGuid == task.TaskGuid.ToString());

                                        if (updatedTask == null)
                                            section.Tasks.Remove(task);
                                        else
                                        {
                                            task.Name = task.Name;
                                            task.Priority = task.Priority;
                                            task.IsDone = task.IsDone;
                                            task.Notes = task.Notes;
                                        }
                                    }
                                }
                                else
                                {
                                    if (section.Tasks == null)
                                        section.Tasks = new List<Task>();

                                    foreach (var newTask in updatedSection.Tasks)
                                    {
                                        section.Tasks.Add(new Task
                                        {
                                            Name = newTask.Name,
                                            Priority = newTask.Priority,
                                            IsDone = newTask.IsDone,
                                            Notes = newTask.Notes
                                        });
                                    }
                                }
                            }
                            else if (section.Tasks != null && section.Tasks.Any())
                            {
                                foreach (var taskToDelete in section.Tasks)
                                {
                                    section.Tasks.Remove(taskToDelete);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var newSection in request.Sections)
                    {
                        if (toDoList.Sections == null)
                            toDoList.Sections = new List<Section>();

                        toDoList.Sections.Add(new Section
                        {
                            Name = newSection.Name,
                            Tasks = newSection.Tasks?.Select(t => new Task
                            {
                                Name = t.Name,
                                Priority = t.Priority,
                                IsDone = t.IsDone,
                                Notes = t.Notes
                            }).ToList()
                        });
                    }
                }
            }
            else if (toDoList.Sections != null && toDoList.Sections.Any())
            {
                foreach (var sectionToDelete in toDoList.Sections)
                {
                    toDoList.Sections.Remove(sectionToDelete);
                }
            }

            try
            {
                _uow.TodoListRepo.Update(toDoList);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return toDoList.ToGetToDoListResult();
        }

        public async Task<SetFavouriteResult> SetFavourite(SetFavouriteRequest request)
        {
            if (!Guid.TryParse(request.ToDoListGuid, out Guid toDoListGuid))
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");
            }

            var toDoList = await _uow.TodoListRepo.SingleOrDefaultAsync(g => g.ToDoListGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            toDoList.IsFavourite = request.IsFavourite;

            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return new SetFavouriteResult("ToDoList has been updated successfully!");
        }

        public async Task<SetTemplateResult> SetTemplate(SetTemplateRequest request)
        {
            if (!Guid.TryParse(request.ToDoListGuid, out Guid toDoListGuid))
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");
            }

            var toDoList = await _uow.TodoListRepo.SingleOrDefaultAsync(g => g.ToDoListGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            toDoList.ToDoListType = request.ToDoListType;

            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return new SetTemplateResult("ToDoList has been updated successfully!");
        }

        public async Task<DeleteToDoListResult> DeleteToDoList(string toDoListGuidAsString)
        {
            if (!Guid.TryParse(toDoListGuidAsString, out Guid toDoListGuid))
            {
                throw new OperationException(StatusCodes.Status400BadRequest, "ToDoList guid is incorrect");
            }

            var toDoList = await _uow.TodoListRepo.SingleOrDefaultAsync(t => t.ToDoListGuid == toDoListGuid);

            if (toDoList == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "Internal server error. There is no ToDoList like this");

            try
            {
                _uow.TodoListRepo.Remove(toDoList);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return new DeleteToDoListResult("ToDoList has been deleted successfully!");
        }
    }
}

public static class ToDoListsServiceExtensions
{
    public static GetToDoListResult ToGetToDoListResult(this ToDoList toDoList)
    {
        return new GetToDoListResult
        {
            ToDoListGuid = toDoList.ToDoListGuid.ToString(),
            ToDoListType = toDoList.ToDoListType,
            Description = toDoList.Description,
            IsFavourite = toDoList.IsFavourite,
            Name = toDoList.Name,
            CreatedDate = toDoList.CreatedDate,
            Sections = toDoList.Sections?.Select(s => s.ToGetSectionResult())
        };
    }

    public static GetSectionResult ToGetSectionResult(this DAL.DataEntities.Section section)
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
