using System;
using System.IO;

namespace FoxfordHack.Services
{
    class Logging
    {
        private static readonly string tokenPath = Path.Combine(Directory.GetCurrentDirectory(), "logs.log");
        public void FixTheError (Exception ex)
            => File.AppendAllText(tokenPath, $"{DateTime.Now} {ex}\n\n");
        
    }
}
