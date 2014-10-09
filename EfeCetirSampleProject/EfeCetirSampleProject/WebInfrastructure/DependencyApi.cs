using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure.Data;
using Infrastructure.Entity;
using Infrastructure.Service;

namespace EfeCetirSampleProject.WebInfrastructure
{
    public class DependencyApi 
    {
        public static void DependencyApiInit()
        {
            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register other dependencies.
            builder.RegisterType<DataContext>().As<IDbContext>().InstancePerDependency();
            builder.RegisterGeneric(typeof(RepositoryService<>)).As(typeof(IRepository<>));
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);
            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}