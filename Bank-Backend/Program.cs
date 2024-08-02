using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BankContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BankDB")));
builder.Services.AddScoped<IBankRepository>(repo => new BankRepository(builder.Configuration.GetConnectionString("BankDB") ?? ""));

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();
// public class Program
// {
//     public static void Main(string[] args)
//     {

//     }
// }
