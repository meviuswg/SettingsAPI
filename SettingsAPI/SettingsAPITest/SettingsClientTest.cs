using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsAPIClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using SettingsAPIRepository.Util;
using System.Drawing;

namespace SettingsAPITest
{
    [TestClass]
    public class SettingsClientTest
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
        public async Task SaveBoolean()
        {
            await CreateApplicationMasterAsync();

            await currentDirectory.SaveAsync("boolTrue", true);
            await currentDirectory.SaveAsync("boolFalse", false);
            await currentDirectory.SaveNullAsync("boolNull", ValueDataType.Bool);

            Assert.IsTrue((await currentDirectory.GetBooleanAsync("boolTrue")).Value);
            Assert.IsFalse((await currentDirectory.GetBooleanAsync("boolFalse")).Value);
            Assert.IsNull((await currentDirectory.GetBooleanAsync("boolNull")));
        }


        [TestMethod]
        public async Task SaveDateTime()
        {
            await CreateApplicationMasterAsync();

            DateTime now = DateTime.Now;

            await currentDirectory.SaveAsync("dateTime", now);
            await currentDirectory.SaveNullAsync("dateTimeNull", ValueDataType.DateTime);

            Assert.AreEqual((await currentDirectory.GetDateTimeAsync("dateTime")).Value.ToString(), now.ToString());
            Assert.IsNull((await currentDirectory.GetBooleanAsync("dateTimeNull")));
        }

        [TestMethod]
        public async Task SaveInt()
        {
            await CreateApplicationMasterAsync();

            int number1 = 1;
            int number2 = 2;

            await currentDirectory.SaveAsync("number1", number1);
            await currentDirectory.SaveAsync("number2", number2);
            await currentDirectory.SaveNullAsync("numberNull", ValueDataType.Int);

            Assert.AreEqual((await currentDirectory.GetIntAsync("number1")).Value, number1);
            Assert.AreEqual((await currentDirectory.GetIntAsync("number2")).Value, number2);
            Assert.IsNull((await currentDirectory.GetIntAsync("numberNull")));
        }

        [TestMethod]
        public async Task SaveImage()
        {
            await CreateApplicationMasterAsync();

            Image image = Image.FromFile("logo.png");

            await currentDirectory.SaveAsync("image", image); 
            await currentDirectory.SaveNullAsync("imageNull", ValueDataType.Image);

            Assert.AreEqual((await currentDirectory.GetImageAsync("image")).RawFormat.ToString(), image.RawFormat.ToString()); 
            Assert.IsNull((await currentDirectory.GetIntAsync("imageNull")));
        }

    }
}
