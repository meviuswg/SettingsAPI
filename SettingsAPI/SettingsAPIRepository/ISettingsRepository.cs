using SettingsAPIRepository.Model;
using System.Collections.Generic;

namespace SettingsAPIRepository
{
    public interface ISettingsRepository
    {
        IEnumerable<SettingModel> GetSettings(SettingStore store);
        IEnumerable<SettingModel> GetSettings(SettingStore store, int objectId); 
        SettingModel GetSetting(SettingStore store, string settingKey, int objectId); 

        void SaveSetting(SettingStore store, SettingModel setting);

        void DeleteSetting(SettingStore store, SettingModel setting);

        void SaveSettings(SettingStore store, IEnumerable<SettingModel> settings); 
    }
}