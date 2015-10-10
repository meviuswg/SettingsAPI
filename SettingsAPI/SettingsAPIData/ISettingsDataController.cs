using System.Collections.Generic;
using SettingsAPIData.Model;

namespace SettingsAPIData
{
    public interface ISettingsDataController
    {
        IEnumerable<SettingModel> GetSettings(SettingStore store);
        SettingModel GetSetting(SettingStore store, string settingKey);
        void SaveSetting(SettingStore store, SettingModel setting);
        void SaveSettings(SettingStore store, IEnumerable<SettingModel> settings);
        bool AllowRead(SettingStore store);
        bool AllowWrite(SettingStore store);
        bool AllowDelete(SettingStore store);
        bool AllowCreate(SettingStore store);
        bool Exists(SettingStore store);
        bool Exists(SettingStore store, string settingKey);
    }
}