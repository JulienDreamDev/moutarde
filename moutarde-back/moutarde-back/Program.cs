using Microsoft.EntityFrameworkCore;
using moutarde_back.Infrastructure.Data;
using moutarde_back.Infrastructure.Security;

namespace moutarde_back;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Configurations
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
            {
                Title =  "〽️ Moutarde - API",
                Version = "v1",
                Description = "Mini Social Network"
            });
        });
        builder.Services.AddDbContext<MoutardeDbContext>(options => options.UseNpgsql(connectionString));
        builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else // Only use HTTPS in prod.
        {
            app.UseHttpsRedirection();
        }
        
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<MoutardeDbContext>();
        
        try
        {
            // To proceed with migrations even in docker containers
            logger.LogInformation("Migrating the database...");
            dbContext.Database.Migrate();
            logger.LogInformation("Database is successfully migrated.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while migrating the database.");
            throw;
        }

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}