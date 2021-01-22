namespace FoxfordHack.Services.WebApi.Query
{
    abstract class BaseQuery
    {
        protected static readonly string DefaultUserAgent = @"Mozilla/5.0 (compatible; U; ABrowse 0.6; Syllable) AppleWebKit/420+ (KHTML, like Gecko)";
        protected static readonly string DefaultURL = @"https://foxford.ru";
        public string Cookie { get; protected set; }
        public string XCSRFToken { get; protected set; }
        public int CountThreads { get; protected set; }
        public int Delay { get; protected set; }
    }
}
