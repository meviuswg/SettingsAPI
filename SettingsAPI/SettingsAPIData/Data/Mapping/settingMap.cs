using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SettingsAPIData.Data.Mapping
{
    public class settingMap : EntityTypeConfiguration<SettingData>
    {
        public settingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ObjecId, t.RepositoryId, t.DirectoryId, t.SettingKey });

            // Properties
            this.Property(t => t.ObjecId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RepositoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DirectoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SettingKey)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("settings");
            this.Property(t => t.ObjecId).HasColumnName("object_id");
            this.Property(t => t.RepositoryId).HasColumnName("repository_id");
            this.Property(t => t.DirectoryId).HasColumnName("directory_id");
            this.Property(t => t.SettingKey).HasColumnName("setting_key");
            this.Property(t => t.SettingValue).HasColumnName("setting_value");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Modified).HasColumnName("modified");

            // Relationships
            this.HasRequired(t => t.Directory)
                .WithMany(t => t.Settings)
                .HasForeignKey(d => d.DirectoryId);
            this.HasRequired(t => t.Repository)
                .WithMany(t => t.Settings)
                .HasForeignKey(d => d.RepositoryId);

 

        }
    }
}
