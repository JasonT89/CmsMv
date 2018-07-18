using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CmsMvc.Models
{
    public class AddPersonViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }

        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }

        public AddPersonViewModel(Person person, List<Country> countries)
        {
            PersonId = person.PersonId;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Email = person.Email;
           
            CityId = person.CityId;
            City = person.City;

            if (countries != null && countries.Count > 0)
            {
                Countries = new List<Country>();
                Cities = new List<City>();

                foreach (var item in countries)
                {
                    Countries.Add(item);
                    if (item.Cities != null && item.Cities.Count > 0)
                    {
                        foreach (var item2 in item.Cities)
                        {
                            Cities.Add(item2);
                        }
                    }
                }
            }
        }
    }
}