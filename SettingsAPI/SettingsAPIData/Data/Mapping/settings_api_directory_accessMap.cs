using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SettingsAPIData.Models.Mapping
{
    public class settings_api_directory_accessMap : EntityTypeConfiguration<DirectoryAccessData>
    {
        public settings_api_directory_accessMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ApiKeyId, t.DirectoryId });

            // Properties
            this.Property(t => t.ApiKeyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DirectoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("settings_api_directory_access");
            this.Property(t => t.ApiKeyId).HasColumnName("apikey_id");
            this.Property(t => t.DirectoryId).HasColumnName("directory_id");
            this.Property(t => t.AllowWrite).HasColumnName("allow_write");
            this.Property(t => t.AllowDelete).HasColumnName("allow_delete");
            this.Property(t => t.AllowCreate).HasColumnName("allow_create");

            // Relationships
            this.HasRequired(t => t.Directory)
                .WithMany(t => t.Access)
                .HasForeignKey(d => d.DirectoryId);
            this.HasRequired(t => t.ApiKey)
                .WithMany(t => t.Access)
                .HasForeignKey(d => d.ApiKeyId);

        }
    }
}
