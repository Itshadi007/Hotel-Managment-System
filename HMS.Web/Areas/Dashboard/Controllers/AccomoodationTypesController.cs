using HMS.Services;
using HMS.Web.Areas.Dashboard.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}