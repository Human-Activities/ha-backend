using API.Models.Categories;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-activity-categories")]
        [ProducesResponseType(typeof(IEnumerable<ActivityCategory>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActvityCategories()
        {
            var result = await _categoryService.GetActivityCategories();

            return Ok(result);
        }

        [HttpGet("get-bill-item-categories")]
        [ProducesResponseType(typeof(IEnumerable<BillItemCategory>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBillItemCategories()
        {
            var result = await _categoryService.GetBillItemCategories();

            return Ok(result);
        }
    }
}
