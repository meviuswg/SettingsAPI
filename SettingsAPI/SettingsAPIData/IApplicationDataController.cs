using System.Collections.Generic;
using SettingsAPIData.Model;

namespace SettingsAPIData
{
    public interface IApplicationDataController
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
        IEnumerable<VersionModel> GetVersions(string applicationName); 
        void CreateVersion(string applicationName, int version);
        void DeleteVersion(string applicationName, int version);
        bool AllowRead();
        bool AllowDelete();
        void DeleteApplication(string name);
    }
}