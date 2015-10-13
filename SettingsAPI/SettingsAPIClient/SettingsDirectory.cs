using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingsDirectory
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool AllowCreate { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }
    }
}
