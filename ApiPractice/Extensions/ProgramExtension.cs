using ApiPractice.Interfaces;
using ApiPractice.Repositories;
using ApiPractice.Services;

namespace ApiPractice.Extensions
{
    public static class ProgramExtension
    {
        public static void DependecyInjection(this IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IService, Service>();
        }
    }
}
