using Domain.Abstractions;
using Domain.Abstractions.Migrators;
using Domain.Configurations;
using LinqToDB.Data;
using Microsoft.Extensions.Options;
using SoulConnection.Migrators;
using SoulConnection.Services;
using WebClients.Abstractions;
using WebClients.Implementations;

namespace SoulConnection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<ApiAccessConfiguration>(builder.Configuration.GetSection("ApiAccessConfiguration"));
        var apiAccessConfiguration = builder.Services.BuildServiceProvider()
            .GetRequiredService<IOptions<ApiAccessConfiguration>>()
            .Value;

        builder.Services.Configure<SQLiteConfiguration>(builder.Configuration.GetSection("SQLiteConfiguration"));
        var sqLiteConfiguration = builder.Services.BuildServiceProvider()
            .GetRequiredService<IOptions<SQLiteConfiguration>>()
            .Value;

        builder.Services.Configure<MigrationConfiguration>(
            builder.Configuration.GetSection(nameof(MigrationConfiguration)));
        var migrationConfiguration = builder.Services.BuildServiceProvider()
            .GetRequiredService<IOptions<MigrationConfiguration>>()
            .Value;
        
        // Add services to the container.
        builder.Services.AddSingleton(apiAccessConfiguration);
        builder.Services.AddSingleton(sqLiteConfiguration);
        builder.Services.AddSingleton(migrationConfiguration);

        builder.Services.AddSingleton(new Uri("https://soul-connection.fr"));

        builder.Services
            .AddScoped<IAuthWebClient, AuthWebClient>()
            .AddScoped<IApiAuthService, ApiAuthService>()
            .AddScoped<IHttpClientPool, HttpClientPool>()
            .AddScoped<ITipsWebClient, TipsWebClient>()
            .AddScoped<IEventsWebClient, EventsWebClient>()
            .AddScoped<IEncountersWebClient, EncountersWebClient>()
            .AddScoped<IEmployeeWebClient, EmployeeWebClient>()
            .AddScoped<ICustomersWebClient, CustomersWebClient>()
            .AddScoped<IClothesWebClient, ClothesWebClient>();

        builder.Services
            .AddScoped<ICustomerDataCollector, CustomerDataCollector>()
            .AddScoped<ICustomerDatabaseFiller, CustomerDatabaseFiller>()
            .AddScoped<IEventDatabaseFiller, EventDatabaseFiller>()
            .AddScoped<IEmployeeDatabaseFiller, EmployeeDatabaseFiller>()
            .AddScoped<IEncounterDatabaseFiller, EncounterDatabaseFiller>();

        builder.Services
            .AddScoped<ICustomerMigrator, CustomerMigrator>()
            .AddScoped<IEventMigrator, EventMigrator>()
            .AddScoped<IEmployeeMigrator, EmployeeMigrator>()
            .AddScoped<IEncounterMigrator, EncounterMigrator>();

        builder.Services.AddHostedService<DataSynchronizer>();
        
        builder.Services.AddControllers();

        builder.Services.AddScoped(provider =>
            new DataConnection(LinqToDB.DataProvider.SQLite.SQLiteTools.GetDataProvider(),
                $"Data Source={sqLiteConfiguration.PathToDatabase};"));

        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.WebHost.ConfigureKestrel((context, options) => { options.ListenAnyIP(7070); });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.MapGet("/try", async (HttpContext httpContext) =>
        {
            var client = app.Services.GetRequiredService<IClothesWebClient>();
            var result = await client.GetClotheImageAsync(1);
            httpContext.Response.Headers.Append("Content-Type", "image/png");
            httpContext.Response.StatusCode = StatusCodes.Status200OK;

            await httpContext.Response.Body.WriteAsync(result.Image);
        });

        app.Run();
    }
}