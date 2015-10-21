using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SettingsAPIClient;
using SettingsAPIRepository.Util;

namespace SettingsAPITest
{
    [TestClass]
    public class OtherTests
    {
        private readonly string _masterKey = "=a33a5f531f49480eac31d64d02163bcf";
        private readonly string _url = "http://localhost/settings/api/";
        private string currentApplicationName;

        private SettingsManager settingsManager;
        [TestMethod]
        public async Task AuthenticateWrongAPIKey()
        {
            settingsManager = new SettingsManager(_url, "123");

            try
            {
                await settingsManager.OpenDirectoryAsync("SampleApplication", "root");

                Assert.Fail("Allowed access using wrong key");
            }
            catch (SettingAccessDeniedException)
            { }

            try
            {
                //unknown resource
                await settingsManager.OpenDirectoryAsync(Util.RandomString());
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
    }
}
