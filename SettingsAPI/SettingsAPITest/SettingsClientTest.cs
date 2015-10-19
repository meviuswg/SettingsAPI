﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsAPIClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using SettingsAPIRepository.Util;

namespace SettingsAPITest
{
    [TestClass]
    public class SettingsClientTest
    {
        private readonly string _masterKey = "=a33a5f531f49480eac31d64d02163bcf";
        private readonly string _url = "http://localhost/settings/api/"; 
        private string currentApplicationName;
        private string currentDirectoryName;
        private SettingsManager settingsManager;

        [TestMethod]
        public async Task CreateApplicationMasterAsync()
        {
            settingsManager = new SettingsManager(_url, _masterKey);

            string applicationName = RandomString();
            string description = RandomString();
            await settingsManager.CreateApplicationAsync(applicationName, description);
            Assert.AreEqual(settingsManager.Application.Name, applicationName);
            Assert.AreEqual(settingsManager.Application.Description, description);
            Assert.AreEqual(settingsManager.CurrentDirectory.Name, "root");

            currentApplicationName = settingsManager.Application.Name;

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
                    await settingsManager.OpenApplicationAsync(currentApplicationName);
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
                string directoryName = RandomString();
                string directoryDescription = RandomString();
                await settingsManager.CreateDirectoryAsync(currentApplicationName, directoryName, directoryDescription);

                Assert.AreEqual(directoryName, settingsManager.CurrentDirectory.Name);
                Assert.AreEqual(directoryDescription, settingsManager.CurrentDirectory.Description);

                currentDirectoryName = settingsManager.CurrentDirectory.Name;

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
                bool isDeleted = await settingsManager.DeleteDirectoryAsync(currentApplicationName, currentDirectoryName);

                Assert.IsTrue(isDeleted);

                try
                {
                    await settingsManager.OpenDirectoryAsync(currentApplicationName, currentDirectoryName);
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

                Assert.IsTrue(isCreated);
                Assert.IsTrue(settingsManager.Application.Versions.Count == 2);
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
                Assert.IsTrue(settingsManager.Application.Versions.Count == 2);

                bool isDeleted = await settingsManager.DeleteApplicationVersionAsync(currentApplicationName, 2);

                Assert.IsTrue(settingsManager.Application.Versions.Count == 1);


                Assert.IsTrue(isDeleted);
            }
            catch (SettingsException ex)
            {
                Assert.Fail(ex.Message);
            }
        } 

        [TestMethod]
        public async Task SaveSettingAsync()
        {
            await CreateApplicationMasterAsync();

            var items = settingsManager.CurrentDirectory.Items;

            Assert.IsTrue(items.Count() == 0);
            string settingKey = "Sample1";
            string settingValue = RandomString();

            bool IsSaved = await settingsManager.CurrentDirectory.SaveAsync(settingKey, settingValue);

            var savedValues = await settingsManager.CurrentDirectory.GetStringAsync(settingKey);

            Assert.AreEqual(settingValue, savedValues);
            Assert.IsTrue(settingsManager.CurrentDirectory.Items.Count() == 1);
        }

        [TestMethod]
        public async Task ExistsSettingAsync()
        {
            await SaveSettingAsync();

            Assert.IsTrue(await settingsManager.CurrentDirectory.Exists(0,"Sample1"));
            Assert.IsFalse(await settingsManager.CurrentDirectory.Exists(0,"Sample2"));

            settingsManager.CurrentDirectory.UseCache = true;

            Assert.IsTrue(await settingsManager.CurrentDirectory.Exists(0, "Sample1")); 
            Assert.IsFalse(await settingsManager.CurrentDirectory.Exists(0, "Sample2"));

        }

        [TestMethod]
        public async Task AuthenticateWrongAPIKey()
        {
            settingsManager = new SettingsManager(_url, "123");

            try
            {
                await settingsManager.OpenApplicationAsync("SampleApplication");
                Assert.Fail("Allowed access using wrong key");
            }
            catch (SettingAccessDeniedException)
            {  }

            try
            {
                //unknown resource
                await settingsManager.OpenApplicationAsync(RandomString());
                Assert.Fail("Allowed access using wrong key");
            }
            catch (SettingAccessDeniedException)
            { }
        }

        [TestMethod]
        public void OpenWrongStoreUrl()
        {
            try
            {
                settingsManager = new SettingsManager("123123", "123");
            }
            catch (SettingsException)
            { 
            } 
        }

        [TestMethod]
        public void RexexTest()
        { 
            Assert.IsTrue(NameValidator.ValidateName("simple"));
            Assert.IsTrue(NameValidator.ValidateName("simple's"));
            Assert.IsTrue(NameValidator.ValidateName("simple-s"));
            Assert.IsTrue(NameValidator.ValidateName("simpl_-s"));
            Assert.IsTrue(NameValidator.ValidateName("simpl_-s"));
            Assert.IsTrue(NameValidator.ValidateName("simple as is"));
            Assert.IsTrue(NameValidator.ValidateName("simple as is"));
            Assert.IsTrue(NameValidator.ValidateName("it's simple"));
            Assert.IsTrue(NameValidator.ValidateName("simple "));
            Assert.IsTrue(NameValidator.ValidateName("simple"));
            Assert.IsTrue(NameValidator.ValidateName("sim ple"));
            Assert.IsTrue(NameValidator.ValidateName("sim_ple"));
            Assert.IsTrue(NameValidator.ValidateName("sim-ple"));
            Assert.IsFalse(NameValidator.ValidateName("sim.ple"));
            Assert.IsFalse(NameValidator.ValidateName("sim$ple"));
            Assert.IsFalse(NameValidator.ValidateName("simple!"));
            Assert.IsFalse(NameValidator.ValidateName(""));
            Assert.IsFalse(NameValidator.ValidateName("."));
            Assert.IsFalse(NameValidator.ValidateName("a"));
            Assert.IsFalse(NameValidator.ValidateName("a"));
        }

        private static string RandomString()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
        }
    }
}
