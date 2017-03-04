using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class ManagementViewModel
    {
        public virtual string StoreName { get; set; }

        public virtual DateTime DateFrom { get; set; }

        public virtual DateTime DateTo { get; set; }

        public virtual decimal Total { get; set; }

        public virtual List<ReportViewModel> ReportsViewModel { get; set; }

        public virtual SelectList StoreSelectList { get; set; }

        public virtual string SearchOption { get; set; }
        
    }

    public class ReportViewModel
    {
        public virtual string StoreName { get; set; }

        public virtual decimal Total { get; set; }
        public virtual string StoreAddress { get; set; }
        public virtual string EmployeeName { get; set; }
        public virtual string EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public virtual string ListProductsName { get; set; }
    }
}