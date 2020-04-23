using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginWS.DB
{
    public class Account
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        [MaxLength(40)]
        public string AccountLogin { get; set; }
        [Required]
        [MaxLength(40)]
        public string AccountPassword { get; set; }
        [Required]
        public string AccountCSVCurrencyList { get; set; }

        private String GetDetails()
        {
            return this.AccountName + " " + this.AccountLogin + " " + this.AccountPassword;
        }
        public override string ToString()
        {
            return "Account [id=" + this.Id + ", details=" + this.GetDetails() + "]";
        }
    }
}