using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Assignment4_Practice.Models.EF_Models;

namespace Assignment4_Practice.Infrastructure
{
	public class APIHandler
	{
		static string Base_URL = "https://api.fda.gov/food/enforcement.json?";
		//No Api key needed;
		HttpClient httpClient;

		public APIHandler()
		{
			httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(
		  new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
		}
		public Rootobject GetReports()
		{
			string API_Path = Base_URL + "limit=5";
			string reportData = "";
			Rootobject reportObject = null;

			httpClient.BaseAddress = new Uri(API_Path);
			HttpResponseMessage response = httpClient.GetAsync(API_Path).GetAwaiter().GetResult();

			if (response.IsSuccessStatusCode)
			{
				reportData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
			}

			if (!reportData.Equals(""))
			{
				reportObject = JsonConvert.DeserializeObject<Rootobject>(reportData);
			}

			return reportObject;

		}
		public Rootobject GetTampaReports()
		{
			string API_Path = Base_URL + "search=city:Tampa&limit=5";
			string tampaData = "";
			Rootobject tampaObject = null;

			httpClient.BaseAddress = new Uri(API_Path);
			HttpResponseMessage response = httpClient.GetAsync(API_Path).GetAwaiter().GetResult();

			if (response.IsSuccessStatusCode)
			{
				tampaData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
			}

			if (!tampaData.Equals(""))
			{
				tampaObject = JsonConvert.DeserializeObject<Rootobject>(tampaData);
			}

			return tampaObject;

		}
	}
}
