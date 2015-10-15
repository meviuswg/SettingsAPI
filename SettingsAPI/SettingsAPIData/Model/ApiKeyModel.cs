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
        public int Id { get; set; }
        public string Key { get; set; }
        public bool Active { get; set; }
        public bool AdminKey { get; set; }
        public string ApplicationName { get; internal set; }
        public DateTime? LastUsed { get; internal set; } 
        public List<DirectoryAccessModel> Access;
    }
}