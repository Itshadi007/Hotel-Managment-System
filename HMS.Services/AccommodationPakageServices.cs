using HMS.Data;
using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccommodationPakageServices
    {
        public IEnumerable<AccommodationPackage> GetAllAccommodationTypes()
        {
            var context = new HMSContext();

            return context.Accommodationspackages.AsEnumerable();
        }

        public IEnumerable<AccommodationPackage> SearchlAccommodationTypes(string Search_Bar)
        {
            var context = new HMSContext();


            var accommodationPackage = context.Accommodationspackages.AsQueryable();


            if (!string.IsNullOrEmpty(Search_Bar))
            {
                accommodationPackage = accommodationPackage.Where(a => a.Name.ToLower().Contains(Search_Bar.ToLower()));
            }
            return accommodationPackage.AsEnumerable();
        }


        public AccommodationPackage GetAccommodationType(int ID)
        {
            var context = new HMSContext();

            return context.Accommodationspackages.Find(ID);
        }
        public bool SaveAccommodationType(AccommodationPackage accommodationPackage)
        {
            var context = new HMSContext();

            context.Accommodationspackages.Add(accommodationPackage);

            return context.SaveChanges() > 0;
        }

        public bool UpdateAccommodationType(AccommodationPackage accommodationPackage)
        {
            var context = new HMSContext();

            context.Accommodationspackages.AddOrUpdate(accommodationPackage);

            //     context.Entry(accommodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccommodationType(AccommodationPackage accommodationPackage)
        {
            var context = new HMSContext();

            //   context.accommodationTypes.Remove(accommodationType.ID);

            context.Entry(accommodationPackage).State = System.Data.Entity.EntityState.Deleted;

            //     context.Entry(accommodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
    }
}
