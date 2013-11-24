using System;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Autofac;
using Autofac.Integration.Mef;
using Common;
using System.Collections.Generic;

namespace PluginHost
{
    public class Bootstrapper
    {
        public IContainer Configure(ComposablePartCatalog catalog = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Test>().As<ITest>().Exported(x => x.As<ITest>());
            builder.RegisterGeneric(typeof(MyRepository<>)).As(typeof(IRepository<>));
            builder.RegisterType<MyRepository<User>>().As<IRepository<User>>().Exported(x => x.As<IRepository<User>>());
            //builder.RegisterAssemblyTypes(typeof(ITest).Assembly);
            if (catalog != null)
            {
                builder.RegisterComposablePartCatalog(catalog);
            }
            var container = builder.Build();
            var scope = container.BeginLifetimeScope();
            var e = scope.Resolve<IEnumerable<IObgJob>>();
            //var s = container.Resolve<IRepository<User>>();
            //var t = container.Resolve<IRepository<Team>>();
            //var test = container.Resolve<ITest>();
            //var g = container.Resolve<IEnumerable<IObgJob>>();

            return container;
        }
    }
}
