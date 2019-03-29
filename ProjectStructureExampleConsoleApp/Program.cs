using BusinessModel;
using BusinessModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIAuthenticationClient;

namespace ProjectStructureExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientAuthentication.baseWebAddress = "http://localhost:51132/";
            if (ClientAuthentication.login("bowles.lionie@itsligo.ie", "LBowles$1"))
            {
                Console.WriteLine("Successful login Token acquired:\n{0}\n\nUser status is {1}", ClientAuthentication.AuthToken, ClientAuthentication.AuthStatus.ToString());
                try
                {
                    AccountUserViewModel acuvm = ClientAuthentication.getItem<AccountUserViewModel>("api/AccountManager/getAccounts");
                    if (acuvm != null)
                    {
                        Console.WriteLine("Got {0} accounts for currently logged in user {1}\n", acuvm.accounts.Count(), acuvm.AccountManagerName);
                        // Get account list using current UserID
                        List<Account> accounts = ClientAuthentication.getList<Account>("api/AccountManager/getAccountsForCurrentManager/" + acuvm.AccountManagerID);
                        foreach (var item in accounts)
                        {
                            Console.WriteLine("Account Name {0}", item.AccountName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error {0} --> {1}", ex.Message, ex.InnerException.Message);
                }
                Console.ReadKey();
            }

            // Admin login.
            //if (ClientAuthentication.login("powell.paul@itsligo.ie", "Ppowell$1"))
            //{
            //    Console.WriteLine("Successful login Token acquired:\n{0}\n\nUser status is {1}", ClientAuthentication.AuthToken, ClientAuthentication.AuthStatus.ToString());
            //    //try
            //    //{
            //    //    AccountUserViewModel acuvm = ClientAuthentication.getItem<AccountUserViewModel>("api/AccountManager/getAccounts");
            //    //    if (acuvm != null)
            //    //    {
            //    //        Console.WriteLine("Got {0} accounts for currently logged in user {1}\n", acuvm.accounts.Count(), acuvm.AccountManagerName);
            //    //        // Get account list using current UserID
            //    //        List<Account> accounts = ClientAuthentication.getList<Account>("api/AccountManager/getAccountsForCurrentManager/" + acuvm.AccountManagerID);
            //    //        foreach (var item in accounts)
            //    //        {
            //    //            Console.WriteLine("Account Name {0}", item.AccountName);
            //    //        }
            //    //    }
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    Console.WriteLine("Error {0} --> {1}", ex.Message, ex.InnerException.Message);
            //    //}
            //    Console.ReadKey();
            //}
        }
    }
}