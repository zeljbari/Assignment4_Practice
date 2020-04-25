using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment4_Practice.Models;
using static Assignment4_Practice.Models.EF_Models;
using static Assignment4_Practice.Infrastructure.APIHandler;
using Microsoft.EntityFrameworkCore;

using System.Net.Http;
using Newtonsoft.Json;

using Assignment4_Practice.DataAccess;
using Assignment4_Practice.Infrastructure;

namespace Assignment4_Practice.Controllers
{
	public class HomeController : Controller
	{
		public ApplicationDbContext dbContext;
		
		

		public HomeController(ApplicationDbContext context)
		{
			dbContext = context;		
		}
		

		public IActionResult Index()
		{
			ViewBag.dbSuccessComp = 0;
			APIHandler webHandler = new APIHandler();

			Rootobject reportObject = webHandler.GetReports();

			TempData["Reports"] = JsonConvert.SerializeObject(reportObject);

			return View(reportObject);
		}
		public IActionResult SaveFavorite(string recall_ID, string country, string reason, string product,string company)
		{
			try
			{ 
				
				Favorite favorite = new Favorite();
				favorite.reason_for_recall = reason;
				favorite.product_description = product;
				favorite.recalling_firm = company;
				favorite.recall_number = recall_ID;
				favorite.country = country;
				
					dbContext.Favorites.Add(favorite);
				
				
				dbContext.SaveChanges();
			}
			catch(Exception ex)
			{
				
			}
			

			return RedirectToAction("Index");
		}
		public IActionResult UpdateReport(string recall_ID, string country, string reason, string product, string company)
		{
			try
			{
				foreach(Report report in dbContext.Reports)
				{
					if (report.recall_number == recall_ID)
					{
						report.reason_for_recall = reason;
						report.product_description = product;
						report.recalling_firm = company;
						report.recall_number = recall_ID;
						report.country = country;

						dbContext.Reports.Update(report);
					}
				}

				dbContext.SaveChanges();
			}
			catch (Exception ex)
			{

			}


			return RedirectToAction("Index");
		}
		public IActionResult GetReportDetails(string recall_number)
		{
			foreach (Report report in dbContext.Reports)
			{
				if (report.recall_number == recall_number)
				{
					ViewBag.id = report.recall_number;
					ViewBag.productDescription = report.product_description;
					ViewBag.quantity = report.product_quantity;
					ViewBag.reason = report.reason_for_recall;
					
					ViewBag.company = report.recalling_firm;
					ViewBag.address = report.address_1;
					ViewBag.city = report.city;
					ViewBag.state = report.state;
					ViewBag.Country = report.country;
					ViewBag.distribution = report.distribution_pattern;
					ViewBag.date = report.report_date;
					ViewBag.voluntary = report.voluntary_mandated;									

				}


			}
			
			return View("ThankYou");
		}
		public IActionResult DeleteReport(string recall_number)
		{



			foreach (Report report in dbContext.Reports)
			{
				if (report.recall_number == recall_number)
				{
					dbContext.Reports.Remove(report);
					
				}
				

			}
			dbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		
		
		public IActionResult TampaReports()
		{
			ViewBag.dbSuccessComp = 0;

			APIHandler webHandler = new APIHandler();

			Rootobject reportObject = webHandler.GetTampaReports();

			TempData["TampaReports"] = JsonConvert.SerializeObject(reportObject);

			return View(reportObject);
		}
		/*
		public IActionResult Reports()
		{
			ViewBag.dbSuccessComp = 0;
			Rootobject rootObject = GetReports();

			TempData["Reports"] = JsonConvert.SerializeObject(rootObject);

			return View(rootObject);
		}
		*/
		public IActionResult PopulateTampaReports()
		{
			APIHandler webHandler = new APIHandler();

			Rootobject rootObject = webHandler.GetTampaReports();

			TempData["Reports"] = JsonConvert.SerializeObject(rootObject);

			rootObject = JsonConvert.DeserializeObject<Rootobject>(TempData["TampaReports"].ToString());

			foreach (Report report in rootObject.results)
			{
				if (dbContext.Reports.Where(c => c.recall_number.Equals(report.recall_number)).Count() == 0)
				{
					dbContext.Reports.Add(report);
				}


			}

			dbContext.SaveChanges();
			ViewBag.dbSuccessComp = 1;
			return View("TampaReports", rootObject);
		}
		
		public IActionResult PopulateReports()
		{

			APIHandler webHandler = new APIHandler();

			Rootobject rootObject = webHandler.GetReports();

			TempData["Reports"] = JsonConvert.SerializeObject(rootObject);

			rootObject = JsonConvert.DeserializeObject<Rootobject>(TempData["Reports"].ToString());

			foreach (Report report in rootObject.results)
			{
				if (dbContext.Reports.Where(c => c.recall_number.Equals(report.recall_number)).Count() == 0)
				{
					dbContext.Reports.Add(report);
					
				}


			}

			dbContext.SaveChanges();
			ViewBag.dbSuccessComp = 1;
			return View("Index", rootObject);
		}


		

		

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult AboutUs()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
