using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIRepository.Model
{
    public class SaveApiKeyModel
    {
        public SaveApiKeyModel()
        { 
        } 
        public bool AdminKey { get; set; } 
        public List<DirectoryAccessModel> Access { get; set; }
        public string Key { get; set; }
        public bool Active { get; set; }
        public string Name { get; internal set; }
    }
}
