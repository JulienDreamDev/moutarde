using Microsoft.EntityFrameworkCore;
using moutarde_back.Infrastructure.Data;

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

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}