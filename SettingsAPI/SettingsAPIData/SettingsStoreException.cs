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
            WriteLog(message);
        }

        private void WriteLog(string message)
        {
            if (Log != null)
            {
                Log.Invoke(message);
            }
        }

        public static Action<string> Log { get; set; }
    }
}
