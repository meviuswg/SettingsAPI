using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SettingsAPIRepository.Util
{
    public class NameValidator
    {
        private static Regex nameRegex = new Regex(@"^[\w\s'-]{2,50}$");
        private static Regex keyRegex = new Regex(@"^[a-zA-Z0-9]{2,50}$");

        public static bool ValidateName(string name)
        {
            if (string.Equals(name, Constants.DEAULT_DIRECTORY_NAME, System.StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(name, Constants.SYSTEM_APPLICATION_NAME, System.StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return nameRegex.IsMatch(name);
        }

        public static bool ValidateKey(string name)
        { 
            return keyRegex.IsMatch(name);
        }
    }
}
