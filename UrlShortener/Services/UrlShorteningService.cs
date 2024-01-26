using UrlShortener.Repositories.Interfaces;

namespace UrlShortener.Services
{
    using Constants;
    using Microsoft.EntityFrameworkCore;
    using UrlShortener.Repositories.Implementations;

    public class UrlShorteningService
    {
        // For generating random url code
        private readonly Random _random = new();

        // _dbContext context
        private readonly IUnitOfWork _unitOfWork;

        // Constructor for DI
        public UrlShorteningService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateUniqueCode()
        {
            var codeChars = new char[Constants.NumberOfCharsInShortLink];
            int alphabetLength = Constants.Alphabet.Length;

            while (true)
            {
                for (int i = 0; i < Constants.NumberOfCharsInShortLink; ++i)
                {
                    int randomIndex = _random.Next(alphabetLength);

                    codeChars[i] = Constants.Alphabet[randomIndex];
                }

                string code = new string(codeChars);

                // return the code if it is unique
                if (await _unitOfWork.ShortenedUrlRepository.GetByCodeAsync(code) is null)
                {
                    return code;
                }
            }
        }
    }
}
