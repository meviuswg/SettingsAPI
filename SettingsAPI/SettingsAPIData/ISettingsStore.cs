using SettingsAPIData.Data;

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