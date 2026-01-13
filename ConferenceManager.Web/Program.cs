using Auth0.AspNetCore.Authentication;
using ConferenceManager.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Підключаємо сервіс Auth0
builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

// 2. Налаштування Бази Даних
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

// 3. НОВЕ: Налаштування API, Версійності та Swagger

// Додає підтримку API
builder.Services.AddEndpointsApiExplorer();

// Генератор Swagger
builder.Services.AddSwaggerGen(c =>
{
    // Явно реєструємо документацію для версії v1
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Conference API V1", Version = "v1" });
    
    // Явно реєструємо документацію для версії v2
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Conference API V2", Version = "v2" });
    
    // Додаємо підтримку версійності, щоб Swagger розумів, який контролер куди відноситься
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

// Налаштування версійності (v1.0, v2.0)
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// Допомагає Swagger розуміти версії
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  

// 4. НОВЕ: Вмикаємо Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conference API V1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Conference API V2");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        ConferenceManager.Web.Data.DbInitializer.Initialize(context); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();