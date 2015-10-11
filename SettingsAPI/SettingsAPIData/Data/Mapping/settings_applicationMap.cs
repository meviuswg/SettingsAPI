using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace SettingsAPIData.Data.Mapping
{
    internal class settings_applicationMap : EntityTypeConfiguration<ApplicationData>
    {
        public settings_applicationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("settings_application");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.Created).HasColumnName("created");
          
        }
    }
}
