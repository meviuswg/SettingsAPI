using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    internal class ExceptionMessages
    {
        public const string NETWORK_UNAVAILABLE = "Network unavailable.";
        public const string APP_UNKNON = "Application unknown";
        public const string STORE_UNKNON = "Application Store unknown";
        public const string KEY_UNKNON = "Setting Key unknown";
        public const string SERVER_ERROR = "Server Error";
    }
    public class SettingsException : Exception
    {
        public SettingsException(string message) : base(message)
        {

        }
    }
}
