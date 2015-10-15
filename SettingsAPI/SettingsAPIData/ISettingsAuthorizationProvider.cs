using System.Security.Principal;

namespace SettingsAPIData
{
    public interface ISettingsAuthorizationProvider
    {
        IApiKey CurrentApiKey { get; }
        ApiIdentity CurrentIdentity { get; }
        bool IsMasterKey { get; }

        bool AllowCreateApplication(string application);

        bool AllowCreateDirectories(string application);

        bool AllowCreateDirectory(string application, string directoryName);

        bool AllowCreateSetting(string application, string directoryName);

        bool AllowCreateVersion(string application);

        bool AllowDeleteApplication(string application);

        bool AllowDeleteDirectory(string application, string directoryName);

        bool AllowDeleteSetting(string application, string directoryName);

        bool AllowDeleteVersion(string application);

        bool AllowReadVersions(string application);

        bool AllowReadDirectories(string application);

        bool AllowReadDirectory(string application, string directoryName);

        bool AllowriteSetting(string application, string directoryName);
         
        bool AllowReadApiKeys(string applicationName); 

        bool AllowEditApiKeys(string applicationName);

        void Invalidate();

        bool Validate(object apiKey, out IPrincipal principal);

        bool Validate(object apiKey);
    }
}