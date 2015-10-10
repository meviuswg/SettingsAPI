using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData.Model
{
    public class ApplicationModel
    {
        public DateTime? Created { get; internal set; }
        public string Description { get; internal set; }
        public string Name { get; internal set; }
        public IEnumerable<VersionModel> Versions { get;  set; }
        public IEnumerable<DirectoryModel> Directories { get;  set; }
        public bool EditDirectories { get; internal set; }
    }
}
