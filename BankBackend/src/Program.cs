
using BankBackend.Repository;
using BankBackend.Models;
using Microsoft.EntityFrameworkCore;
using BankBackend.Service;
using System.Text.Json.Serialization;
public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        BankRepository _bankRepository = new BankRepository(builder.Configuration.GetConnectionString("BankDB") ?? "");

        builder.Services.AddDbContext<BankContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BankDB")));
        builder.Services.AddScoped<IBankService>(repo => new BankService(_bankRepository));

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.UseCors(options =>
            options.AllowAnyHeader()
            .AllowAnyOrigin()
            // .WithOrigins("http://localhost")
            .AllowAnyMethod()
            // .AllowCredentials()
        );

        app.UseCors(options =>
            options.AllowAnyHeader()
            .AllowAnyOrigin()
            // .WithOrigins("http://localhost")
            .AllowAnyMethod()
            // .AllowCredentials()
        );
        app.Run();
        // Console.WriteLine("Hello!");
    }
}
