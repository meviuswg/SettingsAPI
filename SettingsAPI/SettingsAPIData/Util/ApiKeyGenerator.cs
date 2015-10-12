using System;

namespace SettingsAPIData.Util
{
    internal class ApiKeyGenerator
    {
        public static string Create()
        {
            return Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }
    }
}