using Microsoft.EntityFrameworkCore;
using UrlShortener;
using UrlShortener.Endpoints;
using UrlShortener.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    redisOptions.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.ApplyMigrations();

app.MapShortenedUrlRoutes();

app.UseHttpsRedirection();
app.UseCors();
app.Run();