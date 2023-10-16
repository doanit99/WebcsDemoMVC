using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBDemo.NETMVC.Context;

namespace WeBDemo.NETMVC.Controllers
{
	
	public class SearchController : Controller
    {
		WebCSEntities2 objWebsiteBanHangEntities = new WebCSEntities2();
		// GET: Search
		public ActionResult Index(string keyword)
        {
			if (!string.IsNullOrEmpty(keyword))
			{
				var searchResults = objWebsiteBanHangEntities.Products
					.Where(p => p.Name.Contains(keyword))
					.ToList();

				return View(searchResults);
			}
			else
			{
				// Nếu không có từ khóa, hiển thị tất cả sản phẩm
				var allProducts = objWebsiteBanHangEntities.Products.ToList();
				return View(allProducts);
			}

		}

		
	}
}