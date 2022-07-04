using ASPProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ASPProject.Data.ApplicationDbContext;

namespace ASPProject.Repositories
{
    public class ClientRepo
    {
        ApplicationDbContext db;

        public ClientRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public bool Update(int clientID, string firstName, string lastName)
        {
            Client client = db.Clients
                .Where(c => c.clientID == clientID).FirstOrDefault();

            client.firstName = firstName;
            client.lastName = lastName;
            db.SaveChanges();
            return true;
        }

        public int GetClientId(string email)
        {
            int clientID = db.Clients
                .Where(c => c.email == email)
                .Select(a => a.clientID).FirstOrDefault();

            return clientID;
        }
        public string GetFirstName(string email)
        {
            string firstName = db.Clients
                .Where(c => c.email == email)
                .Select(f => f.firstName).FirstOrDefault();

            return firstName;
        }
        public int Add(Client client)
        {
            db.Clients.Add(client);
            db.SaveChanges();
            
            return client.clientID;
        }
    }
}
