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

        public ActionResult Index(string Search_Bar, int? AccommodationTypeID, int? PageNo)
        {
            int recordCount = 5;

            PageNo = PageNo ?? 1;
            AccommodationListingModel model = new AccommodationListingModel();
            model.Search_Bar = Search_Bar;
            model.AccommodationTypeID = AccommodationTypeID;

            model.accommodationpackages = accommodationServices.SearchAccommodationPackage(Search_Bar, AccommodationTypeID, PageNo.Value, recordCount);
            // var abc = accommodationPakageServices.SearchAccommodationPackage(Search_Bar);

     //       model.AccommodationTypes = (IEnumerable<AccommodationType>)accommodationServices.GetAllAccommodationTypes();

            var totalRecords = accommodationServices.SearchAccommodationPackageCount(Search_Bar, AccommodationTypeID);


            //   model.pager = new Pager(totalRecords, PageNo, recordCount);

            return View(model);


        }
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationActionModel model = new AccommodationActionModel();
            if (ID.HasValue)// trying to Edit
            {
                var accommodationPackage = accommodationServices.GetAccommodationType(ID.Value);

                model.ID = accommodationPackage.ID;
                model.Name = accommodationPackage.Name;
           }
       //     model.AccommodationTypes = (IEnumerable<AccommodationType>)accommodationServices.GetAllAccommodationTypes();

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            if (model.ID > 0)// Trying to Edit Record Base On ID

            {
                var accommodationPackage = accommodationServices.GetAccommodationType(model.ID);
                accommodationPackage.AccommodationType = (AccommodationType)model.AccommodationTypes;
                accommodationPackage.Name = model.Name;
             result = accommodationServices.UpdateAccommodationType(accommodationPackage);

            }
            else

            {
                AccommodationPackage accommodationPackage = new AccommodationPackage();
                accommodationPackage.Name = model.Name;
                accommodationPackage.AccommodationTypeID = model.ID;
                result = accommodationServices.SaveAccommodationType(accommodationPackage);

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

            var accommodationPackage = accommodationServices.GetAccommodationType(ID);

            model.ID = accommodationPackage.ID;
            model.Name = accommodationPackage.Name;
            model.AccommodationTypeID = accommodationPackage.AccommodationTypeID;
            model.NoOfRoom = accommodationPackage.NoOfRoom;
            model.FeePerNight = accommodationPackage.FeePerNight;
            model.AccommodationType = accommodationPackage.AccommodationType;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccommodationActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            var accommodationPackage = accommodationServices.GetAccommodationType(model.ID);


            result = accommodationServices.DeleteAccommodationType(accommodationPackage);

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