using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<BankContext>();
builder.Services.AddScoped<IBankRepository>(repo => new BankRepository(builder.Configuration.GetConnectionString("BankDB") ?? ""));

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();
