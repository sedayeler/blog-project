using BlogProject.Application.Abstractions.Services;
using BlogProject.Infrastructure.Services;
using BlogProject.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlogProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAiService, AiService>();
            services.AddScoped<IMailService, MailService>();
        }
    }
}
