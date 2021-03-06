﻿using System;

namespace SettingsAPIRepository
{
    public class SettingsStoreException : Exception
    {
        public SettingsStoreException(string message) : base(message)
        {
            WriteLog(message);
        }

        public SettingsStoreException(string message, Exception ex) : base(message, ex)
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