using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using ResearchProject.Configuration;
using ResearchProject.Data;
using ResearchProject.Factories;
using ResearchProject.Repositories;
using ResearchProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind DatabaseSettings from config
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
var dbSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>()!;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product API",
        Version = "v1",
        Description = "A simple CRUD API for managing products."
    });
});

// --- Database contexts ---
// SQL (EF Core + PostgreSQL) — scoped per request
builder.Services.AddDbContext<SqlDbContext>(options =>
    options.UseNpgsql(dbSettings.PostgreSql.ConnectionString));

// MongoDB — singleton (MongoClient manages its own connection pool)
builder.Services.AddSingleton<MongoDbContext>();

// --- Repository implementations (both registered; factory picks the right one) ---
builder.Services.AddScoped<SqlProductRepository>();
builder.Services.AddScoped<MongoProductRepository>();

// --- Factory ---
builder.Services.AddScoped<IProductRepositoryFactory, ProductRepositoryFactory>();

// IProductRepository resolved dynamically at runtime via factory based on DefaultDb
builder.Services.AddScoped<IProductRepository>(provider =>
    provider.GetRequiredService<IProductRepositoryFactory>().Create());

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Apply EF Core migrations only when the active DB is PostgreSql
if (dbSettings.DefaultDb == "PostgreSql")
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<SqlDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1");
    options.RoutePrefix = "swagger";
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
