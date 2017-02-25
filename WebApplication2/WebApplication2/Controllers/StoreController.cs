using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class StoreController : Controller
    {
        ABComesticsEntities db = new ABComesticsEntities();

        // GET: Store
        public ActionResult Index()
        {
            var cate = db.Categories.ToList();
            return View(cate);
        }

        public ActionResult Browse(int categoryId, int? page)
        {
            var products = db.Products.Where(x => x.Category == categoryId).ToList();
            var cat = db.Categories.SingleOrDefault(x => x.Id == categoryId);
            var pager = new Pager(products.Count, page);

            var viewModel = new ProductViewModel
            {
                Items = products.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager,
                CategoryId = categoryId,
                CategoryName = cat != null ? cat.Name : "Category Name"
            };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var product = db.Products.Find(id);
            var listImg = db.ProductImages.Where(x => x.ProductId == id).Take(3);
            var relatedProd = db.Products.Where(x => x.Category == product.Category).Take(4);

            var vm = new ProductViewModel
            {
                Prodcut = product,
                ListImages = listImg.ToList(),
                RelatedProducts = relatedProd.ToList(),
                CategoryName = product.Category1.Name
            };
            return View(vm);
        }
    }
}