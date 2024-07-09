using HMS.Entity;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace HMS.Web.Areas.Dashboard.ViewModel
{
    public class AccommodationPackageListingModel
    {
       
            public IEnumerable<AccommodationPackage> accommodationpackages { get; set; }
            public string Search_Bar { get; set; }
    }

    public class AccommodationPackageActionModel
    {
        public int ID { get; set; }

        public int AccommodationTypeID { get; set; }

        public AccommodationType AccommodationType { get; set; }

        public string Name { get; set; }

        public int NoOfRoom { get; set; }

        public decimal FeePerNight { get; set; }


        public IEnumerable<AccommodationType> AccommodationTypes { get; set; }
    }
}