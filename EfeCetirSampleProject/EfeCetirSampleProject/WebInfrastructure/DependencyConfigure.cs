using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using EfeCetirSampleProject.Controllers.api;
using Infrastructure.Core.Tools;
using Infrastructure.Data;
using Infrastructure.Entity;
using Infrastructure.Service;

namespace EfeCetirSampleProject.WebInfrastructure
{
    public class DependencyConfigure
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            DependencyResolver.SetResolver(
                new Dependency(RegisterServices(builder))
                );
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(
                typeof(MvcApplication).Assembly
                ).PropertiesAutowired();

            //deal with your dependencies here
           // builder.RegisterAssemblyTypes(
           //Assembly.GetExecutingAssembly())
           //    .Where(t => 
           //      !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t)).InstancePerDependency();

            builder.RegisterType<DataContext>().As<IDbContext>().InstancePerDependency();
            builder.RegisterGeneric(typeof(RepositoryService<>)).As(typeof(IRepository<>));
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            return builder.Build();
        }
    }
}