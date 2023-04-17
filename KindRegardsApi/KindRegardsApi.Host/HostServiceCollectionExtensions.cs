using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using KindRegardsApi.Presentation.IoC;
using KindRegardsApi.Logic.IoC;
using KindRegardsApi.Data.IoC;

namespace KindRegardsApi.Host
{
    public static class HostServiceCollectionExtensions
    {
        public static void AddHostLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPresentationLayer(configuration);
            services.AddLogicLayer(configuration);
            services.AddDataLayer(configuration);
        }
    }
}
