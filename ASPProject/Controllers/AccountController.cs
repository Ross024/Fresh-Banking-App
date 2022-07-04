using ASPProject.Data;
using ASPProject.Models;
using ASPProject.Repositories;
using ASPProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPProject.Controllers
{
    public class AccountController : Controller
    {


        private readonly ApplicationDbContext _context;

        public AccountController(ILogger<AccountController> logger, ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            string email = User.Identity.Name;

            
            ClientRepo cRepo = new ClientRepo(_context);

            var userFirstName = cRepo.GetFirstName(email);
            var clientID = cRepo.GetClientId(email);
            HttpContext.Session.SetString("userName", userFirstName);
            
            //ApplicationDbContext context = new ApplicationDbContext();
            ClientAccountsVMRepo caRepo = new ClientAccountsVMRepo(_context);
            var query = caRepo.GetAll(email, clientID);

            return View(query);
        }
        public ActionResult Details(int clientID, int accountNum)
        {
            string email = User.Identity.Name;
            ClientAccountsVMRepo caRepo = new ClientAccountsVMRepo(_context);
            ClientAccountsVM caVM = caRepo.Get(clientID, accountNum);
            return View(caVM);
        }
        [HttpGet]
        public ActionResult Edit(int clientID, int accountNum)
        {
            ClientAccountsVMRepo caRepo = new ClientAccountsVMRepo(_context);
           
            var caVM = caRepo.Get(clientID, accountNum);
            return View(caVM);
        }

        // This method is called when the user clicks the submit
        // button from the edit page.
        [HttpPost]
        public ActionResult Edit(ClientAccountsVM caVM)
        {
            ClientAccountsVMRepo caRepo = new ClientAccountsVMRepo(_context);
            caRepo.Update(caVM);

            // go to index action method of the EmployeeStore controller.
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ClientAccountsVM caVM)
        {
            ClientAccountsVMRepo caRepo = new ClientAccountsVMRepo(_context);

            if(ModelState.IsValid)
            {
                caVM.email = User.Identity.Name;
                caRepo.Add(caVM);
                return RedirectToAction("Index", "Account");
            } 
            else
            {
                return View(caVM);
            }
        }

    }
}
