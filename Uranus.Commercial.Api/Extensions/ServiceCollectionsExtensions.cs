using Microsoft.Extensions.DependencyInjection;
using Uranus.Commercial.CommandStack.Commands;
using Uranus.Commercial.CommandStack.Domain.CommandHandlers;
using Uranus.Commercial.CommandStack.Domain.EventHandlers;
using Uranus.Commercial.CommandStack.Domain.Repository;
using Uranus.Commercial.Desnormalizer.Projections.Common;
using Uranus.Commercial.Desnormalizer.Projections.ElasticSearch;
using Uranus.Commercial.Infrastructure.Data;
using Uranus.Commercial.Infrastructure.Framework;
using Uranus.Commercial.Infrastructure.Persistance.DataAccess;
using Uranus.Commercial.Infrastructure.Persistance.Repository;
using Uranus.Commercial.Messaging;
using Uranus.Commercial.QueryStack.Elastic;

namespace Uranus.Commercial.Api.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            // Busses
            services.AddSingleton<ICommandBus, RawRabbitCommandBus>();
            services.AddSingleton<IEventBus, RawRabbitEventBus>();

            // Projections
            services.AddTransient<ElasticSearchContextProvider>();
            services.AddTransient<IProjectionWriter, ElasticSearchProjectionWriter>();

            // Command Handlers
            services.AddTransient<ICommandHandler<CreateMyEntityCommand>, MyEntityCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateMyEntityCommand>, MyEntityCommandHandler>();

            // Event Handlers
            services.AddTransient<MyEntityCreatedEventHandler>();
            services.AddTransient<MyEntityCreated2EventHandler>();

            // Unit Of Work
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddTransient<IMyEntityRepository, MyEntityRepository>();
        }
    }
}
