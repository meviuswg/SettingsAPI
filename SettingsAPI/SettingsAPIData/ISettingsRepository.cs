using SettingsAPIData.Model;
using System.Collections.Generic;

namespace SettingsAPIData
{
    public interface ISettingsRepository
    {
        IEnumerable<SettingModel> GetSettings(SettingStore store);

        SettingModel GetSetting(SettingStore store, string settingKey);

        void SaveSetting(SettingStore store, SettingModel setting);

        void SaveSettings(SettingStore store, IEnumerable<SettingModel> settings); 
    }
}