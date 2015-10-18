using SettingsAPIData.Model;
using System;

namespace SettingsAPIData.Data
{
    public partial class SettingData : IEquatable<string>, IEquatable<SettingModel>
    {
        public int ObjecId { get; set; }
        public int VersionId { get; set; }
        public int DirectoryId { get; set; }
        public string SettingKey { get; set; }
        public string SettingInfo { get; set; }
        public string SettingTypeInfo { get; set; }
        public string SettingValue { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public virtual DirectoryData Directory { get; set; }
        public virtual VersionData Version { get; set; }

        public ApplicationData Application
        {
            get
            {
                if (Version != null)
                    return Version.Application;

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
                && Version.Version == version
                && directory.ToLower() == Directory.Name.ToLower()
                && objectId == null || ObjecId == objectId;
        }

        public bool Equals(string other)
        {
            return string.Equals(this.SettingKey, other, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool Equals(SettingModel other)
        {
            return string.Equals(this.SettingKey, other.Key, StringComparison.CurrentCultureIgnoreCase)
                && other.ObjectId == this.ObjecId;
        }
    }
}