using HMS.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data
{
    public class HMSContext :DbContext
    {
        public HMSContext() : base("HMSConnectionString")
        {
        }

      
        public DbSet<Accommodation> accommodations { get; set; }
        public DbSet<AccommodationType> accommodationTypes { get; set; }
        public DbSet<AccommodationPackage> Accommodationspackages { get; set; }
        public DbSet<Booking> bookings { get; set; }
      

    }
}
