using Microsoft.AspNetCore.Mvc;
using System;
using MyWebsite.Controllers;
using MyWebsite.Models.Databases;
using System.Linq;

namespace MyWebsite.Areas.API
{
	[ApiController]
	[Route("API/[controller]")]
	public class GUTScheduleController : ExtendedController
	{
		GUTScheduleDatabaseContext databaseContext;
		public GUTScheduleController(DatabaseContext context, GUTScheduleDatabaseContext db) : base(context) =>
			databaseContext = db;

		[Route("SemesterOffsetDay")]
		public string SemesterOffsetDay()
		{
			try
			{
				return DateTime.Parse(databaseContext.OffsetDates?.FirstOrDefault()?.Value).Ticks.ToString();
			}
			catch
			{
				return "undefined";
			}
		}
	}
}