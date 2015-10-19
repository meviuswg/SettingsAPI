using System;
using System.Linq;

namespace SettingsAPIRepository.Util
{
    internal class ApiKeyGenerator
    {
        public static string Create()
        {
            return Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }

        public static bool ValidateKey(string apiKey)
        { 
            System.Text.RegularExpressions.Regex lower = new System.Text.RegularExpressions.Regex("[a-zA-Z]"); 
            System.Text.RegularExpressions.Regex number = new System.Text.RegularExpressions.Regex("[0-9]");  
        
            if (apiKey.Length < 32)
                return false;

            if (apiKey.ToCharArray().Distinct().Count() < 10)
                return false;
         
            if (lower.Matches(apiKey).Count < 10)
                return false;

            if (number.Matches(apiKey).Count < 10)
                return false;

            return true;
        }

    }
}