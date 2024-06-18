using CPApp.contract;
using CPApp.service;

namespace CPApp.Extentions
{
    public static class ServiceExtensions
    {

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<ICosmosClientConnection, CosmosClientConnection>();
            services.AddTransient<ICustomQuestionService, CustomQuestionService>();
        }
    }
}
