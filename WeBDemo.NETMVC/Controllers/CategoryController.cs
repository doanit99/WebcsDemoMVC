using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;
using WeBDemo.NETMVC.Context;

namespace WeBDemo.NETMVC.Controllers
{
    public class CategoryController : Controller
    {
		WebCSEntities2 objWebsiteBanHangEntities = new WebCSEntities2();
		// GET: Category
		public ActionResult Index(int? CateId)
		{


			var Results = objWebsiteBanHangEntities.Products
				.Where(p => p.CategoryId == CateId)
				.ToList();

			ViewBag.Message = Results.Count > 0 ? null : "Không có sản phẩm trong danh mục này.";

			return View(Results);
		}
    }
}