using Autofac;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace T.DI
{
    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //直接注册某一个类和接口
            //左边的是实现类，右边的As是接口
            //containerBuilder.RegisterType<TestServiceE>().As<ITestServiceE>().SingleInstance();




            #region 方法1   Load 适用于无接口注入

            //var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            //var assemblysServices = Assembly.Load(assemblyName);

            //containerBuilder.RegisterAssemblyTypes(assemblysServices)
            //          .AsImplementedInterfaces()
            //          .InstancePerLifetimeScope();

            //var assemblysRepository = Assembly.Load(assemblyName);

            //containerBuilder.RegisterAssemblyTypes(assemblysRepository)
            //          .AsImplementedInterfaces()
            //          .InstancePerLifetimeScope();

            #endregion

            #region

            //Autofac 基于配置文件的服务注册
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("autofac.json");
            var root = configurationBuilder.Build();
            //开始读取配置文件中的内容
            var module = new ConfigurationModule(root);
            //根据配置文件的内容注册服务
            builder.RegisterModule(module);

            #endregion

            #region 方法2  选择性注入 与方法1 一样

            //var assemblyName = typeof(Startup).Assembly.GetName().Name;

            //var assembly = Assembly.Load(assemblyName);
            //var assembly1 = Assembly.Load(assemblyName);

            //builder.RegisterAssemblyTypes(assembly, assembly1)
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces()
            //    .PropertiesAutowired();

            #endregion

            #region 方法3  使用 LoadFile 加载服务层的程序集  将程序集生成到bin目录 实现解耦 不需要引用
            //获取项目路径
            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //var ServicesDllFile = Path.Combine(basePath, "Exercise.Services.dll");//获取注入项目绝对路径
            //var assemblysServices = Assembly.LoadFile(ServicesDllFile);//直接采用加载文件的方法
            //containerBuilder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

            //var RepositoryDllFile = Path.Combine(basePath, "Exercise.Repository.dll");
            //var RepositoryServices = Assembly.LoadFile(RepositoryDllFile);//直接采用加载文件的方法
            //containerBuilder.RegisterAssemblyTypes(RepositoryServices).AsImplementedInterfaces();
            #endregion


            #region 在控制器中使用属性依赖注入，其中注入属性必须标注为public

            //在控制器中使用属性依赖注入，其中注入属性必须标注为public

            var controllersTypesInAssembly = typeof(Startup).Assembly
                .GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .ToArray();

            builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();

            #endregion

            ////一个接口有多个实现：注册所有实现的服务实例
            //builder.RegisterAssemblyTypes(Assembly.Load("AspNetCore.Ioc.Service")).As<IUserService>();
            ////一个接口有多个实现：只注册以A结尾的服务实例
            //builder.RegisterAssemblyTypes(Assembly.Load("AspNetCore.Ioc.Service")).Where(c => c.Name.EndsWith("A")).As<IUserService>();
            ////一个接口有多个实现：注册所有实现的服务实例，并排除UserServiceA服务实例
            //builder.RegisterAssemblyTypes(Assembly.Load("AspNetCore.Ioc.Service")).Except<UserServiceA>().As<IUserService>();
        }
    }
}