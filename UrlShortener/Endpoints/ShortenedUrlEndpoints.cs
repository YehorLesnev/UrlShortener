﻿using Microsoft.AspNetCore.Builder;
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

        /// <summary>
        /// Takes full url and returns shortened url with generated code
        /// </summary>

        /// <remarks>
        /// 
        ///  
        /// Sample request:
        ///
        ///     GET api/shorten
        ///     {
        ///         "url": "https://github.com/YehorLesnev"
        ///     }
        ///     
        /// </remarks>
        /// <returns> This endpoint returns shortened url with generated code</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The specified URL is invalid</response>
        [HttpPost]
        private static async Task<IResult> ShortenUrl(
            ShortenUrlRequest request,
            UrlShorteningService urlShorteningService,
            IUnitOfWork unitOfWork,
            HttpContext httpContext)
        {
            if (false == UlrValidationService.IsUrlValid(request.Url, UriKind.Absolute))
            {
                return Results.BadRequest(Constants.Constants.InvalidUrlBadRequestMessage);
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
        
        /// <summary>
        /// Redirect to original long url by code
        /// </summary>

        /// <remarks>
        /// 
        ///  NOTE: The code must be valid - contain only uppercase and lowercase english alphabet letters and numbers 0-9
        ///  and be only 7 chars long
        ///  
        /// Sample request:
        ///
        ///     GET /api/Fy91MNr
        ///     
        /// </remarks>
        /// <param name="code">Code of the shortened url</param>
        /// <returns> This endpoint redirects to original link by code.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The code is invalid or contains forbidden characters</response>
        /// <response code="404">Shortened url with given code doesn't exist</response>
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
