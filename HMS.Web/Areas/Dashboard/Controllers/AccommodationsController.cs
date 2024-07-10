using HMS.Entity;
using HMS.Services;
using HMS.Web.Areas.Dashboard.ViewModel;
using HMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Web.Areas.Dashboard.Controllers
{
    public class AccommodationsController : Controller
    {
        // GET: Dashboard/Accommodations
        AccommodationServices accommodationServices = new AccommodationServices();

        public ActionResult Index(string Search_Bar)
        {
            AccommodationListingModel model = new AccommodationListingModel();

            model.Search_Bar = Search_Bar;
            model.accommodation = accommodationServices.SearchAccommodation(Search_Bar);
            return View(model);
          }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationActionModel model = new AccommodationActionModel();
            if (ID.HasValue)// trying to Edit
            {
                var accommodationPackage = accommodationServices.GetAccommodation(ID.Value);

                model.ID = accommodationPackage.ID;
                model.Name = accommodationPackage.Name;

            }
            model.Accommodation = accommodationServices.GetAllAccommodation();

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationPackageActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            if (model.ID > 0)// Trying to Edit Record Base On ID

            {
                var accommodationPackage = accommodationServices.GetAccommodation(model.ID);
               
                result = accommodationServices.UpdateAccommodation(accommodationPackage);

            }
            else

            {
                Accommodation accommodation = new Accommodation();
                accommodation.Name = model.Name;
      
                result = accommodationServices.SaveAccommodation(accommodation);

            }
            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = true, Message = "Unable to Perform Action Accommodation Type" };
            }

            return jsonResult;

        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccommodationPackageActionModel model = new AccommodationPackageActionModel();

            var accommodationPackage = accommodationServices.GetAccommodation(ID);

            model.ID = accommodationPackage.ID;
            model.Name = accommodationPackage.Name;
     

            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(AccommodationPackageActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            var accommodationPackage = accommodationServices.GetAccommodation(model.ID);


            result = accommodationServices.DeleteAccommodation(accommodationPackage);

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = true, Message = "Unable to Perform Action Accommodation Pacakge" };
            }

            return jsonResult;


        }
    }


}