using System;
using System.Collections.Generic;

namespace SettingsAPIRepository.Data
{
    public partial class ApiKeyData
    {
        public ApiKeyData()
        {
            this.Access = new List<DirectoryAccessData>();
        }

        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApiKey { get; set; }
        public Nullable<System.DateTime> LastUsed { get; set; }
        public bool AdminKey { get; set; }
        public bool Active { get; set; }
        public System.DateTime Created { get; set; }
        public virtual ICollection<DirectoryAccessData> Access { get; set; }
        public virtual ApplicationData Application { get; set; }
        public string Name { get;  set; }
    }
}