using Autofac;
using Business.Implementation.Services;
using Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.ioC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<InMemoryGroupsService>().As<IGroupsService>().SingleInstance();
        }
    }
}
