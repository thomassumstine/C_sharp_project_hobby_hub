using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HobbyHub.Models;

namespace HobbyHub.Controllers
{
    public class HobbyController : Controller
    {
        private HobbyHubContext dbContext;
        private int? _uid
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
        }

        public HobbyController(HobbyHubContext context)
        {
            dbContext = context;
        }

        [HttpGet("Hobby/Dashboard")]
        public IActionResult Dashboard()
        {
            var uid = _uid;
            if (_uid != null)
            {
                var allInfo = dbContext.Hobby
                    .Include(hobby => hobby.HobbyCreator)
                    .Include(enthusiast => enthusiast.Enthusiasts)
                    .ThenInclude(u => u.user).ToList();
                User user = dbContext.Users.FirstOrDefault(u => u.UserId == _uid);
                ViewBag.user = user;
                return View("Dashboard", allInfo);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Hobby/details/{id}")]
        public IActionResult Details(int id)
        {
            Hobby selectedHobby = dbContext.Hobby
                .Include(hobby => hobby.HobbyCreator)
                .Include(hobby => hobby.Enthusiasts)
                .ThenInclude(eachenthusiast => eachenthusiast.user)
                .FirstOrDefault(h => h.HobbyId == id);

            // in case user manually types URL
            if (selectedHobby == null)
                RedirectToAction("All");
            ViewBag.Uid = _uid;
            return View(selectedHobby);
        }

        [HttpGet("Hobby/create")]
        public IActionResult create()
        {

            if (_uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost("Hobby/AddNewHobby")]
        public IActionResult AddNewHobby(Hobby newHobby)
        {
            if (ModelState.IsValid)
            {
                User user = dbContext.Users.FirstOrDefault(u => u.UserId == _uid);
                newHobby.HobbyCreator = user;
                newHobby.HobbyCreatorId = user.UserId;
                dbContext.Add(newHobby);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("create");
        }

        [HttpGet("Hobby/delete/{HobbyId}")]
        public IActionResult delete(int hobbyId)
        {
            if (_uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Hobby hobby = dbContext.Hobby.FirstOrDefault(h => h.HobbyId == hobbyId);
            dbContext.Remove(hobby);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("Hobby/addedtopersonalhobbies/{hobbyId}")]
        public IActionResult addedtopersonalhobbies(int hobbyId)
        {
            if (_uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User user = dbContext.Users.FirstOrDefault(u => u.UserId == _uid);
            Hobby hobby = dbContext.Hobby.FirstOrDefault(h => h.HobbyId == hobbyId);
            AddedToPersonalHobbies addedtopersonalhobbies = new AddedToPersonalHobbies();
            addedtopersonalhobbies.HobbyId = hobby.HobbyId;
            addedtopersonalhobbies.hobby = hobby;
            addedtopersonalhobbies.UserId = user.UserId;
            addedtopersonalhobbies.user = user;
            dbContext.AddedToPersonalHobbies.Add(addedtopersonalhobbies);
            dbContext.SaveChanges();
            return RedirectToAction("Details", new {id = hobbyId});
        }

        [HttpGet("Hobby/removedfrompersonalhobbies/{hobbyId}")]
        public IActionResult removedfrompersonalhobbies(int hobbyId){
            if (_uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            AddedToPersonalHobbies addedtopersonalhobbies = dbContext.AddedToPersonalHobbies.FirstOrDefault(a => a.UserId==_uid && a.HobbyId == hobbyId);
            dbContext.AddedToPersonalHobbies.Remove(addedtopersonalhobbies);
            dbContext.SaveChanges();
            return RedirectToAction("Details");
        }

    }
}