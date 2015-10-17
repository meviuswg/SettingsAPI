namespace SettingsAPIData.Model
{
    public class DirectoryAccessModel
    {
        public string Directory { get; set; }
        public bool Write { get; set; }
        public bool Delete { get; set; }
        public bool Create { get; set; }
    }
}