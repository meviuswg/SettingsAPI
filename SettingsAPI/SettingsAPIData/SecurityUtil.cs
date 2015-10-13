namespace SettingsAPIData
{
    internal static class SecurityRoles
    {
        public static string RoleCreateApplication()
        {
            return string.Format("create.application@system");
        }

        public static string RoleCreateDirectory(string applicationName)
        {
            return string.Format("create.directory@{0}", applicationName);
        }

        public static string RoleCreateSetting(string applicationName, string directoryName)
        {
            return string.Format("create.key@{0}.{1}", directoryName, applicationName);
        }

        public static string RoleCreateVersion(string applicationName)
        {
            return string.Format("create.version@{0}", applicationName);
        }

        public static string RoleDeleteApplication(string applicationName)
        {
            return string.Format("delete.application@{0}", applicationName);
        }

        public static string RoleDeleteDirectories(string applicationName)
        {
            return string.Format("delete.directory@{0}", applicationName);
        }

        public static string RoleDeleteDirectory(string applicationName, string directoryName)
        {
            return string.Format("delete.directory@{0}", directoryName, applicationName);
        }

        public static string RoleDeleteSetting(string applicationName, string directoryName)
        {
            return string.Format("delete.key@{0}.{1}", directoryName, applicationName);
        }

        public static string RoleDeleteVersion(string applicationName)
        {
            return string.Format("delete.version@{0}", applicationName);
        }

        public static string RoleReadDirectory(string applicationName, string directoryName)
        {
            return string.Format("read.directory@{0}.{1}", directoryName, applicationName);
        }

        public static string RoleUpdateApplication(string applicationName)
        {
            return string.Format("update.application@{0}", applicationName);
        }

        public static string RoleWriteSetting(string applicationName, string directoryName)
        {
            return string.Format("write.key@{0}.{1}", directoryName, applicationName);
        }
    }
}