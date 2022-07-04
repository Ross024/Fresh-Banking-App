using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPProject.Models
{
    public class BankAccount
    {
        [Key]
        public int accountNum { get; set; }
        public string accountType { get; set; }
        public decimal balance { get; set; }

        public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
    }
}
