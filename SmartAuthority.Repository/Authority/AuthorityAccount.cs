/*技术支持QQ群：226090960*/
using System;
using System.Collections.Generic;
using System.Linq;
using SmartAuthority.Interface.Authority;

namespace SmartAuthority.Repository.Authority
{
    public class AuthorityAccount : IAuthorityAccount
    {
        public bool Add(DTO.Authority.AuthorityAccount authorityAccount)
        {
            var result = false;
            using (var repository = new AuthorityContext())
            {
                authorityAccount.Enable = true;
                authorityAccount.CreateTime = DateTime.Now;
                repository.AuthorityAccount.Add(authorityAccount);
                result = repository.SaveChanges() > 0;
            }
            return result;
        }

        public DTO.Authority.AuthorityAccount GetByName(string Name)
        {
            var result = new DTO.Authority.AuthorityAccount();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityAccount.SingleOrDefault(o => o.Name == Name);
            }
            return result;
        }

        public List<DTO.Authority.AuthorityAccount> GetList(string Name, DTO.PagerParm PagerParm, out int TotalCount)
        {
            var result = new List<DTO.Authority.AuthorityAccount>();
            using (var repository = new AuthorityContext())
            {
                Func<DTO.Authority.AuthorityAccount, bool> where = o => 1 == 1;
                if (!string.IsNullOrEmpty(Name))
                {
                    where += o => o.Name.Contains(Name);
                }

                TotalCount = repository.AuthorityAccount.Count();
                result = repository.AuthorityAccount.Where(where).OrderByDescending(o => o.AuthorityAccountId).Skip(PagerParm.CurrentCount).Take(PagerParm.PageSize).ToList();
            }
            return result;
        }

        public DTO.Authority.AuthorityAccount Get(int AccountId)
        {
            var result = new DTO.Authority.AuthorityAccount();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityAccount.SingleOrDefault(o => o.AuthorityAccountId == AccountId);
            }
            return result;
        }

        public bool AccountDisable(int AccountId)
        {
            var result = false;
            using (var repository = new AuthorityContext())
            {
                var account = repository.AuthorityAccount.Single(o => o.AuthorityAccountId == AccountId);
                account.Enable = false;
                result = repository.SaveChanges() > 0;
            }
            return result;
        }

        public bool AccountEnable(int AccountId)
        {
            var result = false;
            using (var repository = new AuthorityContext())
            {
                var account = repository.AuthorityAccount.Single(o => o.AuthorityAccountId == AccountId);
                account.Enable = true;
                result = repository.SaveChanges() > 0;
            }
            return result;
        }

        public bool ChangePassword(int AccountId, string NewPassword)
        {
            var result = false;
            using (var repository = new AuthorityContext())
            {
                var account = repository.AuthorityAccount.Single(o => o.AuthorityAccountId == AccountId);
                account.Password = NewPassword;
                result = repository.SaveChanges() > 0;
            }
            return result;
        }

        public List<DTO.Authority.AuthorityAccount> GetInRole(int RoleId)
        {
            var result = new List<DTO.Authority.AuthorityAccount>();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityAccount.Join(repository.AuthorityAccountRole.Where(o => o.AuthorityRoleId == RoleId), a => a.AuthorityAccountId, b => b.AuthorityAccountId, (a, b) => a).Distinct().ToList();
            }
            return result;
        }

        public List<DTO.Authority.AuthorityAccount> GetOutRole(int RoleId)
        {
            var result = new List<DTO.Authority.AuthorityAccount>();
            using (var repository = new AuthorityContext())
            {
                result = repository.AuthorityAccount.Join(repository.AuthorityAccountRole.Where(o => o.AuthorityRoleId != RoleId), a => a.AuthorityAccountId, b => b.AuthorityAccountId, (a, b) => a).Distinct().ToList();
            }
            return result;
        }

        public bool SaveRoleAccount(int RoleId, List<int> AccountIds)
        {
            var result = false;
            using (var repository = new AuthorityContext())
            {
                List<DTO.Authority.AuthorityAccountRole> accountRole = new List<DTO.Authority.AuthorityAccountRole>();
                foreach (int AccountId in AccountIds)
                {
                    accountRole.Add(new DTO.Authority.AuthorityAccountRole() { AuthorityRoleId = RoleId, AuthorityAccountId = AccountId });
                }
                repository.AuthorityAccountRole.RemoveRange(repository.AuthorityAccountRole.Where(o => o.AuthorityRoleId == RoleId));
                repository.AuthorityAccountRole.AddRange(accountRole);
                result = repository.SaveChanges() > 0;
            }
            return result;
        }
    }
}
