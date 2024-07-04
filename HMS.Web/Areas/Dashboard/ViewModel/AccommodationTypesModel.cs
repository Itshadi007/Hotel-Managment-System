using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Web.Areas.Dashboard.ViewModel
{
    public class AccommodationTypesListingModel
    {
        public IEnumerable<AccommodationType > accommodationTypes {  get; set; }
    }
}