using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private ABComesticsEntities storeDB = new ABComesticsEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Membership objUser)
        {
            if (Session["UserID"] != null || Session["UserName"] != null)
            {
                return RedirectToAction("UserDashBoard");
            }

            if (!ModelState.IsValid) return View(objUser);

            using (var db = new ABComesticsEntities())
            {
                var obj = db.Memberships.FirstOrDefault(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password));
                if (obj != null)
                {
                    Session["UserID"] = obj.Id.ToString();
                    Session["UserName"] = obj.Username.ToString();
                    return RedirectToAction("Index", "Store");
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login");

            var employee = storeDB.Memberships.Find(Convert.ToInt32(Session["UserID"].ToString())).Employee;
            if (employee == null) return RedirectToAction("Login");

            var vm = new UserDashBoardViewModel
            {
                Store = storeDB.Stores.Find(employee.StoreId),
                Role = employee.Role,
                Name = employee.Name,
                DoB = employee.Dob,
                Gender = employee.Gender,
                Address = employee.Address,
                Phone = employee.Phone
            };
            return View(vm);
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}