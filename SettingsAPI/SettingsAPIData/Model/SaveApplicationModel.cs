using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData.Model
{
    [DisplayName("SaveApplication")]
    public class SaveApplicationModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string DirectoryName { get; set; }
        public string DirectoryDescription { get; set; }
    }
}
