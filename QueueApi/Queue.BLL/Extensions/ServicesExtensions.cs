using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queue.BLL.Services;
using Queue.BLL.Services.Interfaces;
using Queue.BLL.Validators;
using Queue.DAL.Context;
using Queue.DAL.Repositories;
using Queue.DAL.Repositories.Interfaces;

namespace Queue.BLL.Extensions
{
    public static class ServicesExtensions
    {
        
        public static IServiceCollection AddQueueServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString:QueueDatabase"];

            services.AddDbContext<QueueDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("QueueDatabase")));
            services.AddDbContext<ImportExportDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ImportExportDatabase")));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<SearchGroupsRequestValidator>();


            services.AddScoped<IQueueRepository, QueueRepository>();
            services.AddScoped<IQueueServices, QueueServices>();
            services.AddScoped<IImportExportRepository, ImportExportRepository>();
            services.AddScoped<IImportExportServices, ImportExportServices>();


            return services;
        }
    }
}
