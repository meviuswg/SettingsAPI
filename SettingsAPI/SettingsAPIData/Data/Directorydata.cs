using System;
using System.Collections.Generic;

namespace SettingsAPIData.Models
{
    public partial class Directorydata
    {
        public Directorydata()
        {
            this.Settings = new List<SettingData>();
            this.Access = new List<DirectoryAccessData>();
        }

        public int Id { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SettingData> Settings { get; set; }
        public virtual ICollection<DirectoryAccessData> Access { get; set; }
        public virtual ApplicationData Application { get; set; }
    }
}
