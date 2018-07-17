using System;
using CmsMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsMvc.Controllers
{
    public class CountryController : Controller
    {

        CmsDb db = new CmsDb();
        // GET: Country
        public ActionResult Index()
        {
            var model = db.Countries.Include("Cities").ToList();
            return View(model);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(country);
                db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = db.Countries.Include("Cities")
                                     .First(x => x.CountryId == id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Country country, params int[] Cities)
        {
            if (country != null)
            {
                var model = db.Countries.Include("Cities")
                                         .First(x => x.CountryId == country.CountryId);
                model.CountryName = country.CountryName;
                model.Population = country.Population;

                if (Cities != null)
                {
                    foreach (var item in Cities)
                    {
                        var removeMe = model.Cities.First(x => x.CityId == item);
                        model.Cities.Remove(removeMe);
                        db.Cities.Remove(removeMe);
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditCity(int id)
        {
            var model = db.Cities.Include("Country")
                                  .First(x => x.CityId == id);
            var model2 = db.Countries.ToList();

            var viewModel = new EditCityViewModel(model, model2);


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditCity(City city, int CountryId)
        {
            if (city == null)
            {
                return View();
            }
            var model = db.Cities.Include("Country").First(x => x.CityId == city.CityId);

            model.CityName = city.CityName;
            model.Population = city.Population;

            model.Country = db.Countries.First(x => x.CountryId == CountryId);
            model.CountryId = model.Country.CountryId;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AddCity(int CountryId)
        {
            var model = new City
            {
                CountryId = CountryId
            };
            var model2 = db.Countries.ToList();

            var viewModel = new EditCityViewModel(model, model2);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCity(City city, int CountryId)
        {
            if (city == null)
            {
                return View();
            }
            var model = new City
            {
                CityName = city.CityName,
                Population = city.Population,

                Country = db.Countries.First(x => x.CountryId == CountryId)
            };
            model.CountryId = model.Country.CountryId;

            db.Cities.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}