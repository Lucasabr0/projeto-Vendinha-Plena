using Microsoft.EntityFrameworkCore;
using VendinhaPlena.API.Data;
using VendinhaPlena.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (builder.Configuration["DatabaseProvider"] == "PostgreSQL")
{
    builder.Services.AddDbContext<VendinhaDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    builder.Services.AddDbContext<VendinhaDbContext>(options =>
        options.UseSqlite(connectionString));
}

// Dependency Injection
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IDividaService, DividaService>();

// Swagger/OpenAPI
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
