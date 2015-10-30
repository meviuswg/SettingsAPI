using System;

namespace SettingsAPIClient
{


    public class SettingsException : Exception
    {
        public SettingsException(string message) : base(message)
        {
        }

        public SettingsException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}