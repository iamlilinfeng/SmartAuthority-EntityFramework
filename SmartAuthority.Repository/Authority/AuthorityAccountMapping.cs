/*技术支持QQ群：226090960*/
using System.Data.Entity.ModelConfiguration;

namespace SmartAuthority.Repository.Authority
{
    public class AuthorityAccountMapping : EntityTypeConfiguration<DTO.Authority.AuthorityAccount>
    {
        public AuthorityAccountMapping()
        {
            this.ToTable("authority_account", "dbo");
            this.HasKey(a => a.AuthorityAccountId);
            this.Property(a => a.AuthorityAccountId).HasColumnName("authority_account_id");
            this.Property(a => a.Name).HasColumnName("name");
            this.Property(a => a.RealName).HasColumnName("real_name");
            this.Property(a => a.CreateTime).HasColumnName("create_time");
            this.Property(a => a.Enable).HasColumnName("enable");
        }
    }
}
