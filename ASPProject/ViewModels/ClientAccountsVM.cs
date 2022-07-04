using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPProject.ViewModels
{
    public class ClientAccountsVM
    {
        [DisplayName("Account Number")]
        public int accountNum { get; set; }
        [DisplayName("Last Name")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only alphabetical characters allowed.")]
        public string lastName { get; set; }
        [DisplayName("First Name")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only alphabetical characters allowed.")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Must select an account type.")]
        [DisplayName("Account Type")]
        public string accountType { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("Client ID")]
        public int clientID { get; set; }
        [DisplayName("Balance")] 
        [RegularExpression("^[0-9]+.?[0-9]{0,2}$", ErrorMessage = "Must be a positive number, up to 2 decimal places.")]
        public decimal balance { get; set; }
    }
}
