using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsAPIClient;
using System.Threading.Tasks;
 

namespace SettingsAPITest
{
    [TestClass]
    public class AdminTest
    {
        private readonly string _masterKey = "=a33a5f531f49480eac31d64d02163bcf";
        private readonly string _url = "http://localhost/settings/api/";
        private string currentApplicationName;

        private SettingsManager settingsManager;
        private WorkingDirectoryObject currentDirectory;
        private SettingsApplication currentApplication;

        public async Task CreateApplicationMasterAsync()
        {
            if (currentApplication == null)
            {
                settingsManager = new SettingsManager(_url, _masterKey);

                string applicationName = "Settings" + Util.RandomString();
                string description = Util.RandomString();

                await settingsManager.CreateApplicationAsync(applicationName, description);
                currentApplication = await settingsManager.GetApplication(applicationName);

                Assert.AreEqual(currentApplication.Name, applicationName);
                Assert.AreEqual(currentApplication.Description, description);

                currentApplicationName = currentApplication.Name;

                currentDirectory = await settingsManager.OpenDirectoryAsync(applicationName);
            }

        }

        [TestMethod]
        public async Task GetApplicationApiKeyByApplicationName()
        {
            await CreateApplicationMasterAsync();
            var applicationName = currentApplication.Name;
            var result = await settingsManager.GetApiKey(applicationName, applicationName);

     
            Assert.IsTrue(result.AdminKey);

        }


        [TestMethod]
        public async Task CreateApiKeyAndTestAccess()
        {
            await CreateApplicationMasterAsync();
            var applicationName = currentApplication.Name;

            //Get the adminKey for this application.
            var result = await settingsManager.GetApiKey(applicationName, applicationName); 
            settingsManager = new SettingsManager(_url, result.Key);

            //Create a directory to set access for.
            await settingsManager.CreateDirectoryAsync(applicationName, "Dir1", "test");
            await settingsManager.CreateDirectoryAsync(applicationName, "Dir2", "test");

            ApiKey key = new ApiKey(); 
            //Save a single setting
            var dir = await settingsManager.OpenDirectoryAsync(applicationName, "Dir1");

            await dir.SaveAsync("testKey", true);

            key.Name = "1000"; 
            key.AdminKey = false;
            key.Active = true;
            key.Access = new System.Collections.Generic.List<ApiAccess>();

            key.Access.Add(new ApiAccess
            {
                Create = false,
                Delete = false,
                Write = false,
                Directory = "Dir1"
            });

            var newKey =  await settingsManager.CreateApiKey(applicationName, key);

            Assert.IsTrue(newKey.Active);
            Assert.AreEqual(newKey.Name, key.Name); 
            Assert.IsTrue(newKey.Access.SingleOrDefault(a => a.Directory == "Dir1") != null);
            Assert.IsTrue(newKey.Access.SingleOrDefault(a => a.Directory == "Dir2") == null); 

            ApiAccess access = newKey.Access.Single();

            Assert.IsFalse(access.Create);
            Assert.IsFalse(access.Delete);
            Assert.IsFalse(access.Write);

            settingsManager = new SettingsManager(_url, newKey.Key);

            var testDir = await settingsManager.OpenDirectoryAsync(currentApplicationName, "Dir1");

            var testSetting = await testDir.GetBooleanAsync("testKey");

            try
            {
                await testDir.SaveAsync("testKey", false);
            }
            catch (SettingAccessDeniedException) { }

            try
            {
                await testDir.DeleteAsync(0, "testKey");
            }
            catch (SettingAccessDeniedException){}


            try
            {
                await testDir.SaveAsync("testKey2", true);
            }
            catch (SettingAccessDeniedException) { } 
        }
    }
}
