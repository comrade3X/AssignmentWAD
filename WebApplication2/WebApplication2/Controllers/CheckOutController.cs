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
        private ABComesticsEntities storeDB = new ABComesticsEntities();

        // POST: Payment
        [HttpPost]
        public ActionResult Payment(ShopingCartViewModel vm)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var cartItems = cart.GetCartItems();

            if (cartItems.Count == 0)
            {
                return RedirectToAction("Index", "Store");
            }
            try
            {
                var customer = new Customer
                {
                    Name = vm.Name,
                    Phone = vm.Phone,
                    Address = vm.Address,
                    Email = vm.Email,
                    Gender = vm.Gender
                };

                storeDB.Customers.Add(customer);

                var emp = storeDB.Memberships.Find(Convert.ToInt32(Session["UserID"].ToString())).Employee;
                var order = new Order
                {
                    Seller = emp.Id,
                    Customer = customer,
                    TotalPrice = vm.CartTotal,
                    CreatedDate = DateTime.Now
                };
                storeDB.Orders.Add(order);

                foreach (var item in cartItems)
                {
                    var orderDetails = new OrderDetail
                    {
                        Order = order,
                        ProductId = item.ProductId,
                        Remarks = string.Empty,
                        ShippingAddress = vm.Address,
                        quantity = Convert.ToInt32(item.Count),
                        UnitPrice = item.Product.Price
                    };
                    storeDB.OrderDetails.Add(orderDetails);
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Store");
            }

            storeDB.SaveChanges();
            cart.EmptyCart();

            return View();
        }
    }
}