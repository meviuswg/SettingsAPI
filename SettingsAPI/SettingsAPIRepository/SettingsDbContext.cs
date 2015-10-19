using SettingsAPIRepository.Data;
using SettingsAPIRepository.Data.Mapping;
using System.Data.Entity;

namespace SettingsAPIRepository
{
    public partial class SettingsDbContext : DbContext
    {
        static SettingsDbContext()
        {
            Database.SetInitializer<SettingsDbContext>(null);
        }

        public SettingsDbContext()
            : base("Name=SettingsDb")
        {
        }

        public DbSet<SettingData> Settings { get; set; }
        public DbSet<DirectoryAccessData> Access { get; set; }
        public DbSet<ApiKeyData> ApiKeys { get; set; }
        public DbSet<ApplicationData> Applications { get; set; }
        public DbSet<DirectoryData> Directories { get; set; }
        public DbSet<VersionData> Versions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new settingMap());
            modelBuilder.Configurations.Add(new settings_directory_accessMap());
            modelBuilder.Configurations.Add(new settings_api_keyMap());
            modelBuilder.Configurations.Add(new settings_applicationMap());
            modelBuilder.Configurations.Add(new settings_directoryMap());
            modelBuilder.Configurations.Add(new settings_versionMap());
        }
    }
}