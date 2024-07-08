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
    public class AccommodationTypesService
    {
        public IEnumerable<AccommodationType> GetAllAccommodationTypes()
        {
            var context = new HMSContext();

            return context.accommodationTypes.AsEnumerable();
        }

        public AccommodationType GetAccommodationType(int ID)
        {
            var context = new HMSContext();

            return context.accommodationTypes.Find(ID);
        }
        public bool SaveAccommodationType(AccommodationType accommodationType)
        {
            var context = new HMSContext();

            context.accommodationTypes.Add(accommodationType);

            return context.SaveChanges() > 0;
        }

        public bool UpdateAccommodationType(AccommodationType accommodationType)
        {
            var context = new HMSContext();

            context.accommodationTypes.AddOrUpdate(accommodationType);

            //     context.Entry(accommodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccommodationType(AccommodationType accommodationType)
        {
            var context = new HMSContext();

             //   context.accommodationTypes.Remove(accommodationType.ID);

            context.Entry(accommodationType).State = System.Data.Entity.EntityState.Deleted;

            //     context.Entry(accommodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
    }
}
