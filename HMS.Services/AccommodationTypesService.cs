using HMS.Data;
using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public  class AccommodationTypesService
    {
        public IEnumerable<AccommodationType> GetAllAccommodationTypes()
        {
            var context = new HMSContext();

            return context.accommodationTypes.AsEnumerable();
        }

        public bool SaveAccommodationType(AccommodationType accommodationType)
        {
            var context = new HMSContext();

            context.accommodationTypes.Add(accommodationType);

            return context.SaveChanges() >0;
        }
    }
}
