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
        public ActionResult Action()
        {
            AccommodationTypeActionModel model = new AccommodationTypeActionModel();
            return PartialView("_Action",model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationTypeActionModel model)
        {
            JsonResult jsonResult = new JsonResult();

           AccommodationType accommodationType = new AccommodationType();
            accommodationType.Name = model.Name;
                accommodationType.Description = model.Description;


            var result = accommodationTypesService.SaveAccommodationType(accommodationType);

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = true , Message = "Unable to add Accommodation Type" };
            }

            return jsonResult;
        }
    }
}