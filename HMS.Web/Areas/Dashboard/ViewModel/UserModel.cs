﻿
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
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
 
    }
}