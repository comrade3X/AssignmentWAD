using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class UserDashBoardViewModel
    {
        public virtual string Name { get; set; }
        public virtual Store Store { get; set; }
        public virtual Role Role { get; set; }
        public virtual DateTime DoB { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Address { get; set; }
        public virtual string Phone { get; set; }

    }
}