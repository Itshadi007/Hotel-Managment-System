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
    public class UsersController : Controller
    {
        // GET: Dashboard/Users
        AccommodationServices accommodationServices = new AccommodationServices();
        AccommodationPakageServices accommodationPakageServices = new AccommodationPakageServices();

        public ActionResult Index(string Search_Bar, string roleID, int? PageNo)
        {
            int recordCount = 5;

            PageNo = PageNo ?? 1;
           UsersListingModel model = new UsersListingModel();

            model.Search_Bar = Search_Bar;
            model.RoleID = roleID;
            //  model.Users = accommodationServices.SearchAccommodation(Search_Bar, roleID, PageNo.Value, recordCount);
            // var abc = accommodationPakageServices.SearchAccommodationPackage(Search_Bar);
            //   model.Roles = accommodationPakageServices.GetAllAccommodationPackage();

            var totalRecords = 0;// accommodationServices.SearchAccommodationCount(Search_Bar, roleID);
            model.pager = new Pager(totalRecords, PageNo, recordCount);

            return View(model);


        }


        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationActionModel model = new AccommodationActionModel();
            if (ID.HasValue) // trying to Edit
            {
                var accommodation = accommodationServices.GetAccommodation(ID.Value);

                model.ID = accommodation.ID;
                model.Name = accommodation.Name;
                model.Description = accommodation.Description;
                model.AccommodationPackageID = accommodation.AccommodationPackageID;
                model.AccommodationPackage = accommodation.AccommodationPackage;
            }
            model.AccommodationPackages = accommodationPakageServices.GetAllAccommodationPackage();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccommodationActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            if (model.ID > 0) // Trying to Edit Record Based On ID
            {
                var accommodation = accommodationServices.GetAccommodation(model.ID);
                accommodation.AccommodationPackage = model.AccommodationPackage;
                accommodation.Name = model.Name;
                accommodation.Description = model.Description;
                accommodation.AccommodationPackageID = model.AccommodationPackageID;
                result = accommodationServices.UpdateAccommodation(accommodation);
            }
            else
            {
                Accommodation accommodation = new Accommodation();
                accommodation.Name = model.Name;
                accommodation.AccommodationPackageID = model.AccommodationPackageID;
                accommodation.Description = model.Description;
                result = accommodationServices.SaveAccommodation(accommodation);
            }

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to Perform Action Accommodation Type" };
            }

            return jsonResult;
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccommodationActionModel model = new AccommodationActionModel();

            var accommodation = accommodationServices.GetAccommodation(ID);

            model.ID = accommodation.ID;
            model.Name = accommodation.Name;
            model.AccommodationPackageID = accommodation.AccommodationPackageID;

            model.Description = accommodation.Description;
            model.AccommodationPackage = accommodation.AccommodationPackage;

            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(AccommodationActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            var accommodation = accommodationServices.GetAccommodation(model.ID);


            result = accommodationServices.DeleteAccommodation(accommodation);

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