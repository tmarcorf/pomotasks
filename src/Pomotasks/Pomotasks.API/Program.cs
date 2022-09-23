using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pomotasks.Domain.Interfaces;
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

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITodoRepository, TodoRepository>();

            services.AddScoped(typeof(IMapper));

            // Services
            services.AddScoped<ITodoService, TodoService>();

            services.AddTransient<MapperConfigurationExpression>();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}