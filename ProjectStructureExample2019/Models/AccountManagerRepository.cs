using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessModel;
using BusinessModel.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace ProjectStructureExample2019.Models
{
    public class AccountManagerRepository : IUser, IAccount
    {
        BusinessContext bctx = new BusinessContext();
        ApplicationDbContext actx = new ApplicationDbContext();

        public void CreateAccount(Account account, string uid)
        {
            // to do
        }

        public AccountUserViewModel GetAccountsByUser(string uid)
        {
            ApplicationUser AccountManager = getUserByID(uid);
            if (AccountManager != null)
            {
                var ManagerAccounts = bctx.Accounts.Where(a => a.AccountManagerID == uid).ToList();
                return new AccountUserViewModel
                {
                    AccountManagerID = AccountManager.Id,
                    AccountManagerName = AccountManager.FirstName + " " + AccountManager.SecondName,
                    accounts = ManagerAccounts
                };
            }
            return null;
        }

        public ApplicationUser getUserByID(string id)
        {
            return actx.Users.Find(id);
        }

        public ApplicationUser getUserByName(string name)
        {
            return actx.Users.FirstOrDefault(u => u.UserName == name);
        }

        public void UpdateAccount(Account account, string uid)
        {
            throw new NotImplementedException();
        }
    }
}