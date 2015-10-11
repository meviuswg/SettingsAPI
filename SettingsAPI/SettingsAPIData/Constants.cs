using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    internal class Constants
    {
        internal static readonly int SYSTEM_MASTER_KEY_ID  = -1;
        internal static readonly string SYSTEM_DIRECTORY_NAME = "_directory";
        internal static readonly string SYSTEM_APPLICATION_NAME = "_system";

        internal static readonly string DEAULT_DIRECTORY_NAME = "Application";
        internal static readonly string DEAULT_DIRECTORY_DESCRIPTION = "";
        internal static readonly string DEAULT_APPLICATION_DESCRIPTION = "";


        internal static readonly string ERROR_APPLICATION_UNKNOWN = "Application does not exist";
        internal static readonly string ERROR_APPLICATION_NO_NAME = "Application name not provided";
        internal static readonly string ERROR_APPLICATION_ALREADY_EXISTS = "Application already exists";
        internal static readonly string ERROR_APPLICATION_CANNOT_DELETED = "This application does not allow to be deleted";

        internal static readonly string ERROR_DIRECTORY_UNKOWN = "Directory does not exist";
        internal static readonly string ERROR_DIRECTORY_NO_NAME = "Directory name not provided";
        internal static readonly string ERROR_DIRECTORY_ALREADY_EXISTS = "Directory already exists";
        internal static readonly string ERROR_DIRECTORY_CANNOT_DELETE = "This directory does not allow to be deleted";

        internal static readonly string ERROR_SETTING_NO_KEY = "Key name not provided";
        internal static readonly string ERROR_SETTING_UNKNOWN = "Key does not exist";
        internal static readonly string ERROR_SETTING_KEY_ALREADY_EXISTS = "Application already exists";

        internal static readonly string ERROR_NO_MASTER_KEY = "No master key found";

        internal static readonly string ERROR_VERION_ALREADY_EXISTS = "Version already exists";
        internal static readonly string ERROR_VERION_UNKNOWN = "Version does not exist";
        internal static readonly string ERROR_ACCESS_DENIED = "Access denied";
        internal static readonly string ERROR_STORE_UNAVAILABLE = "Settings store not available";
    }
}
