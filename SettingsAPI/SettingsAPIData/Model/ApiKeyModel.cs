using System.Collections.Generic;

namespace SettingsAPIData.Model
{
    public class ApiKeyModel
    {
        public ApiKeyModel()
        {
            Access = new List<ApiAccessModel>();
        }

        public int Id { get; set; }
        public string Key { get; set; }
        public bool Active { get; set; }
        public bool AdminKey { get; set; }
        public List<ApiAccessModel> Access;
    }
}