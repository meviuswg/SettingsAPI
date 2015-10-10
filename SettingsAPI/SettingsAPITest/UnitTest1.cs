using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsAPIData;
using SettingsAPIData.Model;

namespace SettingsAPITest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SettingsDataController controller = new SettingsDataController(new SettingsDbContext(), new ApiKey("a33a5f531f49480eac31d64d02163bcf"));

            SettingStore store = new SettingStore("_system", "_directory");

            if (controller.AllowRead(store))
            {
                var data = controller.GetSettings(store).ToList();
            }

            SettingModel setting = new SettingModel();

            setting.Key = "MySettingKey";
            setting.Value = "123123123123123";

            if (controller.AllowWrite(store))
            {
              controller.SaveSetting(store, setting);
            }

            var data2 = controller.GetSettings(store).ToList();

            var data3 = controller.GetSetting(store, "MySettingKey");
        }
    }
}
