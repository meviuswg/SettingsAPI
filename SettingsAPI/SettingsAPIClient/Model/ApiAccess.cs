using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
   public class ApiAccess
    {
        public string Directory { get; set; }
        public bool Write { get; set; }
        public bool Delete { get; set; }
        public bool Create { get; set; }
    }
}
