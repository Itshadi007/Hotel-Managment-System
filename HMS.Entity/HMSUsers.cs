using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entity
{
    public class HMSUsers
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int RoleID { get; set; }
        public IdentityRole Role { get; set; }
    }
}
