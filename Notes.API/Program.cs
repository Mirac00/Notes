using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Notes.API.Data;
using Notes.API.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TableDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("NotesDbConnectionString")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<TableDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.SlidingExpiration = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Create roles if they don't exist
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    if (!await roleManager.RoleExistsAsync("user"))
    {
        await roleManager.CreateAsync(new Role { Name = "user" });
    }
    if (!await roleManager.RoleExistsAsync("admin"))
    {
        await roleManager.CreateAsync(new Role { Name = "admin" });
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();




