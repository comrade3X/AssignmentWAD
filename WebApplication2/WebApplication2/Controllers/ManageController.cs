using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ManageController : Controller
    {
        private ABComesticsEntities storeDB = new ABComesticsEntities();
        // GET: Manage
        public ActionResult Index()
        {
            if (!IsManager())
            {
                return RedirectToAction("Login", "Account");
            }
            var vm = new ManagementViewModel
            {
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                StoreSelectList = GetStoreSelectList(),
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult GetReport(int storeId, DateTime? from, DateTime? to, string option)
        {
            if (storeId == 0)
            {
                return PartialView("_report", new ManagementViewModel());
            }

            var linq = from s in storeDB.Orders.ToList().Where(x => x.Employee.StoreId == storeId)
                       select s;

            if (from == null && to == null)
            {
                if ("Daily".Equals(option))
                {
                    linq = linq.Where(x => x.CreatedDate.Date == DateTime.Now.Date).ToList();
                }
                else
                {
                    // Search current month 
                    var now = DateTime.Now;
                    var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    linq = linq.Where(x => x.CreatedDate.Date >= firstDayOfMonth.Date
                        && x.CreatedDate.Date <= lastDayOfMonth.Date).ToList();
                }
            }
            else
            {
                /*Search custom date range*/
                linq = linq.Where(x => x.CreatedDate.Date >= from.Value.Date && x.CreatedDate.Date <= to.Value.Date).ToList();
            }

            var list = from s in linq
                       let details=s.OrderDetails
                       select new ReportViewModel
                       {
                           StoreName = storeDB.Stores.Find(s.Employee.StoreId).Name,
                           StoreAddress = storeDB.Stores.Find(s.Employee.StoreId).Address,
                           Date = s.CreatedDate,
                           Total = s.TotalPrice,
                           EmployeeName = s.Employee.Name
                       };
         

            var listReports = list.ToList();
            var vm = new ManagementViewModel
            {
                Total = listReports.ToList().Sum(x => x.Total),
                ReportsViewModel = listReports.ToList()
            };

            return PartialView("_report", vm);
        }

        #region -- Helper -- 

        private SelectList GetStoreSelectList()
        {
            var listStore = storeDB.Stores.ToList();
            var linq = from s in listStore
                       select new SelectListItem
                       {
                           Text = s.Name,
                           Value = s.Id.ToString()
                       };

            var sl = new SelectList(linq, "Value", "Text");
            return sl;
        }

        private bool IsManager()
        {
            try
            {
                using (var storeDb = new ABComesticsEntities())
                {
                    var membership = storeDb.Memberships.Find(Convert.ToInt32(Session["UserID"].ToString()));
                    if (membership == null)
                    {
                        return false;
                    }

                    var emp = membership.Employee;
                    var empRole = emp.Role.Name;
                    if (!"Manager".Equals(empRole))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;

            }
        }

        #endregion
    }
}