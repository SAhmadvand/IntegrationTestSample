using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleWeb.Infrastructure.Persistence.EntityFrameworkSQL;

namespace IntegrationTest;

public class BaseWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(option =>
        {
            option.AddEnvironmentVariables();
            option.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            option.AddJsonFile("appsettings.test.json", true, true);
            option.Build();
        });
        
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (dbContextDescriptor is not null) 
                services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbConnection));

            if (dbConnectionDescriptor is not null) 
                services.Remove(dbConnectionDescriptor);

            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                var connection =  container.GetRequiredService<IConfiguration>()
                    .GetConnectionString("Default");
                options.UseSqlServer(connection);
            });
        });

        // if you want to use test db, you can enter necessary configurations in appsettings.test.json
        // and change this method input to 'test'
        builder.UseEnvironment("Development");
    }
    
}