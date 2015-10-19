namespace SettingsAPIRepository.Data
{
    public partial class DirectoryAccessData
    {
        public int ApiKeyId { get; set; }
        public int DirectoryId { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowCreate { get; set; }
        public virtual DirectoryData Directory { get; set; }
        public virtual ApiKeyData ApiKey { get; set; }
    }
}