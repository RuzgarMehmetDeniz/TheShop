using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheShop.Identity.Context;
using TheShop.Identity.Entities;
using TheShop.Identity.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// SQL Server baðlantýsý
builder.Services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlServer(
      builder.Configuration.GetConnectionString("DefaultConnection"),
      sqlOptions => sqlOptions.EnableRetryOnFailure());
});

// ASP.NET Core Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

// Servisler
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
