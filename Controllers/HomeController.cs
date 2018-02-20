using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using loginRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace loginRegistration.Controllers
{
    public class HomeController : Controller
    {
        private UserContext _context;
        public HomeController(UserContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<User> AllUsers = _context.Users.ToList();
            return View();
        }

        public IActionResult Register(RegisterViewModel model)
        {
            var currentUser = _context.Users.SingleOrDefault(user => user.email == model.email);
            if(ModelState.IsValid && currentUser == null)
                {
                    User newUser = new User
                    {
                        firstName = model.firstName,
                        lastName = model.lastName,
                        email = model.email,
                        password = model.password,
                        createdAt = DateTime.Now,
                        updatedAt = DateTime.Now,
                    };
                    _context.Add(newUser);
                    _context.SaveChanges();
                    return RedirectToAction("Success", newUser);
                }else{
                    ViewBag.errors = ModelState.Values;
                    return View("Index");
                }
            

        }

        public IActionResult Login(string email, string password)
        {
            var currentUser = _context.Users.SingleOrDefault(user => user.email == email);
            if(currentUser != null && currentUser.password == password)
            {
                RedirectToAction("Success", currentUser);
            }else{
            ViewBag.Error = "Incorrect email/ password combination";
            return View("Index");
            }
            return RedirectToAction("Success");
        }
        

        public IActionResult Success()
        {
            return View("Success");
        }
    }
}
