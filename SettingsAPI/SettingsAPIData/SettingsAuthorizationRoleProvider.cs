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
                    roles.Add(SecurityRoles.RoleReadApiKeys());
                } 
 
                foreach (var item in model.Access)
                { 
                    if (allowAdministration)
                    {
                        AddRoles(SecurityRoles.RoleDeleteDirectory(item.Application, item.Directory), roles);
                        AddRoles(SecurityRoles.RoleCreateDirectory(item.Application), roles);
                        AddRoles(SecurityRoles.RoleDeleteDirectories(item.Application), roles);
                        AddRoles(SecurityRoles.RoleCreateVersion(item.Application), roles);
                        AddRoles(SecurityRoles.RoleDeleteVersion(item.Application), roles);
                        AddRoles(SecurityRoles.RoleEditApiKey(item.Application), roles);
                    }

                    AddRoles(SecurityRoles.RoleReadDirectory(item.Application, item.Directory), roles);

                    if (item.Create)
                    {
                        AddRoles(SecurityRoles.RoleCreateSetting(item.Application, item.Directory), roles);
                    }

                    if (item.Delete)
                    {
                        AddRoles(SecurityRoles.RoleDeleteSetting(item.Application, item.Directory), roles);
                    }

                    if (item.Write)
                    {
                        AddRoles(SecurityRoles.RoleWriteSetting(item.Application, item.Directory), roles);
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