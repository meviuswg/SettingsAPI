using SettingsAPIData.Data;
using System.Collections.Generic;
using System.Data.Entity;

namespace SettingsAPIData
{
    public interface ISettingsStore
    {
        SettingsDbContext Context { get; } 
        void Save();
        void Dispose();


        VersionData GetVersion(string applicationName, int version);
        DirectoryData GetDirectory(string applicationName, string directoryName);

    }
}