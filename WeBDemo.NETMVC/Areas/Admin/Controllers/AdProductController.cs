using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBDemo.NETMVC.Context;

namespace WeBDemo.NETMVC.Areas.Admin.Controllers
{
    public class AdProductController : Controller
    {
		WebCSEntities1 obj = new WebCSEntities1();

		// GET: Admin/AdProduct
		public ActionResult Index()
        {
            var lstProduct = obj.Products.ToList();

			return View(lstProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Product model, HttpPostedFileBase ImageUpload)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Kiểm tra xem có tệp tin hình ảnh được tải lên không
					if (ImageUpload != null && ImageUpload.ContentLength > 0)
					{
						string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
						string extension = Path.GetExtension(ImageUpload.FileName);
						fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
						model.Avatar = fileName;

						// Lưu trữ tệp tin hình ảnh vào thư mục
						var imagePath = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
						ImageUpload.SaveAs(imagePath);
					}

					// Lưu sản phẩm vào cơ sở dữ liệu
					obj.Products.Add(model);
					obj.SaveChanges();

					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				// Xử lý lỗi ở đây và ghi log nếu cần
				ModelState.AddModelError("", "Lỗi xử lý tạo sản phẩm: " + ex.Message);
			}

			// Nếu có lỗi, hiển thị trang Create lại với thông báo lỗi
			return View(model);
		}


		[HttpGet]
		public ActionResult Details(int id)
		{
			var objProduct = obj.Products.Where(n=>n.Id == id).FirstOrDefault();
			return View(objProduct);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var objProduct = obj.Products.Where(n => n.Id == id).FirstOrDefault();
			return View(objProduct);
		}

		[HttpPost]
		public ActionResult Delete(Product objPro)
		{
			var objProduct = obj.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
			obj.Products.Remove(objProduct);
			obj.SaveChanges();
			return RedirectToAction("Index");
		}


		[HttpGet]
		public ActionResult Edit(int id)
		{
			var objProduct = obj.Products.Where(n => n.Id == id).FirstOrDefault();
			return View(objProduct);
		}

		[HttpPost]
		public ActionResult Edit(Product objProduct)
		{
			// Kiểm tra xem có tệp tin hình ảnh được tải lên không
			if (objProduct.ImageUpload != null)
			{
				string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
				string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
				fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
				objProduct.Avatar = fileName;

				// Lưu trữ tệp tin hình ảnh vào thư mục
				var imagePath = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
				objProduct.ImageUpload.SaveAs(imagePath);
			}
			obj.Entry(objProduct).State = System.Data.Entity.EntityState.Modified;
			obj.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}