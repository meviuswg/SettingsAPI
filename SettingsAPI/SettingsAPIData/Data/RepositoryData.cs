using System;
using System.Collections.Generic;

namespace SettingsAPIData.Models
{
    public partial class RepositoryData
    {
        public RepositoryData()
        {
            this.Settings = new List<SettingData>();
        }

        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int Version { get; set; }
        public System.DateTime Created { get; set; }
        public virtual ICollection<SettingData> Settings { get; set; }
        public virtual ApplicationData Application { get; set; }
    }
}
