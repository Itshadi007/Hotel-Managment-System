using HMS.Entity;
using HMS.Services;
using HMS.Web.Areas.Dashboard.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace HMS.Web.Areas.Dashboard.Controllers
{
    public class AccommodationTypesController : Controller
    {
        AccommodationTypesService accommodationTypesService = new AccommodationTypesService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listing()
        {
            AccommodationTypesListingModel model = new AccommodationTypesListingModel();

            model.accommodationTypes = accommodationTypesService.GetAllAccommodationTypes();


            return PartialView("_Listing", model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationTypeActionModel model = new AccommodationTypeActionModel();
            if (ID.HasValue)// trying to Edit
            {
                var accommodationType = accommodationTypesService.GetAccommodationType(ID.Value);

                model.ID = accommodationType.ID;
                model.Name = accommodationType.Name;
                model.Description = accommodationType.Description;


            }

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationTypeActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            if (model.ID > 0)// Trying to Edit Record Base On ID

            {
                var accommodationType = accommodationTypesService.GetAccommodationType(model.ID);

                accommodationType.Name = model.Name;
                accommodationType.Description = model.Description;
                result = accommodationTypesService.UpdateAccommodationType(accommodationType);

            }
            else

            {
                AccommodationType accommodationType = new AccommodationType();
                accommodationType.Name = model.Name;
                accommodationType.Description = model.Description;


                result = accommodationTypesService.SaveAccommodationType(accommodationType);

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
            AccommodationTypeActionModel model = new AccommodationTypeActionModel();

            var accommodationType = accommodationTypesService.GetAccommodationType(ID);

            model.ID = accommodationType.ID;

            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(AccommodationTypeActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            var accommodationType = accommodationTypesService.GetAccommodationType(model.ID);


              result = accommodationTypesService.DeleteAccommodationType(accommodationType);
            
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

    }
}