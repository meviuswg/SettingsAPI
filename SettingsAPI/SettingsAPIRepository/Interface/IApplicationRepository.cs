using SettingsAPIRepository.Model;
using System.Collections.Generic;

namespace SettingsAPIRepository
{
    public interface IApplicationRepository
    {
        ApplicationModel CreateApplication(string applicationName);

        ApplicationModel CreateApplication(string applicationName, string applicationDescription);

        ApplicationModel CreateApplication(string applicationName, string applicationDescription, string directoryName, string directoryDescription);

        void UpdateApplication(string applicationName, string newApplicationName, string newApplicationDescription);

        void CopyApplication(string applicationName, string newApplicationName, string newApplicationDescription);

        void CopyApplication(string applicationName, string newApplicationName, string newApplicationDescription, int version);

        IEnumerable<ApplicationModel> GetApplications();

        ApplicationModel GetApplication(string name);

        IEnumerable<DirectoryModel> GetDirectories(string applicationName);

        IEnumerable<DirectoryModel> GetDirectories(string applicationName, string directoryName);

        void CreateDirectory(string applicationName, string directoryName, string description);

        void CopyDirectory(string applicationName, string copyFrom,  string toName, int version);

        void DeleteDirectory(string applicationName, string directoryName);

        void UpdateDirectory(string applicationName, string directoryName, string newDirectoryName, string newDescription);

        VersionModel GetVersion(string applicationName, int version);

        IEnumerable<VersionModel> GetVersions(string applicationName);

        void CreateVersion(string applicationName, int version);

        void CopyVersion(string applicationName, int newVersion, int compyVersion);

        void DeleteVersion(string applicationName, int version);

        void DeleteApplication(string name);
    }
}