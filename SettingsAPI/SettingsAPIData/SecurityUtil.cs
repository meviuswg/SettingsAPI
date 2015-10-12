using SettingsAPIShared;

namespace SettingsAPIData
{
    internal static class SecurityRoles
    {
        public static string RoleCreateApplication(string applicationName)
        {
            return Constants.SECURITY_ROLE_MASTER;
        }

        public static string RoleDeleteApplication(string applicationName)
        {
            return Constants.SECURITY_ROLE_MASTER;
        }

        public static string RoleWriteSetting(string applicationName, string directoryName)
        {
            return string.Format("Write-key@{0}:{1}", applicationName, directoryName);
        }

        public static string RoleCreateSetting(string applicationName, string directoryName)
        {
            return string.Format("Create-key@{0}:{1}", applicationName, directoryName);
        }

        public static string RoleDeleteSetting(string applicationName, string directoryName)
        {
            return string.Format("Delete-key@{0}:{1}", applicationName, directoryName);
        }

        public static string RoleCreateDirectory(string applicationName, string directoryName)
        {
            return Constants.SECURITY_ROLE_ADMIN;
        }

        public static string RoleDeleteDirectory(string applicationName, string directoryName)
        {
            return string.Format("Read-Dir@{0}:{1}", applicationName, directoryName);
        }

        public static string RoleDeleteDirectories(string applicationName)
        {
            return string.Format("Read-Dir@{0}:{1}", applicationName);
        }

        public static string RoleCreateVersion(string applicationName)
        {
            return Constants.SECURITY_ROLE_ADMIN;
        }

        public static string RoleDeleteVersion(string applicationName)
        {
            return Constants.SECURITY_ROLE_ADMIN;
        }
    }
}