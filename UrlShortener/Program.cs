using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UrlShortener;
using UrlShortener.Endpoints;
using UrlShortener.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    swaggerOptions =>
{
    swaggerOptions.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "UrlShortener API",
        Description = "An ASP.NET Core Web API",
        Version = "1.0",

        Contact = new OpenApiContact
        {
            Name = "LinkedIn - Yehor Lesnevych",
            Url = new Uri("https://www.linkedin.com/in/yehor-lesnevych-130640158/")
        },

        License = new OpenApiLicense
        {
            Name = "GitHub - Yehor Lesnev",
            Url = new Uri("https://github.com/YehorLesnev")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    swaggerOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Development_Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.ApplyMigrations();

app.MapShortenedUrlRoutes();

app.UseHttpsRedirection();
app.UseCors(c =>
{
    c.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
});

app.Run();