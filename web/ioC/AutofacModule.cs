using Autofac;
using Business.Implementation.Services;
using Business.Models;
using Business.Services;
using Microsoft.Extensions.Logging;
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
            builder.RegisterType<InMemoryGroupsService>().Named<IGroupsService>("groupsService").SingleInstance();
            builder.RegisterDecorator<IGroupsService>((context, service) => new GroupsServiceDecorator(service, context.Resolve<ILogger<GroupsServiceDecorator>>()), "groupsService");
        }

        private class GroupsServiceDecorator : IGroupsService
        {
            private readonly ILogger<GroupsServiceDecorator> _logger;
            private readonly IGroupsService _inner;

            public GroupsServiceDecorator(IGroupsService inner, ILogger<GroupsServiceDecorator> logger)
            {
                _logger = logger;
                _inner = inner;
            }

            public IReadOnlyCollection<Group> GetAll()
            {
                using (var scope = _logger.BeginScope("Decorator scope: {Decorator}", nameof(GroupsServiceDecorator)))
                {
                    _logger.LogTrace("######### Helloooooo {decoratedMethod} #########", nameof(GetAll));
                    var result = _inner.GetAll();
                    _logger.LogTrace("######### GoodByeeee {decoratedMethod} #########", nameof(GetAll));
                    return result;
                }
            }

            public Group GetById(long id)
            {
                _logger.LogTrace("######### Helloooooo {decoratedMethod} #########", nameof(GetById));
                return _inner.GetById(id);
            }

            public Group Update(Group group)
            {
                _logger.LogTrace("######### Helloooooo {decoratedMethod} #########", nameof(Update));
                return _inner.Update(group);
            }

            public Group Add(Group group)
            {
                _logger.LogWarning("######### Helloooooo {decoratedMethod} #########", nameof(Add));
                return _inner.Add(group);
            }
        }
    }
}
