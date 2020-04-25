using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment4_Practice.Models;
using Microsoft.EntityFrameworkCore;
using static Assignment4_Practice.Models.EF_Models;

namespace Assignment4_Practice.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Report> Reports { get; set; }
		public DbSet<Favorite> Favorites { get; set; }
	}
}
