using System;
using System.Collections.Generic;

namespace SettingsAPIData.Data
{
    public partial class DirectoryAccessData
    {
        public int ApiKeyId { get; set; }
        public int DirectoryId { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowCreate { get; set; }
        public virtual Directorydata Directory { get; set; }
        public virtual ApiKeyData ApiKey { get; set; }
    }
}
