namespace SettingsAPIShared
{
    public static class Constants
    {
        public static readonly int SYSTEM_MASTER_KEY_ID = -1; 
        public static readonly string SYSTEM_APPLICATION_NAME = "system";

        public static readonly string DEAULT_DIRECTORY_NAME = "root";
        public static readonly string DEAULT_DIRECTORY_DESCRIPTION = "";
        public static readonly string DEAULT_APPLICATION_DESCRIPTION = "";

        public static readonly string ERROR_APPLICATION_UNKNOWN = "Application does not exist";
        public static readonly string ERROR_APPLICATION_NO_NAME = "Application name not provided";
        public static readonly string ERROR_APPLICATION_ALREADY_EXISTS = "Application already exists";
        public static readonly string ERROR_APPLICATION_CANNOT_DELETED = "This application does not allow to be deleted";
        public static readonly string ERROR_APPLICATION_NAME_INVALID = "Application name not valid";

        public static readonly string ERROR_DIRECTORY_UNKOWN = "Directory does not exist";
        public static readonly string ERROR_DIRECTORY_NO_NAME = "Directory name not provided";
        public static readonly string ERROR_DIRECTORY_ALREADY_EXISTS = "Directory already exists";
        public static readonly string ERROR_DIRECTORY_CANNOT_DELETE = "This directory does not allow to be deleted";
        public static readonly string ERROR_DIRECTORY_NAME_INVALID = "Directory name not valid";

        public static readonly string ERROR_SETTING_NO_KEY = "Key name not provided";
        public static readonly string ERROR_SETTING_UNKNOWN = "Key does not exist";
        public static readonly string ERROR_SETTING_KEY_ALREADY_EXISTS = "Application already exists";

        public static readonly string ERROR_NO_MASTER_KEY = "No master key found";
        public static readonly string ERROR_INTERNAL_ERROR = "Internal server error.";
        public static readonly string ERROR_FORBIDDEN = "Forbidden";

        public static readonly string ERROR_VERION_ALREADY_EXISTS = "Version already exists";
        public static readonly string ERROR_VERION_UNKNOWN = "Version does not exist";
        public static readonly string ERROR_ACCESS_DENIED = "Access denied";
        public static readonly string ERROR_STORE_UNAVAILABLE = "Settings store unavailable";
        public static readonly string ERROR_STORE_EXCEPTION = "Internal store exception"; 
    }
}