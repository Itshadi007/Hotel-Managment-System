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
    public class AccommodationPackagesController : Controller
    {
        AccommodationPakageServices accommodationPakageServices = new AccommodationPakageServices();

        AccommodationTypesService AccommodationTypesService = new AccommodationTypesService();

        public ActionResult Index(string Search_Bar, int? AccommodationTypeID,int? PageNo)
        {
            int recordCount = 5;

            PageNo = PageNo ?? 1;
            AccommodationPackageListingModel model = new AccommodationPackageListingModel();

            model.Search_Bar = Search_Bar;
            model.AccommodationTypeID = AccommodationTypeID;
         



            model.accommodationpackages = accommodationPakageServices.SearchAccommodationPackage(Search_Bar, AccommodationTypeID, PageNo.Value,recordCount);
            // var abc = accommodationPakageServices.SearchAccommodationPackage(Search_Bar);

            model.AccommodationTypes = AccommodationTypesService.GetAllAccommodationTypes();

            var totalRecords = accommodationPakageServices.SearchAccommodationPackageCount(Search_Bar, AccommodationTypeID);


            model.pager = new Pager(totalRecords, PageNo, recordCount);

            return View(model);


        }



        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationPackageActionModel model = new AccommodationPackageActionModel();
            if (ID.HasValue)// trying to Edit
            {
                var accommodationPackage = accommodationPakageServices.GetAccommodationType(ID.Value);

                model.ID = accommodationPackage.ID;
                model.Name = accommodationPackage.Name;
                model.AccommodationTypeID = accommodationPackage.AccommodationTypeID;
                model.NoOfRoom = accommodationPackage.NoOfRoom;
                model.FeePerNight = accommodationPackage.FeePerNight;
                model.AccommodationType = accommodationPackage.AccommodationType;


            }
            model.AccommodationTypes = AccommodationTypesService.GetAllAccommodationTypes();

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationPackageActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            if (model.ID > 0)// Trying to Edit Record Base On ID

            {
                var accommodationPackage = accommodationPakageServices.GetAccommodationType(model.ID);
                accommodationPackage.AccommodationType = model.AccommodationType;
                accommodationPackage.Name = model.Name;
                accommodationPackage.NoOfRoom = model.NoOfRoom;
                accommodationPackage.FeePerNight = model.FeePerNight;
                accommodationPackage.AccommodationTypeID = model.AccommodationTypeID;
                result = accommodationPakageServices.UpdateAccommodationType(accommodationPackage);

            }
            else

            {
                AccommodationPackage accommodationPackage = new AccommodationPackage();
                accommodationPackage.Name = model.Name;
                accommodationPackage.AccommodationTypeID = model.ID;
                accommodationPackage.AccommodationTypeID = model.AccommodationTypeID;
                accommodationPackage.NoOfRoom = model.NoOfRoom;
                accommodationPackage.FeePerNight = model.FeePerNight;   
                result = accommodationPakageServices.SaveAccommodationType(accommodationPackage);

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

            var accommodationPackage = accommodationPakageServices.GetAccommodationType(ID);

            model.ID = accommodationPackage.ID;
            model.Name = accommodationPackage.Name;
            model.AccommodationTypeID = accommodationPackage.AccommodationTypeID;
            model.NoOfRoom = accommodationPackage.NoOfRoom;
            model.FeePerNight = accommodationPackage.FeePerNight;
            model.AccommodationType = accommodationPackage.AccommodationType;

            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(AccommodationPackageActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            var accommodationPackage = accommodationPakageServices.GetAccommodationType(model.ID);


            result = accommodationPakageServices.DeleteAccommodationType(accommodationPackage);

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