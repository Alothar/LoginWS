using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LoginWS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILoginService
    {

        [OperationContract]
        AccountType CreateAccount(string AccountName, string AccountLogin, string AccountPassword);

        [OperationContract]
        AccountType GetAccount(string AccountLogin, string AccountPassword);

        [OperationContract]
        AccountType UpdateAccount(AccountType Account);

    }

    [DataContract]
    public class AccountType
    {
        [DataMember]
        public int AccountID { get; set; }
        [DataMember]
        public string AccountName { get; set; }
        [DataMember]
        public string AccountLogin { get; set; }
        [DataMember]
        public string AccountPassword { get; set; }
        [DataMember]
        public string AccountCSVCurrencyList { get; set; }
    }
}
