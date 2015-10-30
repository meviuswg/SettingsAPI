using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingsApplication
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool AllowEdit { get; set; }

        public List<SettingsVersion> Versions { get; set; }
        public List<SettingsDirectory> Directories { get; set; }

    }
}
