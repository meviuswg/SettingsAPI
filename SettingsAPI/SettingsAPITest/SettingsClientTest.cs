using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsAPIClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using SettingsAPIRepository.Util;
using System.Drawing;
using SettingsAPIClient.Util;

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
        public async Task SaveString()
        {
            await CreateApplicationMasterAsync();
 
            await currentDirectory.SaveAsync("string1", "string1");
            await currentDirectory.SaveAsync("string2", "string2");
            await currentDirectory.SaveNullAsync("stringNull", ValueDataType.String);

            Assert.AreEqual(await currentDirectory.GetStringAsync("string1"), "string1");
            Assert.AreEqual(await currentDirectory.GetStringAsync("string2"), "string2");
            Assert.IsNull((await currentDirectory.GetStringAsync("stringNull")));
        }

        [TestMethod]
        public async Task SaveByeArray()
        {
            await CreateApplicationMasterAsync();
            ImageConverter converter = new ImageConverter(); 
            byte[] byteArray = (byte[])converter.ConvertTo(Image.FromFile("logo.png"), typeof(byte[]));

            await currentDirectory.SaveAsync("byteArray", byteArray);
            await currentDirectory.SaveNullAsync("byteArrayNull", ValueDataType.ByteArray);


            Assert.AreEqual((await currentDirectory.GetByteArrayAsync("byteArray")).Length, byteArray.Length); 
            Assert.IsNull((await currentDirectory.GetByteArrayAsync("byteArrayNull")));
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


        [TestMethod]
        public async Task SettingsPerObject()
        {
            await CreateApplicationMasterAsync();

            List<Setting> settings = new List<Setting>();

            settings.Add(new Setting { ObjectId = 0, Key = Util.RandomString(), Value = Util.RandomString() });
            settings.Add(new Setting { ObjectId = 1, Key = Util.RandomString(), Value = Util.RandomString() });

            await currentDirectory.SaveAsync(settings);


            currentDirectory = await settingsManager.OpenDirectoryAsync(currentApplicationName, currentDirectory.Name); 
            Assert.IsTrue(currentDirectory.Items.Count() == 2);

            currentDirectory.ObjectID = 1; 
            Assert.IsTrue(currentDirectory.Items.Count() == 1);


            await currentDirectory.SaveAsync("test", "tst"); 
            Assert.IsTrue(currentDirectory.Items.Count() == 2);

            Assert.IsTrue(currentDirectory.Items.All(i => i.ObjectId == 1));
        }

    }
}
