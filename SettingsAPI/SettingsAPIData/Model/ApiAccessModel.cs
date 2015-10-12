using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData.Model
{
    public class ApiAccessModel
    {
        public string ApplicationName { get; set; } 
        public string DirectoryName { get; set; }  
        public bool AllowCreate { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; } 

    }
}
