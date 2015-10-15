using SettingsAPIData.Model;
using SettingsAPIShared;
using System.Collections.Generic;

namespace SettingsAPIData
{
    internal class SettingsAuthorizationRoleProvider
    {
        public static string[] ConstructRoles(ApiKeyModel model)
        {
            List<string> roles = new List<string>();

            if (model != null && model.Active)
            {
                bool allowAdministration = model.AdminKey || model.Id == Constants.SYSTEM_MASTER_KEY_ID;

                roles.Add(SecurityRoles.RoleReadDirectories(model.ApplicationName));
                roles.Add(SecurityRoles.RoleReadVersions(model.ApplicationName));

                if (model.Id == Constants.SYSTEM_MASTER_KEY_ID)
                {
                    roles.Add(SecurityRoles.RoleCreateApplication());
                    roles.Add(SecurityRoles.RoleDeleteApplication(model.ApplicationName));
                } 
 
                foreach (var item in model.Access)
                {
                  
                    if (allowAdministration)
                    {
                        AddRoles(SecurityRoles.RoleDeleteDirectory(item.ApplicationName, item.DirectoryName), roles);
                        AddRoles(SecurityRoles.RoleCreateDirectory(item.ApplicationName), roles);
                        AddRoles(SecurityRoles.RoleDeleteDirectories(item.ApplicationName), roles);
                        AddRoles(SecurityRoles.RoleCreateVersion(item.ApplicationName), roles);
                        AddRoles(SecurityRoles.RoleDeleteVersion(item.ApplicationName), roles);
                    }

                    AddRoles(SecurityRoles.RoleReadDirectory(item.ApplicationName, item.DirectoryName), roles);

                    if (item.AllowCreate)
                    {
                        AddRoles(SecurityRoles.RoleCreateSetting(item.ApplicationName, item.DirectoryName), roles);
                    }

                    if (item.AllowDelete)
                    {
                        AddRoles(SecurityRoles.RoleDeleteSetting(item.ApplicationName, item.DirectoryName), roles);
                    }

                    if (item.AllowWrite)
                    {
                        AddRoles(SecurityRoles.RoleWriteSetting(item.ApplicationName, item.DirectoryName), roles);
                    }
                }
            }

            return roles.ToArray();
        }

        private static void AddRoles(string role, List<string> roles)
        {
            if (!roles.Contains(role))
                roles.Add(role);
        }
    }
}