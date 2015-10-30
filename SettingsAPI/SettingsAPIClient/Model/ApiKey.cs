using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class ApiKey
    {
        public ApiKey()
        {
             
        }
        public bool AdminKey { get; set; }
        public List<ApiAccess> Access { get; set; }
        public string Key { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public DateTime? LastUsed { get; set; }
    }
}
