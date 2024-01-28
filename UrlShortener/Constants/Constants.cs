namespace UrlShortener.Constants
{
    public class Constants
    {
        public const int NumberOfCharsInShortLinkCode = 7;
        public const int MaxLengthOfShortenedUrl = 50 + NumberOfCharsInShortLinkCode;
        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public const string InvalidCodeBadRequestMessage = "The Url code is invalid or contains forbidden characters";
    }
}
