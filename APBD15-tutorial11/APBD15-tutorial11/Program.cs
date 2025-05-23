using APBD15_tutorial11.Data;
using APBD15_tutorial11.Services;
using Microsoft.EntityFrameworkCore;

namespace APBD15_tutorial11;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        
        //adding controllers
        builder.Services.AddControllers();
        
        //swagger
        builder.Services.AddSwaggerGen();
        
        // to use DI
        builder.Services.AddScoped<IDbService, DbService>();
        
        // configuration of DbContext + connectionString
        builder.Services.AddDbContext<DatabaseContext> (options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
        );
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // <-- Включает генерацию Swagger JSON
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "APBD15 API V1");
                options.RoutePrefix = string.Empty; // Открывает Swagger по адресу http://localhost:5000/
            });
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();
        
        app.Run();
    }
}