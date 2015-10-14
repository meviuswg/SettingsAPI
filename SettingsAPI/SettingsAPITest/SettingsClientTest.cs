using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsAPIClient;
using System.Collections.Generic;


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
        public void CreateApplicationMaster()
        {
            settingsManager = new SettingsManager(_url, _masterKey);

            string applicationName = RandomString();
            string description = RandomString();
            settingsManager.CreateApplication(applicationName, description);
            Assert.AreEqual(settingsManager.Application.Name, applicationName);
            Assert.AreEqual(settingsManager.Application.Description, description);
            Assert.AreEqual(settingsManager.Directory.Name, "root");

            currentApplicationName = settingsManager.Application.Name;
        }

        [TestMethod]
        public void DeleteApplicationMaster()
        {
            CreateApplicationMaster();

            try
            {   
                bool isDeleted = settingsManager.DeleteApplication(currentApplicationName);

                Assert.IsTrue(isDeleted);

                try
                {
                    settingsManager.OpenApplication(currentApplicationName);
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
        public void CreateDirectoryMaster()
        {
            CreateApplicationMaster();

            try
            {
                string directoryName = RandomString();
                string directoryDescription = RandomString();
                settingsManager.CreateDirectory(currentApplicationName, directoryName, directoryDescription);

                Assert.AreEqual(directoryName, settingsManager.Directory.Name);
                Assert.AreEqual(directoryDescription, settingsManager.Directory.Description);

                currentDirectoryName = settingsManager.Directory.Name;

            }
            catch (SettingsException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void DeleteDirectoryMaster()
        {
            CreateDirectoryMaster();

            try
            {
                bool isDeleted = settingsManager.DeleteDirectory(currentApplicationName, currentDirectoryName);

                Assert.IsTrue(isDeleted);

                try
                {
                    settingsManager.OpenDirectory(currentApplicationName, currentDirectoryName);
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

        private static string RandomString()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
        }
    }
}
