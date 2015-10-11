using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public class SettingsDuplicateException : SettingsStoreException
    {
        public SettingsDuplicateException(string message) : base(message)
        {

        }


    }
}
