using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using SettingsAPIData.Models.Mapping;
using SettingsAPIData.Models;

namespace SettingsAPIData
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
        public DbSet<Directorydata> Directories { get; set; }
        public DbSet<RepositoryData> Repositories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new settingMap());
            modelBuilder.Configurations.Add(new settings_api_directory_accessMap());
            modelBuilder.Configurations.Add(new settings_api_keyMap());
            modelBuilder.Configurations.Add(new settings_applicationMap());
            modelBuilder.Configurations.Add(new settings_directoryMap());
            modelBuilder.Configurations.Add(new settings_repositoryMap());
        }

        public RepositoryData GetRepository(string applicationName, int version)
        {
            return this.Repositories.SingleOrDefault(a => a.Application.Name.ToLower() == applicationName.ToLower() && a.Version == version);
        }

        public Directorydata GetDirectory(string applicationName, string directoryName)
        {
            return this.Directories.SingleOrDefault(a => a.Application.Name.ToLower() == applicationName.ToLower() && a.Name.ToLower() == directoryName.ToLower());
        }
    }
}
