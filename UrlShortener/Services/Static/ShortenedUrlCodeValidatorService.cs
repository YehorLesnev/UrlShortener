namespace UrlShortener.Services.Static
{
    public static class ShortenedUrlCodeValidatorService
    {
        public static bool IsCodeValid(string code)
        {
            if (code.Length == Constants.Constants.NumberOfCharsInShortLinkCode &&
                code.All(c => Constants.Constants.Alphabet.Contains(c)))
            {
                return true;
            }

            return false;
        }
    }
}
