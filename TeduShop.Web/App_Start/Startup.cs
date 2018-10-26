﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Autofac;
using System.Reflection;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Service;
using System.Web.Mvc;
using System.Web.Http;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using TeduShop.Data;

// cài đặt auto startup khi ứng dụng đc chạy.
[assembly: OwinStartup(typeof(TeduShop.Web.App_Start.Startup))]

namespace TeduShop.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            ConfigAutofac(app);
        }
        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<TeduShopDbContext>().AsSelf().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(PostCategoryRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(PostCategoryService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            //gán tất cả các registor này vào 1 thùng chứa.
            Autofac.IContainer container = builder.Build();

            //thay thế cơ chế mặc định bằng autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //set cả Api và cả Controller đều sử dụng chung.
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver

        }
    }
}
