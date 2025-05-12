using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Core.Interfaces.Repositories;
using MonolitoBackend.Infrastructure.Data;
using MonolitoBackend.Infrastructure.Repositories;
using MonolitoBackend.Infrastructure.Services;
using MonolitoBackend.Core.Interfaces.Services;
using MonolitoBackend.Infrastructure.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biblioteca Digital API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));


// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

// Register services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biblioteca Digital API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply migrations automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();