using System;

namespace SettingsAPIData
{
    public class Log
    {
        private static Action<string> _logger;

        public static Action<string> Logger { get { return _logger; } set { _logger = value; } }

        public static void Message(string message)
        {
            if (_logger != null)
            {
                _logger(message);
            }
        }

        public static void Exception(Exception ex)
        {
            Message(ex.ToString());
        }
    }
}