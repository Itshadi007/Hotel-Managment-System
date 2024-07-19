using HMS.Data;
using HMS.Entity;
using HMS.Services;
using HMS.Web.Areas.Dashboard.ViewModel;
using HMS.Web.Controllers;
using HMS.Web.Models;
using HMS.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HMS.Web.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Dashboard/Users
        AccommodationServices accommodationServices = new AccommodationServices();
        AccommodationPakageServices accommodationPakageServices = new AccommodationPakageServices();




        public ActionResult Index(string Search_Bar, string roleID, int? PageNo)
        {
            int recordCount = 3;

            PageNo = PageNo ?? 1;
           UsersListingModel model = new UsersListingModel();

            model.Search_Bar = Search_Bar;
            model.RoleID = roleID;
            var users = 
            model.Users = SearchUser(Search_Bar,roleID,PageNo.Value,recordCount);

            // var abc = accommodationPakageServices.SearchAccommodationPackage(Search_Bar);
            //   model.Roles = accommodationPakageServices.GetAllAccommodationPackage();

            var totalRecords = SearchUserCount(Search_Bar, roleID);
            model.pager = new Pager(totalRecords, PageNo, recordCount);

            return View(model);


        }

        public List<ApplicationUser> SearchUser(string searchBar, string roleID, int pageNo, int recordCount)
        {
            IQueryable<ApplicationUser> users = UserManager.Users;

            // Apply search criteria
            if (!string.IsNullOrEmpty(searchBar))
            {
                users = users.Where(u => u.Email.ToLower().Contains(searchBar.ToLower()));
            }

            // Apply role filter if needed
            if (!string.IsNullOrEmpty(roleID))
            {
                // Implement role filtering logic here if necessary
            }

            // Perform pagination
            int skip = (pageNo - 1) * recordCount;
            users = users.OrderBy(u => u.Email).Skip(skip).Take(recordCount);

            return users.ToList();
        }


        public int SearchUserCount(string searchBar, string roleID)
        {
            IQueryable<ApplicationUser> users = UserManager.Users;

            // Apply search criteria
            if (!string.IsNullOrEmpty(searchBar))
            {
                users = users.Where(u => u.Email.ToLower().Contains(searchBar.ToLower()));
            }

            // Apply role filter if needed
            if (!string.IsNullOrEmpty(roleID))
            {
                // Implement role filtering logic here if necessary
            }

         
         

            return users.Count();
        }
        [HttpGet]
        public async Task< ActionResult> Action(string ID)
        {
            UsersActionModel model = new UsersActionModel();
            if (!string.IsNullOrEmpty(ID)) // trying to Edit
            {
                var Users =  await UserManager.FindByIdAsync(ID);

                model.ID = Users.Id;
                //model.FullName = Users.FullName;
                //model.Email = Users.Email;
                //model.Address = Users.Address;
                model.UserName = Users.UserName;
                //model.City = Users.City;

            }
           // model.Roles = accommodationPakageServices.GetAllAccommodationPackage();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task< JsonResult> Action(UsersActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) // Trying to Edit Record Based On ID
            {
                var Users = await UserManager.FindByIdAsync(model.ID);

                //Users.FullName = model.FullName;
                //Users.Address = model.Address;
           //     Users.UserName = model.UserName;
                //Users.City = model.City;
            //    Users.Email = model.Email;

                result  = await UserManager.UpdateAsync(Users);

            }
            else
            {
                var Users = new ApplicationUser();

                //Users.FullName = model.FullName;
                //Users.Address = model.Address;
                Users.UserName = model.UserName;
                //Users.City = model.City;
                Users.Email = model.Email;

                result = await UserManager.CreateAsync(Users);
            }

            jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(" ", result.Errors) };
            return jsonResult;
        }
        [HttpGet]
        public async Task< ActionResult> Delete(string ID)
        {
            UsersActionModel model = new UsersActionModel();

            var Users = await UserManager.FindByIdAsync(ID);

            model.ID = Users.Id;
            model.UserName = Users.UserName;
         model.Email = Users.Email;

            return PartialView("_Delete", model);
        }


        [HttpPost]
        public async Task<JsonResult> Delete(UsersActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) // Trying to Edit Record Based On ID
            {
                var Users = await UserManager.FindByIdAsync(model.ID);


                //Users.FullName = model.FullName;
                //Users.Address = model.Address;
                //     Users.UserName = model.UserName;
                //Users.City = model.City;
                //    Users.Email = model.Email;

                result = await UserManager.DeleteAsync(Users);
                jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(" ", result.Errors) };


            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Invalid User" };
            }
            return jsonResult;


        }
    }
}