using System.Collections.Generic;
using System.ComponentModel;

namespace SettingsAPIRepository.Model
{
    [DisplayName("Directory")]
    public class DirectoryModel
    {
        public DirectoryModel()
        { 
        }
        public bool AllowCreate { get; internal set; }
        public bool AllowDelete { get; internal set; }
        public string Description { get; internal set; }
        public int Items { get; internal set; }
        public string Name { get; internal set; }
        public bool AllowWrite { get; internal set; }

        public List<SettingModel> Settings { get; set; }
    }
}