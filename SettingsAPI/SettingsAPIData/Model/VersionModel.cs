using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData.Model
{
    [DisplayName("Version")]
    public class VersionModel
    {
        public int Version { get; set; }

        public DateTime Created { get; set; }
    }
}
