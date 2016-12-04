/*技术支持QQ群：226090960*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SmartAuthority.Interface.Authority;

namespace SmartAuthority.Repository.Authority
{
    public class AuthorityRole : IAuthorityRole
    {
        public List<DTO.Authority.AuthorityRole> GetList(string Name, DTO.PagerParm PagerParm, out int TotalCount)
        {
            var result = new List<DTO.Authority.AuthorityRole>();
            using (var repository = new AuthorityContext())
            {
                Func<DTO.Authority.AuthorityRole, bool> where = o => o.Enable;
                if (!string.IsNullOrEmpty(Name))
                {
                    where += a => a.Name.Contains(Name);
                }
                TotalCount = repository.AuthorityRole.Count();
                result = repository.AuthorityRole.Where(where).OrderByDescending(o => o.AuthorityRoleId).Skip(PagerParm.CurrentCount).Take(PagerParm.PageSize).ToList();
            }
            return result;
        }

        public DTO.Authority.AuthorityRole Get(int RoleId)
        {
            var result = new DTO.Authority.AuthorityRole();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityRole.SingleOrDefault(o => o.AuthorityRoleId == RoleId);
            }
            return result;
        }

        public List<DTO.Authority.AuthorityRole> GetByAccountId(int AccountId)
        {
            var result = new List<DTO.Authority.AuthorityRole>();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityRole.Join(repository.AuthorityAccountRole.Where(o => o.AuthorityAccountId == AccountId), a => a.AuthorityRoleId, b => b.AuthorityRoleId, (a, b) => a).Distinct().ToList();
            }
            return result;
        }

        public bool Add(DTO.Authority.AuthorityRole Role, out int RoleId)
        {
            var result = false;
            using (var repository = new AuthorityContext())
            {
                Role.Enable = true;
                Role.CreateTime = DateTime.Now;
                repository.AuthorityRole.Add(Role);
                result = repository.SaveChanges() > 0;
                RoleId = Role.AuthorityRoleId;
            }
            return result;
        }

        public bool Save(DTO.Authority.AuthorityRole Role)
        {
            var result = false;
            using (var repository = new AuthorityContext())
            {
                var role = repository.AuthorityRole.Single(o => o.AuthorityRoleId == Role.AuthorityRoleId);
                role.Name = Role.Name;
                role.Describe = Role.Describe;
                result = repository.SaveChanges() > 0;
            }
            return result;
        }

        public void Remove(int RoleId)
        {
            using (var repository = new AuthorityContext())
            {
                var role = repository.AuthorityRole.Single(o => o.AuthorityRoleId == RoleId);
                repository.AuthorityRole.Remove(role);
                repository.SaveChanges();
            }
        }
    }
}
