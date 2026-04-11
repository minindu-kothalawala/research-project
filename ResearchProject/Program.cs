using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using ResearchProject.Configuration;
using ResearchProject.Data;
using ResearchProject.Factories;
using ResearchProject.IRepositories;
using ResearchProject.IRepository;
using ResearchProject.IServices;
using ResearchProject.Models;
using ResearchProject.Repositories;
using ResearchProject.Repository;
using ResearchProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind DatabaseSettings from config
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
var dbSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>()!;

// Add services to the container.
builder.Services.AddControllers();

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
builder.Services.AddDbContext<SqlDbContext>(options =>
    options.UseNpgsql(dbSettings.PostgreSql.ConnectionString));

builder.Services.AddSingleton<MongoDbContext>();

// --- Generic repositories (open generics — one registration covers ALL entity types) ---
builder.Services.AddScoped(typeof(SqlRepository<>));
builder.Services.AddScoped(typeof(MongoRepository<>));

// --- Factory: reads DefaultDb at runtime and returns the correct generic repository ---
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

// --- IRepository<T> resolved via factory for each entity ---
builder.Services.AddScoped<IRepository<Product>>(provider =>
    provider.GetRequiredService<IRepositoryFactory>().Create<Product>());

builder.Services.AddScoped<IRepository<User>>(provider =>
    provider.GetRequiredService<IRepositoryFactory>().Create<User>());

// --- Specific repositories (wrap generic; home for entity-specific logic) ---
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// --- Application services ---
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();

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
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
