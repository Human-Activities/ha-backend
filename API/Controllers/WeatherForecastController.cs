using DAL;
using DAL.DataContext;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IUnitOfWork _uow;
        private readonly HumanActivitiesDataContext _humanActivitiesDataContext;

        public WeatherForecastController(HumanActivitiesDataContext humanActivitiesDataContext)
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
            _humanActivitiesDataContext = humanActivitiesDataContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<string>> Get()
        {
            await _uow.CategoryRepo.AddAsync(new DAL.DataEntities.Category()
            {
                Name = "Category",
            });

            await _uow.ActivityRepo.AddAsync(new DAL.DataEntities.Activity()
            {
                Name = "Activity",
                Description = "Desc",
                IsTemplate = true,
            });

            await _uow.CompleteAsync();

            var categories = await _uow.CategoryRepo.GetAllAsync();

            return categories?.FirstOrDefault()?.Name ?? string.Empty;
        }
    }
}