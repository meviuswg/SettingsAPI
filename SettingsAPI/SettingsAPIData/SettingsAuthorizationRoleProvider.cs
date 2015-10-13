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


                if (model.Id == Constants.SYSTEM_MASTER_KEY_ID)
                {
                    roles.Add(SecurityRoles.RoleCreateApplication());
                    roles.Add(SecurityRoles.RoleDeleteApplication(model.ApplicationName));  
                }

                if (allowAdministration)
                {
                    roles.Add(SecurityRoles.RoleDeleteDirectories(model.ApplicationName));
                    roles.Add(SecurityRoles.RoleCreateDirectory(model.ApplicationName));
                    roles.Add(SecurityRoles.RoleCreateVersion(model.ApplicationName));
                    roles.Add(SecurityRoles.RoleDeleteVersion(model.ApplicationName));
                }

                foreach (var item in model.Access)
                { 
                    if (allowAdministration)
                    {
                        roles.Add(SecurityRoles.RoleDeleteDirectory(model.ApplicationName, item.DirectoryName));
                    }

                    roles.Add(SecurityRoles.RoleReadDirectory(model.ApplicationName, item.DirectoryName));

                    if (item.AllowCreate)
                    {
                        roles.Add(SecurityRoles.RoleCreateSetting(model.ApplicationName, item.DirectoryName));
                    }

                    if (item.AllowDelete)
                    {
                        roles.Add(SecurityRoles.RoleDeleteSetting(model.ApplicationName, item.DirectoryName));
                    }

                    if (item.AllowWrite)
                    {
                        roles.Add(SecurityRoles.RoleWriteSetting(model.ApplicationName, item.DirectoryName));
                    }
                }
            }

            return roles.ToArray();
        }
    }
}