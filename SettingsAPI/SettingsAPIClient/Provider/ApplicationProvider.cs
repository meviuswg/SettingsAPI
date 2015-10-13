namespace SettingsAPIClient.Provider
{
    internal class ApplicationProvider : ApiClient<SettingsApplication>
    {
        private string applicationName;

        public ApplicationProvider(string url, string apiKey, string applicationName) : base(url, apiKey)
        {
            this.applicationName = applicationName;
        }

        public override string LoalPath { get { return string.Concat("application", "/", applicationName); } }
    }
}