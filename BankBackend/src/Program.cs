
using BankBackend.Repository;
using BankBackend.Models;
using Microsoft.EntityFrameworkCore;
using BankBackend.Service;
public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        BankRepository _bankRepository = new BankRepository(builder.Configuration.GetConnectionString("BankDB") ?? "");

        // builder.Services.AddDbContext<BankContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BankDB")));
        builder.Services.AddScoped<IBankService>(repo => new BankService(_bankRepository));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
        // Console.WriteLine("Hello!");
    }
}
