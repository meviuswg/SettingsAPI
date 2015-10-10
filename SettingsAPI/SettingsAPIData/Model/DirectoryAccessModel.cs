using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData.Model
{
    public class DirectoryAccessModel
    {
        public int DirectoryId { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; } 
        public bool AllowCreate { get; set; } 
    }
}
