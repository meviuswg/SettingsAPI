using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsAPIClient;

namespace SettingsAPITest
{
    [TestClass]
    public class SettingsClientTest
    {
        private readonly string _masterKey = "=a33a5f531f49480eac31d64d02163bcf";
        private readonly string _url = "http://localhost/settings/api/";

        [TestMethod]
        public void OpenRoot()
        {
            SettingsManager settingsManager = new SettingsManager(_url, _masterKey);

            try
            {
                settingsManager.OpenApplication("SampleApplication");  
                
            }
            catch (SettingsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void SaveToRoot()
        {
            SettingsManager settingsManager = new SettingsManager(_url, _masterKey);

            try
            {
                settingsManager.OpenApplication("SampleApplication");

                settingsManager.Save("test", "lalalalalala");

                var result = settingsManager.GetString("test");

                Assert.AreEqual("test", result);

            }
            catch (SettingsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
