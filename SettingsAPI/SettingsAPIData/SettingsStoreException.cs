using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public class SettingsStoreException : Exception
    {
        public SettingsStoreException(string message) : base(message)
        {

        }
    }
}
