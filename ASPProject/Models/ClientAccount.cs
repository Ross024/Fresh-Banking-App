using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASPProject.Models
{
    public class ClientAccount
    {
        [Key, Column(Order = 0)]
        public int clientID { get; set; }
        [Key, Column(Order = 1)]
        public int accountNum { get; set; }

        // Navigation Properties
        // Parents
        public virtual Client Client { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
}
