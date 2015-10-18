using SettingsAPIData.Model;
using System.Collections.Generic;

namespace SettingsAPIData
{
    public interface ISettingsRepository
    {
        IEnumerable<SettingModel> GetSettings(SettingStore store);
        IEnumerable<SettingModel> GetSettings(SettingStore store, int objectId); 
        SettingModel GetSetting(SettingStore store, string settingKey, int objectId); 

        void SaveSetting(SettingStore store, SettingModel setting);

        void SaveSettings(SettingStore store, IEnumerable<SettingModel> settings); 
    }
}