/*技术支持QQ群：226090960*/
using System.Data.Entity.ModelConfiguration;

namespace SmartAuthority.Repository.Authority
{
    public class AuthorityAccountRoleMapping : EntityTypeConfiguration<DTO.Authority.AuthorityAccountRole>
    {
        public AuthorityAccountRoleMapping()
        {
            this.ToTable("authority_account_role", "dbo");
            this.HasKey(a => a.AuthorityAccountRoleId);
            this.Property(a => a.AuthorityAccountRoleId).HasColumnName("authority_account_role_id");
            this.Property(a => a.AuthorityAccountId).HasColumnName("authority_account_id");
            this.Property(a => a.AuthorityRoleId).HasColumnName("authority_role_id");
        }
    }
}
