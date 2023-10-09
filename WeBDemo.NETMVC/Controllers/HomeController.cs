using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

		//GET: Register

		public ActionResult Register()
		{
			return View();
		}

		//POST: Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(User _user)
		{
			if (ModelState.IsValid)
			{
				var check = objWebCSEntities1.Users.FirstOrDefault(s => s.Email == _user.Email);
				if (check == null)
				{
					_user.Password = GetMD5(_user.Password);
					objWebCSEntities1.Configuration.ValidateOnSaveEnabled = false;
					objWebCSEntities1.Users.Add(_user);
					objWebCSEntities1.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.error = "Email already exists";
					return View();
				}


			}
			return View();


		}

		//create a string MD5
		public static string GetMD5(string str)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] fromData = Encoding.UTF8.GetBytes(str);
			byte[] targetData = md5.ComputeHash(fromData);
			string byte2String = null;

			for (int i = 0; i < targetData.Length; i++)
			{
				byte2String += targetData[i].ToString("x2");

			}
			return byte2String;
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(string email, string password)
		{
			if (ModelState.IsValid)
			{


				var f_password = GetMD5(password);
				var data = objWebCSEntities1.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
				if (data.Count() > 0)
				{
					//add session
					Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
					Session["Email"] = data.FirstOrDefault().Email;
					Session["idUser"] = data.FirstOrDefault().Id;
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.error = "Login failed";
					return RedirectToAction("Login");
				}
			}
			return View();
		}

		//Logout
		public ActionResult Logout()
		{
			Session.Clear();//remove session
			return RedirectToAction("Login");
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