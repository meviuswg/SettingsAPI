using System.Security.Principal;

namespace SettingsAPIData
{
    public interface ISettingsAuthorizationProvider
    {
        ApiIdentity CurrentIdentity { get; }
        IApiKey CurrentApiKey { get; }

        bool AllowCreateApplication(string application);

        bool AllowCreateDirectory(string application, string directoryName);

        bool AllowCreateDirectories(string application);

        bool AllowCreateSetting(string application, string directoryName);

        bool AllowCreateVersion(string application);

        bool AllowDeleteApplication(string application);

        bool AllowDeleteDirectory(string application, string directoryName);

        bool AllowDeleteSetting(string application, string directoryName);

        bool AllowDeleteVersion(string application);

        bool AllowWriteSetting(string application, string directoryName);

        bool Validate(object apiKey, out IPrincipal principal);

        bool Validate(object apiKey);

        bool IsMasterKey { get; }

        void Invalidate();
    }
}