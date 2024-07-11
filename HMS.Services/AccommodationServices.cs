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
    public class AccommodationServices
    {
        public IEnumerable<Accommodation> GetAllAccommodation()
        {
            var context = new HMSContext();

            return context.accommodations;
        }

        public List<Accommodation> SearchAccommodation(string Search_Bar, int? AccommodationpackageID, int pageNo, int recordCount)
        {
            //using ()
            var context = new HMSContext();
            var accommodation = context.accommodations.AsQueryable();
            //   var ab = query.Include(x => x.AccommodationType);
            if (!string.IsNullOrEmpty(Search_Bar))
            {
                accommodation = accommodation.Where(a => a.Name.ToLower().Contains(Search_Bar.ToLower()));
            }

            if (AccommodationpackageID.HasValue && AccommodationpackageID.Value > 0)
            {
                accommodation = accommodation.Where(a => a.AccommodationPackageID == AccommodationpackageID.Value);
            }


            var skip = (pageNo - 1) * recordCount;


            return accommodation.OrderBy(x => x.Name).Skip(skip).Take(recordCount).ToList();
        }


        public int SearchAccommodationCount(string searchBar, int? AccommodationpackageID)
        {
            //using ()
            {
                var context = new HMSContext();
                var accommodation = context.accommodations.AsQueryable();
                //   var ab = query.Include(x => x.AccommodationType);
                if (!string.IsNullOrEmpty(searchBar))
                {
                    accommodation = accommodation.Where(a => a.Name.ToLower().Contains(searchBar.ToLower()));
                }

                if (AccommodationpackageID.HasValue && AccommodationpackageID.Value > 0)
                {
                    accommodation = accommodation.Where(a => a.AccommodationPackageID == AccommodationpackageID.Value);
                }



                //   return accommodationPackage.Count();

                return accommodation.Count();
            }
        }



        public Accommodation GetAccommodation(int ID)
        {
            var context = new HMSContext();

            return context.accommodations.Find(ID);

        }
        public bool SaveAccommodation(Accommodation accommodation)
        {
            var context = new HMSContext();

            context.accommodations.Add(accommodation);

            return context.SaveChanges() > 0;
        }

        public bool UpdateAccommodation(Accommodation accommodation)
        {
            var context = new HMSContext();


            context.accommodations.AddOrUpdate(accommodation);

            //     context.Entry(accommodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;

        }

        public bool DeleteAccommodation(Accommodation accommodatione)
        {
            using (var context = new HMSContext())
            {
                Accommodation ab = new Accommodation();
                ab = context.accommodations.Find(accommodatione.ID);
                if (ab != null)
                {
                    context.accommodations.Remove(ab);
                    return context.SaveChanges() > 0;
                }
                return false; // Return false if accommodationTypeToDelete is null

            }
        }
    }
}
