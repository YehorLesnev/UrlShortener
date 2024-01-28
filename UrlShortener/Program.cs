using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using UrlShortener;
using UrlShortener.Constants;
using UrlShortener.Entities;
using UrlShortener.Extensions;
using UrlShortener.Models;
using UrlShortener.Repositories.Implementations;
using UrlShortener.Repositories.Interfaces;
using UrlShortener.Services;
using UrlShortener.Services.Static;

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
}

app.ApplyMigrations();

app.MapPost("api/shorten", async (
    ShortenUrlRequest request,
    UrlShorteningService urlShorteningService,
    IUnitOfWork unitOfWork,
    HttpContext httpContext) =>
{
    if (false == UlrValidationService.IsUrlValid(request.Url, UriKind.Absolute))
    {
        return Results.BadRequest("The specified URL is invalid: " + request.Url);
    }

    var code = await urlShorteningService.GenerateUniqueCode();

    var shortenedUrl = new ShortenedUrl(
        Guid.NewGuid(),
        request.Url,
        $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{code}",
        code,
        DateTime.UtcNow
        );

    unitOfWork.ShortenedUrlRepository.Insert(shortenedUrl);

    await unitOfWork.ShortenedUrlRepository.CommitAsync();

    return Results.Ok(shortenedUrl.ShortUrl);
});

app.MapGet("api/{code}", async (string code, IUnitOfWork unitOfWork, HttpResponse response) =>
{
    if(false == ShortenedUrlCodeValidatorService.IsCodeValid(code)) 
    {
        return Results.BadRequest(Constants.InvalidCodeBadRequestMessage);
    }

    var shortenedUrl = await unitOfWork.ShortenedUrlRepository.GetByCodeAsync(code);

    return shortenedUrl is null ? Results.NotFound() : Results.Redirect(shortenedUrl.LongUrl);
});

app.UseHttpsRedirection();
app.UseCors();
app.Run();