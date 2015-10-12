namespace SettingsAPIData.Model
{
    public class ApiAccessModel
    {
        public string ApplicationName { get; set; }
        public string DirectoryName { get; set; }
        public bool AllowCreate { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }
    }
}