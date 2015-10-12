using System;
using System.ComponentModel;

namespace SettingsAPIData.Model
{
    [DisplayName("Version")]
    public class VersionModel
    {
        public int Version { get; set; }

        public DateTime Created { get; set; }
    }
}