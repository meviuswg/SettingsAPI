using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsAPIClient;

namespace SettingsAPITest
{
    [TestClass]
    public class ApplicationTests
    {
        private readonly string _masterKey = "=a33a5f531f49480eac31d64d02163bcf";
        private readonly string _url = "http://localhost/settings/api/";

        private SettingsManager settingsManager;
        private string currentApplicationName;
        private SettingsApplication currentApplication;
        private WorkingDirectoryObject currentDirectory;

        [TestMethod]
        public async Task CreateApplicationMasterAsync()
        {
            settingsManager = new SettingsManager(_url, _masterKey);

            string applicationName = Util.RandomString();
            string description = Util.RandomString();

            await settingsManager.CreateApplicationAsync(applicationName, description);

            currentApplication = await settingsManager.GetApplication(applicationName);

            Assert.AreEqual(currentApplication.Name, applicationName);
            Assert.AreEqual(currentApplication.Description, description);


            currentApplicationName = currentApplication.Name;

        }

        [TestMethod]
        public async Task DeleteApplicationMasterAsync()
        {
            await CreateApplicationMasterAsync();

            try
            {
                bool isDeleted = await settingsManager.DeleteApplicationAsync(currentApplicationName);

                Assert.IsTrue(isDeleted);

                try
                {
                    await settingsManager.GetApplication(currentApplicationName);
                    Assert.Fail("Open application that was deleted");
                }
                catch (SettingNotFoundException)
                {
                    //Perfect;
                }

            }
            catch (SettingsException ex)
            {
                Assert.Fail(ex.Message);
            }
        } 
 
        [TestMethod]
        public async Task CreateDirectoryMasterAsync()
        {
            await CreateApplicationMasterAsync();

            try
            {
                string directoryName = Util.RandomString();
                string directoryDescription = Util.RandomString();

                await settingsManager.CreateDirectoryAsync(currentApplicationName, directoryName, directoryDescription);
                currentDirectory = await settingsManager.OpenDirectoryAsync(currentApplicationName, directoryName);

                Assert.AreEqual(directoryName, currentDirectory.Name);
                Assert.AreEqual(directoryDescription, currentDirectory.Description); 

            }
            catch (SettingsException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task UpdateDirectoryMasterAsync()
        {
            await CreateDirectoryMasterAsync();

            try
            {
                string newName = Util.RandomString();

                string newDescription = Util.RandomString();
 
                await settingsManager.UpdateDirectoryAsync(currentApplicationName, currentDirectory.Name, newName, newDescription);
                currentDirectory = await settingsManager.OpenDirectoryAsync(currentApplicationName, newName);

                Assert.AreEqual(newName, currentDirectory.Name);
                Assert.AreEqual(newDescription, currentDirectory.Description); 
            }
            catch (SettingsException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task CopyDirectoryMasterAsync()
        {
            await CreateDirectoryMasterAsync();

            try
            {
                string newName = Util.RandomString();

                string directoryDescription = currentDirectory.Description;

                string settingsKey = "Sample";
                bool settingValue = true;

                await currentDirectory.SaveAsync(settingsKey, settingValue);
                await settingsManager.CopyDirectoryAsync(currentApplicationName, currentDirectory.Name, newName);
                currentDirectory = await settingsManager.OpenDirectoryAsync(currentApplicationName, newName);

                Assert.AreEqual(newName, currentDirectory.Name);
                Assert.AreEqual(directoryDescription, currentDirectory.Description);
                Assert.IsTrue(await currentDirectory.Exists(settingsKey));
                Assert.IsTrue((await currentDirectory.GetBooleanAsync(settingsKey)).Value);

            }
            catch (SettingsException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task DeleteDirectoryMasterAsync()
        {
            await CreateDirectoryMasterAsync();

            try
            {
                bool isDeleted = await settingsManager.DeleteDirectoryAsync(currentApplicationName, currentDirectory.Name); 
                Assert.IsTrue(isDeleted);

                try
                {
                    await settingsManager.OpenDirectoryAsync(currentApplicationName, currentDirectory.Name);
                    Assert.Fail("Open directory that was deleted");
                }
                catch (SettingNotFoundException)
                {
                    //Perfect;
                }

            }
            catch (SettingsException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task CreateApplicationVersionMasterAsync()
        {
            await CreateApplicationMasterAsync();

            try
            {
                bool isCreated = await settingsManager.CreateApplicationVersionAsync(currentApplicationName, 2); 
                currentApplication = await settingsManager.GetApplication(currentApplicationName);

                Assert.IsTrue(isCreated);
                Assert.IsTrue(currentApplication.Versions.Count == 2);
            }
            catch (SettingsException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task DeleteApplicationVersionMasterAsync()
        {
            await CreateApplicationVersionMasterAsync();

            try
            {
                Assert.IsTrue(currentApplication.Versions.Count == 2);

                bool isDeleted = await settingsManager.DeleteApplicationVersionAsync(currentApplicationName, 2);
                currentApplication = await settingsManager.GetApplication(currentApplicationName);

                Assert.IsTrue(currentApplication.Versions.Count == 1);


                Assert.IsTrue(isDeleted);
            }
            catch (SettingsException ex)
            {
                Assert.Fail(ex.Message);
            }
        } 

    }
}
