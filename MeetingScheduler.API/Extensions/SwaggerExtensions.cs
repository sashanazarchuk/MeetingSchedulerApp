using Microsoft.OpenApi.Models;

namespace MeetingScheduler.API.Extensions
{
    public static class SwaggerExtensions
    {

        // Registers Swagger generation services
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meeting Scheduler API", Version = "v1" });
            });

            return services;
        }

        // Enables Swagger and Swagger UI middleware
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meeting Scheduler API v1"));

            return app;
        }
    }
}
