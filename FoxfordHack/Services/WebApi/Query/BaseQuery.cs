using System.Net.Http;
using System.Collections.Generic;
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
        protected string ChangeCookie(HttpResponseMessage message)
        {
            var cookies = Cookie.Replace(" ", "").Split(';');
            var cookiesCollection = new Dictionary<string, string>();
            for (int i = 0; i < cookies.Length; i++)
            {
                if (string.IsNullOrEmpty(cookies[i]))
                    break;
                var keyValue = cookies[i].Split('=');
                cookiesCollection.Add(keyValue[0], keyValue[1]);
            }
            try
            {
                var newCookie = message.Headers.GetValues("Set-cookie");
                foreach (var item in newCookie)
                {
                    var keyValue = item.Split(';')[0].Split('=');
                    if (cookiesCollection.ContainsKey(keyValue[0]))
                        cookiesCollection[keyValue[0]] = keyValue[1];
                    else
                        cookiesCollection.Add(keyValue[0], keyValue[1]);
                }
                var cookie = "";
                foreach (var item in cookiesCollection)
                {
                    cookie += $"{item.Key}={item.Value};";
                }
                return cookie;
            }
            catch
            {
                return Cookie;
            }
        }
    }
}
