/*技术支持QQ群：226090960*/
using System.Data.Entity.ModelConfiguration;

namespace SmartAuthority.Repository.Authority
{
    public class AuthorityRoleMapping : EntityTypeConfiguration<DTO.Authority.AuthorityRole>
    {
        public AuthorityRoleMapping()
        {
            this.ToTable("authority_role", "dbo");
            this.HasKey(r => r.AuthorityRoleId);
            this.Property(r => r.AuthorityRoleId).HasColumnName("authority_role_id");
            this.Property(r => r.Name).HasColumnName("name");
            this.Property(r => r.Describe).HasColumnName("describe");
            this.Property(r => r.CreateTime).HasColumnName("create_time");
            this.Property(r => r.Enable).HasColumnName("enable");
        }
    }
}
