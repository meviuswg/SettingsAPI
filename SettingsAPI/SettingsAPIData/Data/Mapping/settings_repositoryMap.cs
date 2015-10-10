using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace SettingsAPIData.Models.Mapping
{
    public class settings_repositoryMap : EntityTypeConfiguration<RepositoryData>
    {
        public settings_repositoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("settings_repository");
            this.Property(t => t.Id).HasColumnName("repository_id");
            this.Property(t => t.ApplicationId).HasColumnName("application_id");
            this.Property(t => t.Version).HasColumnName("version");
            this.Property(t => t.Created).HasColumnName("created");

            // Relationships
            this.HasRequired(t => t.Application)
                .WithMany(t => t.Repositories)
                .HasForeignKey(d => d.ApplicationId);

        }
    }
}
