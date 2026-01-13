using Auth0.AspNetCore.Authentication;
using ConferenceManager.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// 1. Підключаємо сервіс Auth0

builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

var dbType = builder.Configuration["DatabaseType"];
var connectionStrings = builder.Configuration.GetSection("ConnectionStrings");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    switch (dbType)
    {
        case "Sqlite":
            options.UseSqlite(connectionStrings["Sqlite"]);
            break;
        case "SqlServer":
            options.UseSqlServer(connectionStrings["SqlServer"]);
            break;
        default:
            options.UseSqlite(connectionStrings["Sqlite"]);
            break;
    }
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // Перевірка: "Хто ти?"
app.UseAuthorization();  // Перевірка: "Чи можна тобі сюди?"

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Цей метод заповнить базу, якщо вона порожня
        ConferenceManager.Web.Data.DbInitializer.Initialize(context); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}
app.Run();
