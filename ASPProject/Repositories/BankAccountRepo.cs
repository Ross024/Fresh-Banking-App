using ASPProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ASPProject.Data.ApplicationDbContext;

namespace ASPProject.Repositories
{
    public class BankAccountRepo
    {
        ApplicationDbContext db;

        public BankAccountRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public bool Update(int accountNum, decimal balance)
        {
            BankAccount bankAccount = db.BankAccounts
                .Where(b => b.accountNum == accountNum).FirstOrDefault();

            bankAccount.balance = balance;
            db.SaveChanges();
            return true;
        }
        public int Add(BankAccount bankAccount)
        {
            db.BankAccounts.Add(bankAccount);
            db.SaveChanges();

            return(bankAccount.accountNum);     
        }
    }
}
