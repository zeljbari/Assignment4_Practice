using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Assignment4_Practice.Models.EF_Models;

namespace Assignment4_Practice.Models.ViewModel
{
	public class FavoriteReports
	{
		public Report Reports { get; set; }
		public Favorite Current { get; set; }

		public string recall_number { get; set; }
	}
}
