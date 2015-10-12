using System;
using System.Collections.Generic;

namespace SettingsAPIData.Data
{
    public partial class ApplicationData
    {
        public ApplicationData()
        {
            this.ApiKeys = new List<ApiKeyData>();
            this.Directories = new List<DirectoryData>();
            this.Versions = new List<VersionData>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public virtual ICollection<ApiKeyData> ApiKeys { get; set; }
        public virtual ICollection<DirectoryData> Directories { get; set; }
        public virtual ICollection<VersionData> Versions { get; set; }
    }
}