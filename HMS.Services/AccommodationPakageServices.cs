using HMS.Data;
using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccommodationPakageServices
    {
        public IEnumerable<AccommodationPackage> GetAllAccommodationPackage()
        {
            var context = new HMSContext();

            return context.Accommodationspackages.AsEnumerable();
        }

        public List<AccommodationPackage> SearchAccommodationPackage(string searchBar, int? accommodationTypeID, int pageNo, int recordCount)
        {
            //using ()
            {
                var context = new HMSContext();
                var accommodationPackage = context.Accommodationspackages.AsQueryable();
                //   var ab = query.Include(x => x.AccommodationType);
                if (!string.IsNullOrEmpty(searchBar))
                {
                    accommodationPackage = accommodationPackage.Where(a => a.Name.ToLower().Contains(searchBar.ToLower()));
                }

                if (accommodationTypeID.HasValue && accommodationTypeID.Value > 0)
                {
                    accommodationPackage = accommodationPackage.Where(a => a.AccommodationTypeID == accommodationTypeID.Value);
                }


                var skip = (pageNo-1) *recordCount;


                return accommodationPackage.OrderBy(x => x.Name).Skip(skip).Take(recordCount).ToList();

              //  return accommodationPackage.ToList();
            }
        }


        public int SearchAccommodationPackageCount(string searchBar, int? accommodationTypeID)
        {
            //using ()
            {
                var context = new HMSContext();
                var accommodationPackage = context.Accommodationspackages.AsQueryable();
                //   var ab = query.Include(x => x.AccommodationType);
                if (!string.IsNullOrEmpty(searchBar))
                {
                    accommodationPackage = accommodationPackage.Where(a => a.Name.ToLower().Contains(searchBar.ToLower()));
                }

                if (accommodationTypeID.HasValue && accommodationTypeID.Value > 0)
                {
                    accommodationPackage = accommodationPackage.Where(a => a.AccommodationTypeID == accommodationTypeID.Value);
                }



             //   return accommodationPackage.Count();

                  return accommodationPackage.Count();
            }
        }



        public AccommodationPackage GetAccommodationPackage(int ID)
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
            using (var context = new HMSContext())
            {
                AccommodationPackage ab = new AccommodationPackage();
                ab = context.Accommodationspackages.Find(accommodationPackage.ID);
                if (ab != null)
                {
                    context.Accommodationspackages.Remove(ab);
                    return context.SaveChanges() > 0;
                }
                return false; // Return false if accommodationTypeToDelete is null

            }
        }
    }
}
