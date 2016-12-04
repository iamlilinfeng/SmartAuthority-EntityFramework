/*技术支持QQ群：226090960*/
using System.Data.Entity;

namespace SmartAuthority.Repository.Authority
{
    public class AuthorityContext : DbContext
    {
        public AuthorityContext() : base("Conn")
        {
            // the terrible hack
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<DTO.Authority.AuthorityAccount> AuthorityAccount { get; set; }
        public DbSet<DTO.Authority.AuthorityRole> AuthorityRole { get; set; }
        public DbSet<DTO.Authority.AuthorityOperate> AuthorityOperate { get; set; }
        public DbSet<DTO.Authority.AuthorityAccountRole> AuthorityAccountRole { get; set; }
        public DbSet<DTO.Authority.AuthorityRoleOperate> AuthorityRoleOperate { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuthorityAccountMapping());
            modelBuilder.Configurations.Add(new AuthorityRoleMapping());
            modelBuilder.Configurations.Add(new AuthorityOperateMapping());
            modelBuilder.Configurations.Add(new AuthorityAccountRoleMapping());
            modelBuilder.Configurations.Add(new AuthorityRoleOperateMapping());
        }
    }
}
