using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace SettingsAPIData.Data.Mapping
{
    internal class settings_versionMap : EntityTypeConfiguration<VersionData>
    {
        public settings_versionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("settings_version");
            this.Property(t => t.Id).HasColumnName("version_id");
            this.Property(t => t.ApplicationId).HasColumnName("application_id");
            this.Property(t => t.Version).HasColumnName("version");
            this.Property(t => t.Created).HasColumnName("created");

            // Relationships
            this.HasRequired(t => t.Application)
                .WithMany(t => t.Versions)
                .HasForeignKey(d => d.ApplicationId);
                

        }
    }
}
