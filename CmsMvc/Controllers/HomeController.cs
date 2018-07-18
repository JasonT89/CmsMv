using CmsMvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsMvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        CmsDb db = new CmsDb();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = db.People.Include("City")
                                  .ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new Person();
            var model2 = db.Countries.Include("Cities").ToList();
           
            var viewModel = new AddPersonViewModel(model, model2);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Person person, int CityId)
        {
            person.CityId = CityId;
            person.City = db.Cities.First(x => x.CityId == CityId);
            
            db.People.Add(person);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = db.People.Include("City").First(x => x.PersonId == id);
            var model2 = db.Countries.Include("Cities").ToList();

            var viewModel = new AddPersonViewModel(model, model2);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Person person, int CityId)
        {
            var model = db.People.Include("City")                                  
                                  .First(x => x.PersonId == person.PersonId);
            model.FirstName = person.FirstName;
            model.LastName = person.LastName;
            model.Email = person.Email;

            model.CityId = CityId;
            model.City = db.Cities.First(x => x.CityId == CityId);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var removeMe = db.People.First(x => x.PersonId == id);
            db.People.Remove(removeMe);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CheckUsers()
        {
            var AppDb = new ApplicationDbContext();

            var model = AppDb.Users.ToList();

            return View(model);
        }

        [Authorize(Users = "Admin@Admin.com")]
        public ActionResult AddAdmin(string id)
        {
            var AppDb = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(AppDb));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(AppDb));

            var model = AppDb.Users.First(x => x.Id == id);

            var check = UserManager.AddToRole(model.Id, "Admin");

            return RedirectToAction("CheckUsers");
        }

        [Authorize(Users = "Admin@Admin.com")]
        public ActionResult RemoveAdmin(string id)
        {
            var AppDb = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(AppDb));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(AppDb));

            var model = AppDb.Users.First(x => x.Id == id);

            var check = UserManager.RemoveFromRole(model.Id, "Admin");

            return RedirectToAction("CheckUsers");
        }



    }
}