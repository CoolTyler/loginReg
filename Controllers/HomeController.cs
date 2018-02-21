﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using loginRegistration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace loginRegistration.Controllers
{
    public static class SessionExtensions
    {
    
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
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
                    HttpContext.Session.SetObjectAsJson("currentUser", currentUser);
                    return RedirectToAction("Success");
                }else{
                    ViewBag.errors = ModelState.Values;
                    return View("Index");
                }
            

        }

        public IActionResult Login(LoginViewModel model)
        {
            var currentUser = _context.Users.SingleOrDefault(user => user.email == model.logEmail);
            if(currentUser != null && currentUser.password == model.logPassword)
            {
                HttpContext.Session.SetObjectAsJson("currentUser", currentUser);
                RedirectToAction("Success");
            }else{
            ViewBag.Error = "Incorrect email/ password combination";
            return View("Index");
            }
            
            return RedirectToAction("Success");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        

        public IActionResult Success()
        {
            User currentUser = HttpContext.Session.GetObjectFromJson<User>("currentUser");
            ViewBag.firstName = currentUser.firstName;
            return View("Success");
        }
    }
}
