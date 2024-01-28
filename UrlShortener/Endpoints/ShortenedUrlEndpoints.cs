using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Entities;
using UrlShortener.Models;
using UrlShortener.Repositories.Interfaces;
using UrlShortener.Services;
using UrlShortener.Services.Static;

namespace UrlShortener.Endpoints
{
    /// <summary>
    /// 
    /// </summary>
    public static class ShortenedUrlEndpoints
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void MapShortenedUrlRoutes(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/shorten", ShortenUrl);
            app.MapGet("api/{code}", GetUrlByCode);
        }

        [HttpPost]
        private static async Task<IResult> ShortenUrl(
            ShortenUrlRequest request,
            UrlShorteningService urlShorteningService,
            IUnitOfWork unitOfWork,
            HttpContext httpContext)
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
        }
        
        [HttpGet]
        private static async Task<IResult> GetUrlByCode(
            string code,
            IUnitOfWork unitOfWork,
            HttpResponse response)
        {
            if (false == ShortenedUrlCodeValidatorService.IsCodeValid(code))
            {
                return Results.BadRequest(Constants.Constants.InvalidCodeBadRequestMessage);
            }

            var shortenedUrl = await unitOfWork.ShortenedUrlRepository.GetByCodeAsync(code);

            return shortenedUrl is null ? Results.NotFound() : Results.Redirect(shortenedUrl.LongUrl);
        }
    }
}
