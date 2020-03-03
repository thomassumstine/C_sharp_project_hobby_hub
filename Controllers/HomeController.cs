using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HobbyHub.Models;

namespace HobbyHub.Controllers
{
    public class HomeController : Controller
    {
        private HobbyHubContext dbContext;


        public HomeController(HobbyHubContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                HttpContext.Session.Clear();
                return View();
            }
            return View();
        }

        [HttpPost("Home/reg")]
        public IActionResult reg(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Users.Any(user => user.Username == newUser.Username))
                {
                    ModelState.AddModelError("Username", "Please use different username to register");
                    return RedirectToAction("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Dashboard", "Hobby");
            }
            return View("Index");
        }
        public IActionResult login(LogModel user)
        {
            if (ModelState.IsValid)
            {
                var finduser = dbContext.Users.FirstOrDefault(u => u.Username == user.LUsername);
                if (finduser == null)
                {
                    ModelState.AddModelError("LUsername", "Invalid Username/Password");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LogModel>();
                var res = hasher.VerifyHashedPassword(user, finduser.Password, user.LPassword);
                if (res == 0)
                {
                    ModelState.AddModelError("LUsername", "Cannot log in for some reason");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", finduser.UserId);
                return RedirectToAction("Dashboard", "Hobby");
            }
            return View("Index");
        }

        [HttpGet("Home/logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}