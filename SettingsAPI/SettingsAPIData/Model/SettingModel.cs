using System;

namespace SettingsAPIData.Model
{
    public class SettingModel
    {
        public int ObjectId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Info { get; set; }
        public string TypeInfo { get; set; }
        public DateTime? Created{ get; set; }
        public DateTime? Modified{ get; set; }
    }
}