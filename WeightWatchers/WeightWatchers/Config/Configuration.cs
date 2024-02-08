using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Subscriber.Core.InterfaceService;
using Subscriber.Core.InterfaceDAL;
using Subscriber.DAL;
using Subscriber.Data.Entities;
using Subscriber.Services;

namespace Subscriber.WebApi.Config
{
    public static class Configuration
    {
        public static void ConfigurationService(this IServiceCollection services)
        {
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            services.AddScoped<ISubscriberService, SubscriberService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WeightWatchersProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
