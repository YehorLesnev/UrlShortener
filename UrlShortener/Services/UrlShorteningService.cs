namespace UrlShortener.Services
{
    using Constants;
    using Microsoft.EntityFrameworkCore;

    public class UrlShorteningService
    {
        // For generating random url code
        private readonly Random _random = new();

        // db context
        private readonly ApplicationDbContext _dbContext;

        public UrlShorteningService(ApplicationDbContext context)
        {
            this._dbContext = context;
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

                // return if the code is unique
                if (false == await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
            }
        }
    }
}
