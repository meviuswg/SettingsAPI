using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace SettingsAPIData.Models.Mapping
{
    public class settings_api_keyMap : EntityTypeConfiguration<ApiKeyData>
    {
        public settings_api_keyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ApiKey)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("settings_api_key");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ApplicationId).HasColumnName("application_id");
            this.Property(t => t.ApiKey).HasColumnName("apikey");
            this.Property(t => t.LastUsed).HasColumnName("last_used");
            this.Property(t => t.EditDirectories).HasColumnName("edit_directories");
            this.Property(t => t.Active).HasColumnName("active");
            this.Property(t => t.Created).HasColumnName("created");

            // Relationships
            this.HasRequired(t => t.Application)
                .WithMany(t => t.ApiKeys)
                .HasForeignKey(d => d.ApplicationId);

        }
    }
}
