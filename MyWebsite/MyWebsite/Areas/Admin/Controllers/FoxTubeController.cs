using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class FoxTubeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Screenshots()
		{
			return View();
		}
	}
}