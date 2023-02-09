using AOPDemo.AOP;
using Autofac;
using Autofac.Extras.DynamicProxy;
using IServices;
using Services;
using System.Reflection;

namespace AOPDemo
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;

            //直接注册某一个类和接口
            //左边的是实现类，右边的As是接口
            builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();

            builder.RegisterType<LogAOP>();//可以直接替换其他拦截器！一定要把拦截器进行注册

            //注册要通过反射创建的组件
            var servicesDllFile = Path.Combine(basePath, "Services.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);

            //.EnableInterfaceInterceptors()对目标类型启用接口拦截。拦截器将被确定，通过在类或接口上截取属性, 或添加 InterceptedBy ()
            //.InterceptedBy(typeof(BlogLogAOP));//允许将拦截器服务的列表分配给注册。
            //说人话就是，将拦截器添加到要注入容器的接口或者类之上。

            builder.RegisterAssemblyTypes(assemblysServices)//注册程序集中的所有类型。
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors()//启用接口拦截器
                      .InterceptedBy(typeof(LogAOP));//可以放一个AOP拦截器集合
        }
    }
}
