using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models.Databases;

namespace MyWebsite.Areas.API
{
	[ApiController]
	[Route("API/[controller]")]
	public class GUTScheduleController : ExtendedController
	{
		public GUTScheduleController(DatabaseContext context) : base(context) { }

		[Route("SemesterOffsetDay")]
		public string SemesterOffsetDay() =>
			Database.CustomData.Find("gut.schedule.semester.offset")?.Value ?? "undefined";
	}
}