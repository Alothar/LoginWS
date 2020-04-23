using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace LoginWS.DB
{
    public class DBAcessor
    {
        private readonly SqlConnectionStringBuilder builder;

        private DBAcessor()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost\\SQLEXPRESS";
            builder.InitialCatalog = "EFAccountDB";
            builder.IntegratedSecurity = true;
        }

        public int InsertAccount(AccountType obj)
        {
            Account account = convertToAccount(obj);
            using (EFAccountDB ConnectionContext = new EFAccountDB(builder.ConnectionString))
            {
                try
                {
                    ConnectionContext.Accounts.Add(account);
                    ConnectionContext.SaveChanges();
                }
                catch (SqlException e)
                {
                    if (e.Message.Contains("duplicate"))
                        return -1;
                }
            }
            Console.WriteLine("\nAdded Account: " + account.ToString());
            return GetAccount(account.AccountLogin, account.AccountPassword).AccountID;
        }

        public void UpdateAccount(AccountType obj)
        {
            Account account = convertToAccount(obj);
            Account account_from_db;
            using (EFAccountDB ConnectionContext = new EFAccountDB(builder.ConnectionString))
            {
                account_from_db = ConnectionContext.Accounts.Find(account.Id);
                if (account_from_db != null)
                {
                    ConnectionContext.Entry(account_from_db).CurrentValues.SetValues(account);
                    ConnectionContext.SaveChanges();
                }
            }
        }

        public AccountType GetAccount(AccountType obj)
        {
            AccountType account;
            using (EFAccountDB ConnectionContext = new EFAccountDB(builder.ConnectionString))
            {
                account = convertToAccountType(ConnectionContext.Accounts.Find(convertToAccount(obj).Id));
            }
            return account;
        }

        public AccountType GetAccount(string AccountLogin, string AccountPassword)
        {
            Account account;
            using (EFAccountDB ConnectionContext = new EFAccountDB(builder.ConnectionString))
            {
                account = ConnectionContext.Accounts.Where(a => a.AccountLogin == AccountLogin).Where(a => a.AccountPassword == AccountPassword).FirstOrDefault();
            }
            AccountType accountType = convertToAccountType(account);
            return accountType;
        }

        private Account convertToAccount(AccountType accountType)
        {
            Account account = new Account();
            if (account != null)
            {
                account.AccountName = accountType.AccountName;
                account.AccountLogin = accountType.AccountLogin;
                account.AccountPassword = accountType.AccountPassword;
                account.Id = accountType.AccountID;
                account.AccountCSVCurrencyList = accountType.AccountCSVCurrencyList;
            }
            return account;
        }

        private AccountType convertToAccountType(Account account)
        {
            AccountType accountType = new AccountType();
            if (account != null)
            {
                accountType.AccountName = account.AccountName;
                accountType.AccountLogin = account.AccountLogin;
                accountType.AccountPassword = account.AccountPassword;
                accountType.AccountID = account.Id;
                accountType.AccountCSVCurrencyList = account.AccountCSVCurrencyList;
            }
            return accountType;
        }

        public static DBAcessor Instance { get { return NestedDBAcessor.instance; } }
        private class NestedDBAcessor
        {
            static NestedDBAcessor()
            {
            }

            internal static readonly DBAcessor instance = new DBAcessor();
        }
    }
}