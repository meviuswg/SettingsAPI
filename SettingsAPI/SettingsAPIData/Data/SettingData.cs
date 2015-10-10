using SettingsAPIData.Model;
using System;
using System.Collections.Generic;

namespace SettingsAPIData.Data
{
    public partial class SettingData : IEquatable<string>, IEquatable<SettingModel>
    {
        public int ObjecId { get; set; }
        public int RepositoryId { get; set; }
        public int DirectoryId { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public virtual Directorydata Directory { get; set; }
        public virtual RepositoryData Repository { get; set; }

        public ApplicationData Application
        {
            get
            {
                if (Repository != null)
                    return Repository.Application;

                return null;
            }
        }

        public bool Match(string applicationName, int version, string directory)
        {
            return Match(applicationName, version, directory, null);
        }

        public bool Match(string applicationName, int version, string directory, int? objectId)
        {
            return applicationName.ToLower() == Application.Name.ToLower()
                && Repository.Version == version
                && directory.ToLower() == Directory.Name.ToLower()
                && objectId == null || ObjecId == objectId;
        }

        public bool Equals(string other)
        {
            return string.Equals(this.SettingKey, other, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool Equals(SettingModel other)
        {
            return string.Equals(this.SettingKey, other.Key, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
