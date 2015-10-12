namespace SettingsAPIData.Model
{
    public class DirectoryAccessModel
    {
        public int DirectoryId { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowCreate { get; set; }
    }
}