using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBDemo.NETMVC.Context;
using WeBDemo.NETMVC.Models;

namespace WeBDemo.NETMVC.Controllers
{
	public class HomeController : Controller
	{
		private WebCSEntities1 objWebCSEntities1 = new WebCSEntities1();

		public ActionResult Index()
		{
			HomeModel ojbHomeModel = new HomeModel();
			ojbHomeModel.ListCategory = objWebCSEntities1.Categories.ToList();
			ojbHomeModel.ListProduct = objWebCSEntities1.Products.ToList();
			return View(ojbHomeModel);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}