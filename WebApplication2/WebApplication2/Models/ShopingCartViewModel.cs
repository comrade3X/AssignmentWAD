using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ShopingCartViewModel
    {
        public virtual List<Cart> CartItems { get; set; }
        public virtual decimal CartTotal { get; set; }

        #region -- Customer Information --

        public virtual string Name { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Address { get; set; }

        public virtual string Email { get; set; }

        public virtual string Gender { get; set; }

        #endregion

        public virtual int SellerId { get; set; }

    }
}