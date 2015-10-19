using System.ComponentModel;

namespace SettingsAPIRepository.Model
{
    [DisplayName("SaveApplication")]
    public class SaveApplicationModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string DirectoryName { get; set; }
        public string DirectoryDescription { get; set; }
    }
}