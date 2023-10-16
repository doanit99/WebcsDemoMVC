using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBDemo.NETMVC.Context;
using WeBDemo.NETMVC.Models;

namespace WeBDemo.NETMVC.Controllers
{
    public class PaymentController : Controller
    {
		WebCSEntities2 objWebsiteBanHangEntities = new WebCSEntities2();

		// GET: Payment
		public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
				objOrder.Name = "ĐonHang" + DateTime.Now.ToString("yyyyMMddHHmmss");
				objOrder.UserId = int.Parse(Session["idUser"].ToString());
				objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
				objWebsiteBanHangEntities.Orders.Add(objOrder);
                objWebsiteBanHangEntities.SaveChanges();

                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach(var item in lstCart){
                    OrderDetail obj=new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId= intOrderId;
                    obj.ProductId=item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objWebsiteBanHangEntities.OrderDetails.AddRange(lstOrderDetail);
				objWebsiteBanHangEntities.SaveChanges();

			}
			return View();
        }
    }
}