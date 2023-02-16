using DAL.UnitOfWork;
using DAL;
using API.Models.Categories;

namespace API.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        public async Task<IEnumerable<ActivityCategory>> GetActivityCategories()
        {
            return (await _uow.CategoryRepo.WhereAsync(c => c.IsActivityCategory == true)).Select(c => new ActivityCategory
            {
                Id= c.Id,
                Name= c.Name,
                RankPoints = c.Value ?? 0
            });
        }
        
        public async Task<IEnumerable<ActivityCategory>> GetBillItemCategories()
        {
            return (await _uow.CategoryRepo.WhereAsync(c => c.IsActivityCategory == false)).Select(c => new ActivityCategory
            {
                Id = c.Id,
                Name = c.Name,
            });
        }
    }
}
