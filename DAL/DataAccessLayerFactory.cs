using Autofac;
using DAL.DataContext;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DAL
{
    public class DataAccessLayerFactory
    {
        private static Autofac.IContainer Container { get; set; }
        static DataAccessLayerFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<DbContextOptions<HumanActivitiesDataContext>>();
            builder.RegisterType<HumanActivitiesDataContext>().As<IHumanActivitiesDataContext>();
            builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>();
            Container = builder.Build();
        }

        public static IUnitOfWork CreateUnitOfWork()
        {
            return Container.Resolve<IUnitOfWork>();
        }
    }
}
