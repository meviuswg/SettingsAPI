using SettingsAPIRepository.Data;
using SettingsAPIRepository.Model;
using SettingsAPIShared;
using System.Collections.Generic;

namespace SettingsAPIRepository
{
    internal class SettingsAuthorizationRoleProvider
    {
       

        public SettingsAuthorizationRoleProvider()
        {
         
        }
        public static string[] ConstructRoles(string strKey)
        {
            List<string> roles = new List<string>();

            using (ValidationRepository repository = new ValidationRepository())
            {
                ApiKeyData data = repository.GetApiKey(strKey);

                if (data != null && data.Active)
                {
                    bool allowAdministration = data.AdminKey || data.Id == Constants.SYSTEM_MASTER_KEY_ID;

                    roles.Add(SecurityRoles.RoleReadDirectories(data.Application.Name));
                    roles.Add(SecurityRoles.RoleReadVersions(data.Application.Name));

                    if (data.Id == Constants.SYSTEM_MASTER_KEY_ID)
                    {
                        roles.Add(SecurityRoles.RoleCreateApplication());
                        roles.Add(SecurityRoles.RoleDeleteApplication(data.Application.Name));
                        roles.Add(SecurityRoles.RoleReadApiKeys());
                    }

                    foreach (var item in data.Access)
                    {
                        if (allowAdministration)
                        {
                            AddRoles(SecurityRoles.RoleDeleteDirectory(item.Directory.Application.Name, item.Directory.Name), roles);
                            AddRoles(SecurityRoles.RoleCreateDirectory(item.Directory.Application.Name), roles);
                            AddRoles(SecurityRoles.RoleDeleteDirectories(item.Directory.Application.Name), roles);
                            AddRoles(SecurityRoles.RoleCreateVersion(item.Directory.Application.Name), roles);
                            AddRoles(SecurityRoles.RoleDeleteVersion(item.Directory.Application.Name), roles);
                            AddRoles(SecurityRoles.RoleEditApiKey(item.Directory.Application.Name), roles);
                            AddRoles(SecurityRoles.RoleReadApiKeys(item.Directory.Application.Name), roles);
                        }

                        AddRoles(SecurityRoles.RoleReadDirectory(item.Directory.Application.Name, item.Directory.Name), roles);

                        if (item.AllowCreate)
                        {
                            AddRoles(SecurityRoles.RoleCreateSetting(item.Directory.Application.Name, item.Directory.Name), roles);
                        }

                        if (item.AllowDelete)
                        {
                            AddRoles(SecurityRoles.RoleDeleteSetting(item.Directory.Application.Name, item.Directory.Name), roles);
                        }

                        if (item.AllowWrite)
                        {
                            AddRoles(SecurityRoles.RoleWriteSetting(item.Directory.Application.Name, item.Directory.Name), roles);
                        }
                    }
                }

                return roles.ToArray();
            }
        }

        private static void AddRoles(string role, List<string> roles)
        {
            if (!roles.Contains(role))
                roles.Add(role);
        }
    }
}