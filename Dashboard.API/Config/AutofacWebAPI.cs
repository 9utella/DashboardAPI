using Autofac;
using Autofac.Integration.WebApi;
using Dashboard.DAL.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dashboard.API.Config
{
    public class AutofacWebAPI
    {
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterService(new ContainerBuilder()));
        }
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        private static IContainer RegisterService(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // EF DbContext
            builder.RegisterType<EntityContext>().As<DbContext>().InstancePerApiRequest();
            //Repositories
            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IEntityRepository<>)).InstancePerApiRequest();
            //Services
            return builder.Build();
        }
    }
}
