using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CheckOutController : Controller
    {
        private ABComesticsEntities storeDB =new ABComesticsEntities();

        // GET: CheckOut
        public ActionResult Index()
        {
            return View();
        }
    }
}