namespace MeetingScheduler.API.Extensions
{
    public static class ServiceExtensions
    {
        // Registers the API services with the dependency injection container.
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerDocumentation();
            return services;
        }
    }
}
