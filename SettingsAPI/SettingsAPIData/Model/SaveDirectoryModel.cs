using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData.Model
{
    [DisplayName("SaveDirectory")]
    public class SaveDirectoryModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
