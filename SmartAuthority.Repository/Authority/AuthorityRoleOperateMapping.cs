/*技术支持QQ群：226090960*/
using System.Data.Entity.ModelConfiguration;

namespace SmartAuthority.Repository.Authority
{
    public class AuthorityRoleOperateMapping : EntityTypeConfiguration<DTO.Authority.AuthorityRoleOperate>
    {
        public AuthorityRoleOperateMapping()
        {
            this.ToTable("authority_role_operate", "dbo");
            this.HasKey(a => a.AuthorityRoleOperateId);
            this.Property(a => a.AuthorityRoleOperateId).HasColumnName("authority_role_operate_id");
            this.Property(a => a.AuthorityRoleId).HasColumnName("authority_role_id");
            this.Property(a => a.AuthorityOperateId).HasColumnName("authority_operate_id");
        }
    }
}
