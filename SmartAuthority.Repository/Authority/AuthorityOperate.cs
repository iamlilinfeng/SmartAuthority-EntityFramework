/*技术支持QQ群：226090960*/
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SmartAuthority.Interface.Authority;
using System;

namespace SmartAuthority.Repository.Authority
{
    public class AuthorityOperate : IAuthorityOperate
    {
        public List<DTO.Authority.AuthorityOperate> GetList(string Name, DTO.PagerParm PagerParm, out int TotalCount)
        {
            var result = new List<DTO.Authority.AuthorityOperate>();
            using (var repository = new AuthorityContext())
            {
                Func<DTO.Authority.AuthorityOperate, bool> where = o => o.Enable;
                if (!string.IsNullOrEmpty(Name))
                {
                    where += o => o.Name.Contains(Name);
                }
                TotalCount = repository.AuthorityOperate.Count();
                result = repository.AuthorityOperate.Where(where).OrderByDescending(o => o.AuthorityOperateId).Skip(PagerParm.CurrentCount).Take(PagerParm.PageSize).ToList();
            }
            return result;
        }

        public List<DTO.Authority.AuthorityOperate> GetList()
        {
            var result = new List<DTO.Authority.AuthorityOperate>();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityOperate.ToList();
            }
            return result;
        }

        public bool SaveRoleOperate(int RoleId, List<int> OperateIds)
        {
            var result = false;
            using (var repository = new AuthorityContext())
            {
                List<DTO.Authority.AuthorityRoleOperate> roleOperate = new List<DTO.Authority.AuthorityRoleOperate>();
                foreach (int OperateId in OperateIds)
                {
                    roleOperate.Add(new DTO.Authority.AuthorityRoleOperate() { AuthorityRoleId = RoleId, AuthorityOperateId = OperateId });
                }
                repository.AuthorityRoleOperate.RemoveRange(repository.AuthorityRoleOperate.Where(o => o.AuthorityRoleId == RoleId));
                repository.AuthorityRoleOperate.AddRange(roleOperate);
                result = repository.SaveChanges() > 0;
            }
            return result;
        }

        public List<DTO.Authority.AuthorityOperate> GetList(int RoleId)
        {
            var result = new List<DTO.Authority.AuthorityOperate>();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityOperate.Join(repository.AuthorityRoleOperate.Where(o => o.AuthorityRoleId == RoleId), a => a.AuthorityOperateId, b => b.AuthorityOperateId, (a, b) => a).Distinct().ToList();
            }
            return result;
        }

        public DTO.Authority.AuthorityOperate Get(int OperateId)
        {
            var result = new DTO.Authority.AuthorityOperate();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityOperate.SingleOrDefault(o => o.AuthorityOperateId == OperateId);
            }
            return result;
        }

        public bool Save(DTO.Authority.AuthorityOperate Operate)
        {
            var result = false;
            using (var repository = new AuthorityContext())
            {
                var account = repository.AuthorityOperate.Single(o => o.AuthorityOperateId == Operate.AuthorityOperateId);
                account.Name = Operate.Name;
                account.Order = Operate.Order;
                result = repository.SaveChanges() > 0;
            }
            return result;
        }

        public List<DTO.Authority.AuthorityOperate> GetOperateByAccountId(int AccountId)
        {
            var result = new List<DTO.Authority.AuthorityOperate>();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityOperate.Join(repository.AuthorityRoleOperate.Join(repository.AuthorityAccountRole.Where(o => o.AuthorityAccountId == AccountId), a => a.AuthorityRoleId, b => b.AuthorityRoleId, (a, b) => a), a => a.AuthorityOperateId, b => b.AuthorityOperateId, (a, b) => a).Distinct().ToList();
            }
            return result;
        }
    }
}
