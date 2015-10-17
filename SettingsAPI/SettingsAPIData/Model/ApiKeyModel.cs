using System;
using System.Collections.Generic;

namespace SettingsAPIData.Model
{
    public class ApiKeyModel
    {
        public ApiKeyModel()
        {
            Access = new List<DirectoryAccessModel>();
        }
        public string Key { get; set; }
        public bool Active { get; set; }
        public bool AdminKey { get; set; }
        public string ApplicationName { get; set; }
        public DateTime? LastUsed { get; set; } 
        public List<DirectoryAccessModel> Access { get; set; }
    }
}