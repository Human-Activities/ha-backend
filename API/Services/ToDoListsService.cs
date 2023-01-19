using DAL;
using DAL.UnitOfWork;

namespace API.Services
{
    public class ToDoListsService
    {
        private readonly IUnitOfWork _uow;

        public ToDoListsService()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }
    }
}
