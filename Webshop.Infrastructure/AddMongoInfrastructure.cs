using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.MongoDb;
using Webshop.Infrastructure.MongoDb.Repositories;
using Webshop.Infrastructure.MongoDb.SequenceServices;
using Webshop.Infrastructure.MongoDb.UnitOfWork;

namespace Webshop.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddMongoInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        services.AddScoped<ICustomerRepository, MongoCustomerRepository>();
        services.AddScoped<IOrderRepository, MongoOrderRepository>();
        services.AddScoped<IProductCategoryRepository, MongoProductCategoryRepository>();
        services.AddScoped<IProductRepository, MongoProductRepository>();
        services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
        services.AddSingleton<SequenceService>();

        return services;
    }
}