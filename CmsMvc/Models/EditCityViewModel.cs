using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CmsMvc.Models
{
    public class EditCityViewModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int Population { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public List<Country> Countries { get; set; }

        public EditCityViewModel(City city, List<Country> countries)
        {
            CityId = city.CityId;
            CityName = city.CityName;
            Population = city.Population;
            CountryId = city.CountryId;
            Country = city.Country;

            if (countries != null && countries.Count > 0)
            {
                Countries = new List<Country>();
                foreach (var item in countries)
                {
                    Countries.Add(item);
                }
            }
        }
    }
}