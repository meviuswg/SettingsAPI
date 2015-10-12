using System;
using System.Collections.Generic;

namespace SettingsAPIData.Data
{
    public partial class DirectoryData
    {
        public DirectoryData()
        {
            this.Settings = new List<SettingData>();
            this.Access = new List<DirectoryAccessData>();
        }

        public int Id { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<SettingData> Settings { get; set; }
        public virtual ICollection<DirectoryAccessData> Access { get; set; }
        public virtual ApplicationData Application { get; set; }
    }
}