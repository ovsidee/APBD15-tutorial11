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
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();
        
        app.Run();
    }
}