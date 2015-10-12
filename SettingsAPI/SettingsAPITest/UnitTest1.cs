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
            SettingsStore repository = new SettingsStore(new SettingsDbContext());
            ApiKeyRepository apiKeyRepository = new ApiKeyRepository(new SettingsDbContext());
            SettingsAuthorizationProvider provider = new SettingsAuthorizationProvider(apiKeyRepository);

            provider.Validate("=a33a5f531f49480eac31d64d02163bcf");

            SettingsRepository controller = new SettingsRepository(repository, provider);

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

        [TestMethod]
        public void MyTestMethod()
        {
         
            SettingsStore repository = new SettingsStore(new SettingsDbContext());
            ApiKeyRepository apiKeyRepository = new ApiKeyRepository(new SettingsDbContext()); 
            SettingsAuthorizationProvider provider = new SettingsAuthorizationProvider(apiKeyRepository); 
            provider.Validate("=a33a5f531f49480eac31d64d02163bcf");

            ApplicationRepository controller = new ApplicationRepository(repository, provider);

            bool create  = provider.AllowDeleteSetting("_system", "_direotory");
            var apps = controller.GetApplications();

            var a = apps.FirstOrDefault();

            var newApp = controller.CreateApplication("FirstApp");
        }
    }
}
