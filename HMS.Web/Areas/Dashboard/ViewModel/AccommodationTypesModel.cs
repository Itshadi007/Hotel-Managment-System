using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Web.Areas.Dashboard.ViewModel
{
    public class AccommodationTypesListingModel
    {
        public IEnumerable<AccommodationType> accommodationTypes { get; set; }
        public string Search_Bar { get; set; }
    }

    public class AccommodationTypeActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}