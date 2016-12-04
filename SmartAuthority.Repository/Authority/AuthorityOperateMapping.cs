/*技术支持QQ群：226090960*/
using System.Data.Entity.ModelConfiguration;

namespace SmartAuthority.Repository.Authority
{
    public class AuthorityOperateMapping : EntityTypeConfiguration<DTO.Authority.AuthorityOperate>
    {
        public AuthorityOperateMapping()
        {
            this.ToTable("authority_operate", "dbo");
            this.HasKey(r => r.AuthorityOperateId);
            this.Property(r => r.AuthorityOperateId).HasColumnName("authority_operate_id");
            this.Property(r => r.Name).HasColumnName("name");
            this.Property(r => r.Control).HasColumnName("control");
            this.Property(r => r.Action).HasColumnName("action");
            this.Property(r => r.Icon).HasColumnName("icon");
            this.Property(r => r.Visible).HasColumnName("visible");
            this.Property(r => r.Order).HasColumnName("order");
            this.Property(r => r.ParentId).HasColumnName("parent_id");
            this.Property(r => r.CreateTime).HasColumnName("create_time");
            this.Property(r => r.Enable).HasColumnName("enable");
            this.Property(r => r.Level).HasColumnName("level");
        }
    }
}
