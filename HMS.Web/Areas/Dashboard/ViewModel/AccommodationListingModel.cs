using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Web.Areas.Dashboard.ViewModel
{
    public class AccommodationListingModel
    {
        public IEnumerable<Accommodation> accommodation{ get; set; }
        public string Search_Bar { get; set; }
    }
    public class AccommodationActionModel
    {
        public int ID { get; set; }
        public int AccommodationPackageID { get; set; }

        public AccommodationPackage AccommodationPackage { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Accommodation> Accommodation { get; set; }
    }

}