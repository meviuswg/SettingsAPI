using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
