using SettingsAPIRepository.Data;

namespace SettingsAPIRepository
{
    public interface ISettingsDatabase
    {
        SettingsDbContext Context { get; }

        void Save();

        void Dispose();

        VersionData GetVersion(string applicationName, int version);

        DirectoryData GetDirectory(string applicationName, string directoryName);
    }
}