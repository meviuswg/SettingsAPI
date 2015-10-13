using SettingsAPIData.Model;
using System.Collections.Generic;

namespace SettingsAPIData
{
    public interface IApplicationRepository
    {
        ApplicationModel CreateApplication(string applicationName);

        ApplicationModel CreateApplication(string applicationName, string applicationDescription);

        ApplicationModel CreateApplication(string applicationName, string applicationDescription, string directoryName, string directoryDescription);

        IEnumerable<ApplicationModel> GetApplications();

        ApplicationModel GetApplication(string name);

        IEnumerable<DirectoryModel> GetDirectories(string applicationName);

        DirectoryModel GetDirectory(string applicationName, string directoryName);

        void CreateDirectory(string applicationName, string directoryName, string description);

        void DeleteDirectory(string applicationName, string directoryName);

        VersionModel GetVersion(string applicationName, int version);

        IEnumerable<VersionModel> GetVersions(string applicationName);

        void CreateVersion(string applicationName, int version);

        void DeleteVersion(string applicationName, int version);

        void DeleteApplication(string name);
    }
}