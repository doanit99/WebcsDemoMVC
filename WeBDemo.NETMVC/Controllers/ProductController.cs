using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBDemo.NETMVC.Context;

namespace WeBDemo.NETMVC.Controllers
{
    public class ProductController : Controller
    {
		private WebCSEntities1 objWebCSEntities1 = new WebCSEntities1();

		// GET: Product
		public ActionResult Detail(int Id)
        {
			var ojbProduct = objWebCSEntities1.Products.Where(n=>n.Id == Id).FirstOrDefault();

			return View(ojbProduct);
        }
    }
}