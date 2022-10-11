using Microsoft.EntityFrameworkCore;
using Pomotasks.Domain.Interfaces;
using Pomotasks.Domain.Mappers;
using Pomotasks.Persistence.Context;
using Pomotasks.Persistence.Interfaces;
using Pomotasks.Persistence.Repositories;
using Pomotasks.Service.Interfaces;
using Pomotasks.Service.Services;

namespace Pomotasks.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            // Database configuration
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("PomotasksDbConnection"),
                    b => b.MigrationsAssembly("Pomotasks.Persistence"));
            });

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITodoRepository, TodoRepository>();

            // Mapping
            services.AddScoped(typeof(IMapping<,>), typeof(Mapping<,>));

            // Services
            services.AddScoped<ITodoService, TodoService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Globalization
            var supportedCultures = new[] { "pt-BR", "en-US" };

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}