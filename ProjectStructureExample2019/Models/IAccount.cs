using BusinessModel;
using BusinessModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStructureExample2019.Models
{
    public interface IAccount
    {
        AccountUserViewModel GetAccountsByUser(string uid);
        void UpdateAccount(Account account, string uid);
        void CreateAccount(Account account, string uid);
    }
}