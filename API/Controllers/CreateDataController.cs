using DAL;
using DAL.DataEntities;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/create-data")]
    [ApiController]
    public class CreateDataController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CreateDataController()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        [HttpPost("create-categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCategories()
        {
            try
            {
                var categories = new List<Category> {
                new Category
                {
                    Name = "walk",
                    IsActivityCategory = true,
                    Value = 3
                },
                new Category
                {
                    Name = "running",
                    IsActivityCategory = true,
                    Value = 10
                },
                new Category
                {
                    Name = "cycling",
                    IsActivityCategory = true,
                    Value = 8
                },
                new Category
                {
                    Name = "skating",
                    IsActivityCategory = true,
                    Value = 12
                },
                new Category
                {
                    Name = "skiing",
                    IsActivityCategory = true,
                    Value = 15
                },
                new Category
                {
                    Name = "snowboarding",
                    IsActivityCategory = true,
                    Value = 15
                },
                new Category
                {
                    Name = "nordic-walking",
                    IsActivityCategory = true,
                    Value = 5
                },
                new Category
                {
                    Name = "gym",
                    IsActivityCategory = true,
                    Value = 20
                },
                new Category
                {
                    Name = "streching",
                    IsActivityCategory = true,
                    Value = 10
                },
                new Category
                {
                    Name = "yoga",
                    IsActivityCategory = true,
                    Value = 10
                },
                new Category
                {
                    Name = "lifting",
                    IsActivityCategory = true,
                    Value = 20
                },
                new Category
                {
                    Name = "full body workout",
                    IsActivityCategory = true,
                    Value = 20
                },
                new Category
                {
                    Name = "swimming",
                    IsActivityCategory = true,
                    Value = 15
                },
                new Category
                {
                    Name = "climbing",
                    IsActivityCategory = true,
                    Value = 20
                },
                new Category
                {
                    Name = "ice-skating",
                    IsActivityCategory = true,
                    Value = 10
                },
                new Category
                {
                    Name = "dancing",
                    IsActivityCategory = true,
                    Value = 15
                },
                new Category
                {
                    Name = "cardio",
                    IsActivityCategory = true,
                    Value = 10
                },
                new Category
                {
                    Name = "grocery",
                    IsActivityCategory = false
                },
                new Category
                {
                    Name = "clothes",
                    IsActivityCategory = false
                },
                new Category
                {
                    Name = "bills",
                    IsActivityCategory = false
                },
            };

                await _uow.CategoryRepo.AddRangeAsync(categories);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {

            }

            return Ok();
        }

        [HttpPost("create-todolist-templates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateToDoListTemplates()
        {
            try
            {
                var todoLists = new List<ToDoList>
            {
                new ToDoList
                {
                    Name = "Week",
                    ToDoListType = DAL.CommonVariables.ToDoListType.Template,
                    IsFavourite = true,
                    Description = "Is template todolist with all days",
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Name = "Monday"
                        },
                        new Section
                        {
                            Name = "Tuesday"
                        },
                        new Section
                        {
                            Name = "Wednesday"
                        },
                        new Section
                        {
                            Name = "Thursday"
                        },
                        new Section
                        {
                            Name = "Friday"
                        },
                        new Section
                        {
                            Name = "Saturday"
                        },
                        new Section
                        {
                            Name = "Sunday"
                        }
                    }
                },
                new ToDoList
                {
                    Name = "Shopping",
                    ToDoListType = DAL.CommonVariables.ToDoListType.Template,
                    IsFavourite = true,
                    Description = "Is template todolist for shopping",
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Name = "Food"
                        },
                        new Section
                        {
                            Name = "Chemical"
                        },
                        new Section
                        {
                            Name = "Clothes"
                        },
                        new Section
                        {
                            Name = "Household"
                        }
                    }
                },
                new ToDoList
                {
                    Name = "Trip",
                    ToDoListType = DAL.CommonVariables.ToDoListType.Template,
                    IsFavourite = true,
                    Description = "Is template todolist for trips",
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Name = "Transport"
                        },
                        new Section
                        {
                            Name = "People list"
                        },
                        new Section
                        {
                         Name=    "Reservations"
                        },
                        new Section
                        {
                            Name = "Packing"
                        }
                    }
                },
                new ToDoList
                {
                    Name = "Party",
                    ToDoListType = DAL.CommonVariables.ToDoListType.Template,
                    IsFavourite = true,
                    Description = "Is template todolist for parties",
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Name = "Preparation"
                        },
                        new Section
                        {
                            Name = "Guests"
                        },
                        new Section
                        {
                            Name = "Cleaning"
                        }
                    }
                },
                new ToDoList
                {
                    Name = "Language learning",
                    ToDoListType = DAL.CommonVariables.ToDoListType.Template,
                    IsFavourite = true,
                    Description = "Is template todolist for languege learning",
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Name = "Vocabulary"
                        },
                        new Section
                        {
                            Name = "Grammar"
                        }
                    }
                },
                new ToDoList
                {
                    Name = "Exercies",
                    ToDoListType = DAL.CommonVariables.ToDoListType.Template,
                    IsFavourite = true,
                    Description = "Is template todolist for exercises",
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Name = "Warm-up"
                        },
                        new Section
                        {
                            Name = "Training"
                        },
                        new Section
                        {
                            Name = "Stretching"
                        }
                    }
                }
            };

                await _uow.TodoListRepo.AddRangeAsync(todoLists);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {

            }

            return Ok();
        }
    }
}
