using SettingsAPIData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SettingsAPI.App_Start
{
    public class HttpContextCachApiKey : IApiKey
    {
        public string Key
        {
            get
            {
               if(HttpContext.Current != null)
                {
                    object cachedKey = HttpContext.Current.Cache["APIKEY"];

                    if (cachedKey != null)
                        return cachedKey.ToString();
                }

                return null;
            }
        }
    }
}
