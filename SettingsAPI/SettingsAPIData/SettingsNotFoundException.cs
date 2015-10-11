using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public class SettingsNotFoundException : SettingsStoreException
    {
        public SettingsNotFoundException(string message) : base(message)
        {

        }
    }
}
