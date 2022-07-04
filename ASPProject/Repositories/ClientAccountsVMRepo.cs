using ASPProject.Data;
using ASPProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ASPProject.Data.ApplicationDbContext;

namespace ASPProject.Repositories
{
    public class ClientAccountsVMRepo
    {
        ApplicationDbContext db;

        public ClientAccountsVMRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public IQueryable<ClientAccountsVM> GetAll(string email, int clientID)
        {
            var queryA = from c in db.Clients
                         from ca in db.ClientAccounts
                         where email == c.email && clientID == ca.clientID
                         select new ClientAccountsVM()
                         {
                             accountNum = ca.accountNum,
                             lastName = c.lastName,
                             firstName = c.firstName,
                             clientID = c.clientID,
                             email = c.email
                         };

            var queryB = from ba in db.BankAccounts
                         from q in queryA
                         where ba.accountNum == q.accountNum
                         orderby ba.accountNum descending
                         select new ClientAccountsVM()
                         {
                             accountNum = ba.accountNum,
                             accountType = ba.accountType,
                             balance = ba.balance,
                             clientID = q.clientID,
                             lastName = q.lastName,
                             firstName = q.firstName,
                             email = q.email
                         };
                         
            return (queryB);
        }

        public ClientAccountsVM Get(int clientID, int accountNum)
        {

            var query = db.ClientAccounts
                        .Where(ca => ca.clientID == clientID && ca.accountNum == accountNum)
                        .Select(ca => new ClientAccountsVM()
                        {
                            clientID = ca.clientID,
                            accountNum = ca.accountNum,
                            firstName = ca.Client.firstName,
                            lastName = ca.Client.lastName,
                            email = ca.Client.email,
                            accountType = ca.BankAccount.accountType,
                            balance = ca.BankAccount.balance
                        })
                        .FirstOrDefault();

            return(query);
        }
        public bool Update(ClientAccountsVM caVM)
        {
            // Update the Client table
            ClientRepo clientRepo = new ClientRepo(db);
            clientRepo.Update(caVM.clientID, caVM.firstName, caVM.lastName);

            // Update the BankAccount table
            BankAccountRepo baRepo = new BankAccountRepo(db);
            baRepo.Update(caVM.accountNum, caVM.balance);

            return true;
        }
        public bool Add(ClientAccountsVM caVM)
        {

            ClientRepo clientRepo = new ClientRepo(db);
            int clientId = clientRepo.GetClientId(caVM.email);

            BankAccountRepo accountRepo = new BankAccountRepo(db);
            BankAccount bankAccount = new BankAccount { accountType = caVM.accountType, balance = caVM.balance };

            int accountNum = accountRepo.Add(bankAccount);

            ClientAccount clientAccount = new ClientAccount()
            {
               clientID = clientId,
               accountNum = accountNum
            };

            // Need to add to bridge table
            db.ClientAccounts.Add(clientAccount);
            db.SaveChanges();

            return true;
        }
    }
}
