using Microsoft.AspNetCore.Mvc;
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
		public string SemesterOffsetDay() =>
			databaseContext.OffsetDates?.FirstOrDefault()?.Value ?? "undefined";
	}
}