
using HMS.Entity;
using HMS.Web.Models;
using HMS.Web.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Web.Areas.Dashboard.ViewModel
{
    public class UsersListingModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public string Search_Bar { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public Pager pager { get; set; }
        public string RoleID { get; set; }
    }
    public class UsersActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string RoleID { get; set; }
        public IdentityRole Role { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}