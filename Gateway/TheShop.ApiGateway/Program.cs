using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ocelot.json dosyasýný konfigürasyona ekler
builder.Configuration.AddJsonFile(
    "ocelot.json",
    optional: false,
    reloadOnChange: true);

// Ocelot servislerini Dependency Injection sistemine ekler
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();


// Ocelot middleware'ini çalýþtýrýr
await app.UseOcelot();


app.MapGet("/", () => "Hello World!");

app.Run();
