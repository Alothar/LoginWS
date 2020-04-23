using LoginWS.DB;
using System;
using System.Text;

namespace LoginWS
{
    public class LoginService : ILoginService
    {
        DBAcessor DB = DBAcessor.Instance;

        enum CSVCurrencyList
        { 
            USD,
            EUR
        }

        public AccountType CreateAccount(string AccountName, string AccountLogin, string AccountPassword)
        {
            AccountType account = new AccountType();
            if (AccountName == null)
            {
                throw new ArgumentNullException("AccountName");
            }
            else if (AccountLogin == null)
            {
                throw new ArgumentNullException("AccountLogin");
            }
            else if (AccountPassword == null)
            {
                throw new ArgumentNullException("AccountPassword");
            }
            else 
            {
                account.AccountName = AccountName;
                account.AccountLogin = AccountLogin;
                account.AccountPassword = AccountPassword;
                StringBuilder CSVStringBuilder = new StringBuilder();
                foreach (string s in Enum.GetNames(typeof(CSVCurrencyList)))
                    CSVStringBuilder.Append(s + ",");                
                account.AccountCSVCurrencyList = CSVStringBuilder.Remove(CSVStringBuilder.Length - 1, 1).ToString();
                account.AccountID = DB.InsertAccount(account);
                if (account.AccountID == -1)
                    throw new Exception("Account already exist");
            }
            return account;
        }

        public AccountType GetAccount(string AccountLogin, string AccountPassword)
        {
            AccountType account = new AccountType();
            if (AccountLogin == null)
            {
                throw new ArgumentNullException("AccountLogin");
            }
            else if (AccountPassword == null)
            {
                throw new ArgumentNullException("AccountPassword");
            }
            else
            {
                account = DB.GetAccount(AccountLogin, AccountPassword);
                if (account.AccountLogin == null)
                    throw new Exception("Account does not exist");
            }
            return account;
        }

        public AccountType UpdateAccount(AccountType Account)
        {
            AccountType account = new AccountType();
            if (Account.AccountName == null)
            {
                throw new ArgumentNullException("AccountName");
            }
            else if (Account.AccountLogin == null)
            {
                throw new ArgumentNullException("AccountLogin");
            }
            else if (Account.AccountPassword == null)
            {
                throw new ArgumentNullException("AccountPassword");
            }
            else
            {
                DB.UpdateAccount(Account);
                account = DB.GetAccount(Account);
                if (account.AccountLogin == null)
                    throw new Exception("Account does not exist");
            }
            return account;
        }
    }
}
