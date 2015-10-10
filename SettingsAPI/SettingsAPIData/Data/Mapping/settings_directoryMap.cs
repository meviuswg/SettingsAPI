using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace SettingsAPIData.Data.Mapping
{
    public class settings_directoryMap : EntityTypeConfiguration<Directorydata>
    {
        public settings_directoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(20);

            this.Property(t => t.Description)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("settings_directory");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ApplicationId).HasColumnName("application_id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Description).HasColumnName("description");

            // Relationships
            this.HasOptional(t => t.Application)
                .WithMany(t => t.Directories)
                .HasForeignKey(d => d.ApplicationId);

        }
    }
}
